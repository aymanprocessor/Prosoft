using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
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
        private readonly IMapper _mapper;

        public PriceListNewController(IPriceListRepo priceListRepo, ITermsPriceListRepo termsPriceListRepo, IMainClinicRepo mainClinicRepo, ISubClinicRepo subClinicRepo, IServiceClinicRepo serviceClinicRepo, IMapper mapper)
        {
            _priceListRepo = priceListRepo;
            _termsPriceListRepo = termsPriceListRepo;
            _mainClinicRepo = mainClinicRepo;
            _subClinicRepo = subClinicRepo;
            _serviceClinicRepo = serviceClinicRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            List<PriceList> priceLists = await _priceListRepo.GetAllAsync();
            List<PriceListDetail> priceListDetailRaw = await _termsPriceListRepo.GetAllPriceListDetails(id);
            List<PriceListDetailDTO> priceListDetail = _mapper.Map<List<PriceListDetailDTO>>(priceListDetailRaw);
            List<SelectListItem> mainClinics = (await _mainClinicRepo.GetAllAsync()).Select(x => new SelectListItem { Value = x.ClinicId.ToString(), Text = x.ClinicDesc }).ToList();
            List<SelectListItem> subClinics = (await _subClinicRepo.GetAllAsync()).Select(x => new SelectListItem { Value = x.SClinicId.ToString(), Text = x.SClinicDesc }).ToList();
            List<SelectListItem> services = (await _serviceClinicRepo.GetAllAsync()).Select(x => new SelectListItem { Value = x.ServId.ToString(), Text = x.ServDesc }).ToList();
            PriceListDTO priceListDTO = new() { PriceList = priceLists, PriceListDetail = priceListDetail, MainClinic = mainClinics, SubClinic = subClinics, Services = services };
            return View(priceListDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetPriceList()
        {
            List<PriceList> priceList = await _priceListRepo.GetAllAsync();
            return Json( priceList );
        }


        [HttpGet]
        public async Task<IActionResult> GetPriceListDetail(int id)
        {
            if (id == 0)
            {
                return Json(new { success = false, message = "ID is not exist" });

            }
            var priceListDetail = await _termsPriceListRepo.GetAllAsync();
            return Json(priceListDetail.Where(x => x.PLId == id).ToList());
        }

        [HttpPost]
        

        public async Task<IActionResult> SaveRecordPriceList([FromBody] List<PriceListEditAddDTO> records)
        {

            

          
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (records == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Request body is null or invalid JSON format."
                    });
                }

            
                await _priceListRepo.AddBatchPriceListsAsync(records);



                return StatusCode(200, new
                {
                    success = true,
                    message = "Data Added",
                    data = records
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while saving records." });

            }
        }

        public async Task<IActionResult> UpdateRecordPriceList([FromBody] List<PriceListEditAddDTO> records)
        {




            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (records == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Request body is null or invalid JSON format."
                    });
                }


                await _priceListRepo.EditPriceListBatchAsync(records);



                return StatusCode(200, new
                {
                    success = true,
                    message = "Data Added",
                    data = records
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while saving records." });

            }
        }
        [HttpPost]
       
        public async Task<JsonResult> DeletePriceList( int id)
        {

            try
            {
                var product = await _priceListRepo.GetByIdAsync(id);
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

        public async Task<IActionResult> SaveRecordPriceListDetail([FromBody] PriceListDetailRecordDTO records)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (records == null)
            {
                return Json(new { success = false, message = "No data received." });
            }

            try
            {

                // Insert new records
                if (records.InsertData != null && records.InsertData.Any())
                {
                    foreach (var record in records.InsertData)
                    {
                        // Map DTO to entity and insert
                        var newEntity = new PriceListDetail
                        {
                            ServOnOff = record.ServOnOff,
                            PLId = record.PLId,
                            ClinicId = record.ClinicId,
                            SClinicId = record.SClinicId,
                            ServId = record.ServId,
                            ServBefDesc = record.ServBefDesc,
                            DiscoutComp = record.DiscoutComp,
                            PlValue = record.PlValue,
                            CompCovPercentage = record.CompCovPercentage,
                            CompValue = record.CompValue,
                            PlValue2 = record.PlValue2,
                            PlValue3 = record.PlValue3,
                            ExtraVal = record.ExtraVal,
                            ExtraVal2 = record.ExtraVal2,
                            Covered = record.Covered,
                            BranchId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "U_Branch_Id").Value),

                        };
                        await _termsPriceListRepo.AddAsync(newEntity);

                    }
                    await _termsPriceListRepo.SaveChangesAsync();

                }

                // Update existing records
                if (records.UpdateData != null && records.UpdateData.Any())
                {
                    foreach (var record in records.UpdateData)
                    {
                        var existingEntity = await _termsPriceListRepo.GetByIdAsync((int)record.PLDtlId);
                        if (existingEntity != null)
                        {
                            existingEntity.ServOnOff = record.ServOnOff;
                            existingEntity.ClinicId = record.ClinicId;
                            existingEntity.SClinicId = record.SClinicId;
                            existingEntity.ServId = record.ServId;
                            existingEntity.ServBefDesc = record.ServBefDesc;
                            existingEntity.DiscoutComp = record.DiscoutComp;
                            existingEntity.PlValue = record.PlValue;
                            existingEntity.CompCovPercentage = record.CompCovPercentage;
                            existingEntity.CompValue = record.CompValue;
                            existingEntity.PlValue2 = record.PlValue2;
                            existingEntity.PlValue3 = record.PlValue3;
                            existingEntity.ExtraVal = record.ExtraVal;
                            existingEntity.ExtraVal2 = record.ExtraVal2;
                            existingEntity.Covered = record.Covered;
                            existingEntity.BranchId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "U_Branch_Id").Value);
                            await _termsPriceListRepo.UpdateAsync(existingEntity);
                        }
                        else
                        {
                            return Json(new { success = false, message = "Record is not exists!" });

                        }


                    }
                    await _termsPriceListRepo.SaveChangesAsync();

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
        public async Task<JsonResult> DeletePriceListDetail([FromBody] PriceListDetailDTO model)
        {

            try
            {
                var product = await _termsPriceListRepo.GetByIdAsync((int)model.PLDtlId);
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
