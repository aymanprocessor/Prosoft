using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area(nameof(Medical))]
    public class DegreeAndRoomAndBedController : Controller
    {
        private readonly IDegreeCodeRepo _degreeCodeRepo;
        private readonly IRoomCodeRepo _roomCodeRepo;
        private readonly IBedCodeRepo _bedCodeRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public DegreeAndRoomAndBedController(ICurrentUserService currentUserService, IDegreeCodeRepo degreeCodeRepo, IRoomCodeRepo roomCodeRepo, IBedCodeRepo bedCodeRepo, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _degreeCodeRepo = degreeCodeRepo;
            _roomCodeRepo = roomCodeRepo;
            _bedCodeRepo = bedCodeRepo;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        //******************************** DEGREE ********************************//
        [HttpGet]
        public async Task<IActionResult> AddDegree()
        {
        
            ViewBag.NewCode = await _degreeCodeRepo.GetNewIdAsync();
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddDegree(DegreeRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.DegreeType = 100;
            model.DegreeOnOff = 1;
            model.Branch = _currentUserService.BranchId;
            DegreeCode degreeCode = _mapper.Map<DegreeCode>(model);
            await _degreeCodeRepo.AddAsync(degreeCode);
            await _degreeCodeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> EditDegree(int id)
        {
            DegreeCode degreeCode = await _degreeCodeRepo.GetByIdAsync(id);

            if (degreeCode == null)
            {
                return BadRequest(new { success = false, message = "Degree ID mismatch" });
            }
            try
            {

               
                DegreeRequestDTO degreeRequestDTO = _mapper.Map<DegreeRequestDTO>(degreeCode);

                ViewBag.DegreeName = degreeCode?.DegreeName;
                return View(degreeRequestDTO);
            }
            catch (ServiceException serEx)
            {
                return StatusCode(500, new { success = false, message = serEx.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { success = false, message = "An unexpected error occurred." });
            }
           

        }

        [HttpPost]
        public async Task<IActionResult> EditDegree(int id,DegreeRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid customer data.", errors = ModelState });
            }

            model.DegreeType = 100;
            model.Branch = _currentUserService.BranchId;

            DegreeCode degreeCode = await _degreeCodeRepo.GetByIdAsync(id);

            _mapper.Map(model, degreeCode);
            
            await _degreeCodeRepo.UpdateAsync(degreeCode);
            await _degreeCodeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> DeleteDegree(int id)
        {
            DegreeCode degreeCode = await _degreeCodeRepo.GetByIdAsync(id);
            await _degreeCodeRepo.DeleteAsync(degreeCode);
            await _degreeCodeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> GetDegrees()
        {
            var Degrees = await _degreeCodeRepo.GetAllAsync();
            var result = new
            {
                draw = 1,
                recordsTotal = Degrees.Count(),       // Total number of records
                recordsFiltered = Degrees.Count(),    // Total filtered records (for paging)
                data = Degrees                        // Actual data to display
            };

            return Json(result);


        }

     



        //******************************** ROOM ********************************//

        [HttpGet]
        public async Task<IActionResult> AddRoom(int id)
        {
            ViewBag.DegreeId = id;
            ViewBag.NewCode = await _roomCodeRepo.GetNewIdAsync();
            ViewBag.Branch = _currentUserService.BranchId;
            ViewBag.RoomOnOff = 1;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            RoomCode roomCode = _mapper.Map<RoomCode>(model);
            roomCode.DegreeCode = model.DegreeId;
            await _roomCodeRepo.AddAsync(roomCode);
            await _roomCodeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> EditRoom(int id)
        {

            RoomCode roomCode = await _roomCodeRepo.GetByIdAsync(id);
            if (roomCode == null)
            {
                return NotFound();
            }
            RoomRequestDTO roomRequestDTO = _mapper.Map<RoomRequestDTO>(roomCode);

            ViewBag.RoomCode = roomCode?.Id;
            return View(roomRequestDTO);

        }

        [HttpPost]
        public async Task<IActionResult> EditRoom(int id, RoomRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Branch = _currentUserService.BranchId;

            RoomCode roomCode = await _roomCodeRepo.GetByIdAsync(id);

            _mapper.Map(model, roomCode);

            await _roomCodeRepo.UpdateAsync(roomCode);
            await _roomCodeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            RoomCode roomCode = await _roomCodeRepo.GetByIdAsync(id);
            await _roomCodeRepo.DeleteAsync(roomCode);
            await _roomCodeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> GetRoomsByDegreeId(int degreeId)
        {
            // Fetch room data based on DegreeId (use your database logic)
            var rooms = await _roomCodeRepo.GetRoomsByDegreeId(degreeId); // Replace with your service/repository call
            var result = new
            {
                draw = 1,
                recordsTotal = rooms.Count(),       // Total number of records
                recordsFiltered = rooms.Count(),    // Total filtered records (for paging)
                data = rooms                        // Actual data to display
            };
            return Json(result);
        }


        //******************************** BED ********************************//

        [HttpGet]
        public async Task<IActionResult> AddBed(int degreeId,int roomId)
        {
            ViewBag.DegreeId = degreeId;
            ViewBag.RoomId = roomId;
            ViewBag.NewCode = await _bedCodeRepo.GetNewIdAsync();
            ViewBag.Branch = _currentUserService.BranchId;
            ViewBag.BookId = 0;
            ViewBag.BedOnOff = 1;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddBed(BedRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            BedCode bedCode = _mapper.Map<BedCode>(model);
            bedCode.DegreeCode = model.DegreeId;
            bedCode.RoomCode = model.RoomId;
            bedCode.BedCodeSys = model.DegreeId.ToString() + model.RoomId.ToString() + model.Id.ToString();
            await _bedCodeRepo.AddAsync(bedCode);
            await _bedCodeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> EditBed(int id)
        {

            BedCode bedCode = await _bedCodeRepo.GetByIdAsync(id);
            if (bedCode == null)
            {
                return NotFound();
            }
            BedRequestDTO roomRequestDTO = _mapper.Map<BedRequestDTO>(bedCode);

            ViewBag.BedCode = bedCode?.Id;
            return View(roomRequestDTO);

        }

        [HttpPost]
        public async Task<IActionResult> EditBed(int id, RoomRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Branch = _currentUserService.BranchId;

            BedCode bedCode = await _bedCodeRepo.GetByIdAsync(id);

            _mapper.Map(model, bedCode);

            await _bedCodeRepo.UpdateAsync(bedCode);
            await _bedCodeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> DeleteBed(int id)
        {
            BedCode bedCode = await _bedCodeRepo.GetByIdAsync(id);
            await _bedCodeRepo.DeleteAsync(bedCode);
            await _bedCodeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> GetBedsByDegreeIdAndRoomId(int degreeId, int roomId)
        {
            // Fetch room data based on DegreeId (use your database logic)
            var beds = await _bedCodeRepo.GetBedsByDegreeIdAndRoomId(degreeId, roomId); // Replace with your service/repository call
            var result = new
            {
                draw = 1,
                recordsTotal = beds.Count(),       // Total number of records
                recordsFiltered = beds.Count(),    // Total filtered records (for paging)
                data = beds                        // Actual data to display
            };
            return Json(result);
        }


    }
}
