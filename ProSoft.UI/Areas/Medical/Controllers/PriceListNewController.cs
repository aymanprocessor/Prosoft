using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DTOs;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]

    public class PriceListNewController : Controller
    {
        private readonly IPriceListRepo _priceListRepo;
        private readonly ITermsPriceListRepo _termsPriceListRepo;
        private readonly IMainClinicRepo _mainClinicRepo;
        private readonly ISubClinicRepo _subClinicRepo;
        private readonly IServiceClinicRepo _serviceClinicRepo;

        public PriceListNewController(IPriceListRepo priceListRepo, ITermsPriceListRepo termsPriceListRepo, IMainClinicRepo mainClinicRepo, ISubClinicRepo subClinicRepo, IServiceClinicRepo serviceClinicRepo)
        {
            _priceListRepo = priceListRepo;
            _termsPriceListRepo = termsPriceListRepo;
            _mainClinicRepo = mainClinicRepo;
            _subClinicRepo = subClinicRepo;
            _serviceClinicRepo = serviceClinicRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            List <PriceList> priceLists = await _priceListRepo.GetAllAsync();
            List <PriceListDetail> priceListDetail = await _termsPriceListRepo.GetAllPriceListDetails(id);
            List <SelectListItem> mainClinics = (await _mainClinicRepo.GetAllAsync()).Select(x => new SelectListItem { Value = x.ClinicId.ToString(),Text = x.ClinicDesc}).ToList();
            List <SelectListItem> subClinics = (await _subClinicRepo.GetAllAsync()).Select(x => new SelectListItem { Value = x.SClinicId.ToString(), Text = x.SClinicDesc }).ToList();
            List<SelectListItem> services = (await _serviceClinicRepo.GetAllAsync()).Select(x => new SelectListItem { Value = x.ServId.ToString(), Text = x.ServDesc }).ToList();
            PriceListDTO priceListDTO = new() { PriceList = priceLists ,PriceListDetail = priceListDetail,MainClinic = mainClinics , SubClinic = subClinics,Services = services };
            return View(priceListDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> SaveRecords([FromBody] PriceListRecordsDTO records)
        {

            if (records == null)
            {
                return Json( new { success = false, message = "No data received." });
            }

            try
            {

                // Insert new records
                if (records.InsertData != null && records.InsertData.Any())
                {
                    foreach (var record in records.InsertData)
                    {
                        // Map DTO to entity and insert
                        var newEntity = new PriceList
                        {
                            Flag1 = record.Flag1,
                            PlDesc = record.PlDesc,
                            PLDate = record.PLDate,
                            Year = record.Year,
                            BranchId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "U_Branch_Id").Value),

                        };
                        await _priceListRepo.AddAsync(newEntity);

                    }
                    await _priceListRepo.SaveChangesAsync();

                }

                // Update existing records
                if (records.UpdateData != null && records.UpdateData.Any())
                {
                    foreach (var record in records.UpdateData)
                    {
                        var existingEntity = await _priceListRepo.GetByIdAsync(record.PLId);
                        if (existingEntity != null)
                        {
                            existingEntity.Flag1 = record.Flag1;
                            existingEntity.PlDesc = record.PlDesc;
                            existingEntity.PLDate = record.PLDate;
                            existingEntity.Year = record.Year;
                            await _priceListRepo.UpdateAsync(existingEntity);
                        }


                    }
                    await _priceListRepo.SaveChangesAsync();

                }

                
                return Json(new { success = true, message = "Records saved successfully!" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while saving records." });

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeletePriceList([FromBody] PriceListDTO model)
        {
            
            try
            {
                var product = await _priceListRepo.GetByIdAsync(model.PLId);
                if (product == null)
                {
                    return Json(new { success = false, message = "Product not found" });
                }
                await _priceListRepo.DeleteAsync(product);
                await _priceListRepo.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeletePriceListDetail([FromBody] PriceListDetailDTO model)
        {

            try
            {
                var product = await _termsPriceListRepo.GetByIdAsync(model.PLDtlId);
                if (product == null)
                {
                    return Json(new { success = false, message = "Product not found" });
                }
                await _termsPriceListRepo.DeleteAsync(product);
                await _termsPriceListRepo.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


    }
}
