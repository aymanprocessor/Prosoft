using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class JournalTypeController : Controller
    {
        private readonly IJournalTypeRepo _journalTypeRepo;
        private readonly IMapper _mapper;

        public JournalTypeController(IJournalTypeRepo journalTypeRepo, IMapper mapper)
        {
            _journalTypeRepo = journalTypeRepo;
            _mapper = mapper;

        }
        public async Task<IActionResult> Index()
        {
            List<JournalType> journalTypes = await _journalTypeRepo.GetAllAsync();
            List<JournalTypeDTO> journalTypeDTOs= _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            return View(journalTypeDTOs);
        }

        //Get add
        public async Task<IActionResult> Add_JournalType(int journalCode)
        {
            ViewBag.JournalID = await _journalTypeRepo.GetNewIdAsync();
            return View();
        }

        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_JournalType(JournalTypeDTO journalTypeDTO)
        {
            if (ModelState.IsValid)
            {
                JournalType journalType = _mapper.Map<JournalType>(journalTypeDTO);

                await _journalTypeRepo.AddAsync(journalType);
                await _journalTypeRepo.SaveChangesAsync();
                return RedirectToAction("Index", "JournalType");
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_JournalType(int id)
        {
            JournalType journalType = await _journalTypeRepo.GetByIdAsync(id);
            JournalTypeDTO journalTypeDTO = _mapper.Map<JournalTypeDTO>(journalType);
            return View(journalTypeDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_JournalType(int id, JournalTypeDTO journalTypeDTO)
        {
            if (ModelState.IsValid)
            {
                JournalType journalType = await _journalTypeRepo.GetByIdAsync(id);
                _mapper.Map(journalTypeDTO, journalType);

                await _journalTypeRepo.UpdateAsync(journalType);
                await _journalTypeRepo.SaveChangesAsync();
                return RedirectToAction("Index", "JournalType");
            }
            return View(journalTypeDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_JournalType(int id)
        {
            JournalType journalType = await _journalTypeRepo.GetByIdAsync(id);

            await _journalTypeRepo.DeleteAsync(journalType);
            await _journalTypeRepo.SaveChangesAsync();
            return RedirectToAction("Index", "JournalType");
        }
    }
}
