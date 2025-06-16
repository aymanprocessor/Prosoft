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

        public async Task<IActionResult> SaveRecordPriceListDetail([FromBody] List<PriceListDetailDTO> records)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (records == null || !records.Any())
            {
                return Json(new { success = false, message = "No data received." });
            }

            try
            {
                
                await _termsPriceListRepo.AddTermPriceListBatchAsync(records[0].PLId, _mapper.Map<List<TermsPriceListEditAddDTO>>(records));

                return Json(new { success = true, message = "Records saved successfully!" });
            }
            catch (Exception ex)
            {
                // Optional: Log exception
                return Json(new
                {
                    success = false,
                    message = "An error occurred while saving records.",
                    error = ex.Message
                });
            }
        }


        public async Task<IActionResult> UpdateRecordPriceListDetail([FromBody] List<TermsPriceListEditAddDTO> records)
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


                await _termsPriceListRepo.EditTermPriceListBatchAsync(records);



                return StatusCode(200, new
                {
                    success = true,
                    message = "Data Edited",
                    data = records
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while saving records." });

            }
        }


        [HttpPost]
        public async Task<JsonResult> DeletePriceListDetail(int id)
        {

            try
            {
                var product = await _termsPriceListRepo.GetByIdAsync(id);
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
