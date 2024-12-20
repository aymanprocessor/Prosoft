using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

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
            if(degreeCode == null)
            {
                return NotFound();
            }
            DegreeRequestDTO degreeRequestDTO = _mapper.Map<DegreeRequestDTO>(degreeCode);
            
            ViewBag.DegreeName = degreeCode?.DegreeName;
            return View(degreeRequestDTO);

        }

        [HttpPost]
        public async Task<IActionResult> EditDegree(int id,DegreeRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
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
        public async Task<IActionResult> AddRoom()
        {

            ViewBag.NewCode = await _roomCodeRepo.GetNewIdAsync();
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            model.RoomOnOff = 1;
            model.Branch = _currentUserService.BranchId;
            RoomCode roomCode = _mapper.Map<RoomCode>(model);
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
        public IActionResult GetRoomsByDegreeId(int degreeId)
        {
            // Fetch room data based on DegreeId (use your database logic)
            var rooms = _roomCodeRepo.GetRoomsByDegreeId(degreeId); // Replace with your service/repository call

            return Json(rooms);
        }
    }
}
