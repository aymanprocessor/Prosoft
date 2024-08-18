using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class AccSubCodeEditRepo : IAccSubCodeEditRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public AccSubCodeEditRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<DisplayAccSubCodeEditDTO> GetAllDataAsync()
        {
            DisplayAccSubCodeEditDTO displayAccSubCodeEditDTO = new DisplayAccSubCodeEditDTO();
            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes.Where(main => _Context.AccSubCodes.Any(sub => sub.MainCode == main.MainCode)).ToListAsync();
            displayAccSubCodeEditDTO.branchs = _mapper.Map<List<BranchDTO>>(branches);
            displayAccSubCodeEditDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);

            return displayAccSubCodeEditDTO;
        }

        public async Task<List<AccSubCodeEditDTO>> GetAccSubCodeEditAsync(string mainCode)
        {
            // الحصول على قائمة AccSubCode بناءً على MainCode
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == mainCode)
                .ToListAsync();

            // الحصول على قائمة AccSubCodeEdit الموجودة بالفعل
            List<AccSubCodeEdit> accSubCodeEdits = await _Context.AccSubCodeEdits
                .Where(obj => obj.MainCode == mainCode)
                .ToListAsync();

            // إنشاء قائمة لتخزين الكائنات الجديدة من AccSubCodeEdit
            List<AccSubCodeEdit> accSubCodeEditsInsert = new List<AccSubCodeEdit>();

            // التحقق من كل عنصر في قائمة AccSubCode
            foreach (var item in accSubCodes)
            {
                // التحقق مما إذا كان العنصر موجودًا بالفعل في قائمة AccSubCodeEdit
                bool exists = accSubCodeEdits.Any(bal => bal.MainCode == mainCode && (bal.SubCodeFr == item.SubCode || bal.SubCodeTo ==item.SubCode));

                // إذا لم يكن موجودًا، قم بإنشاء كائن جديد وإضافته إلى قائمة الإدخالات
                if (!exists)
                {
                    AccSubCodeEdit accSubCodeEditObj = new AccSubCodeEdit
                    {
                        MainCode = item.MainCode,
                        SubCodeFr = item.SubCode,
                        SubCodeTo = ""
                    };

                    accSubCodeEditsInsert.Add(accSubCodeEditObj);
                }
            }

            // إدخال جميع الكائنات الجديدة إذا كانت موجودة
            if (accSubCodeEditsInsert.Any())
            {
                await _Context.AccSubCodeEdits.AddRangeAsync(accSubCodeEditsInsert);
                await _Context.SaveChangesAsync();
            }

            // الحصول على الكائنات المدخلة كقائمة
            List<AccSubCodeEdit> accSubCodeEditsDisplay = await _Context.AccSubCodeEdits
                .Where(obj => obj.MainCode == mainCode)
                .ToListAsync();

            // تحويل الكائنات إلى DTO باستخدام حلقة foreach غير متزامنة
            List<AccSubCodeEditDTO> accSubCodeEditsDisplayDTO = new List<AccSubCodeEditDTO>();

            foreach (var obj in accSubCodeEditsDisplay)
            {
                var subName = obj.SubCodeTo == ""
                    ? await GetSubName(obj.SubCodeFr, obj.MainCode)
                    : await GetSubName(obj.SubCodeTo, obj.MainCode);
                accSubCodeEditsDisplayDTO.Add(new AccSubCodeEditDTO
                {
                    SubName = subName,
                    SubCodeFr = obj.SubCodeFr,
                    SubCodeTo = obj.SubCodeTo,
                });
            }

            return accSubCodeEditsDisplayDTO;
        }

        // Get Sub Name
        private async Task<string> GetSubName(string subCode, string mainCode)
        {
            var accSubCode = await _Context.AccSubCodes
                                           .FirstOrDefaultAsync(cc => cc.SubCode == subCode && cc.MainCode == mainCode);
            return accSubCode?.SubName ?? ""; // Return "" if no matching sub code is found
        }

        public async Task<AccSubCodeEditDTO> GetAccAccSubCodeEditById(string id, string subFr)
        {
            AccSubCodeEdit accSubCodeEdit = await _Context.AccSubCodeEdits.FirstOrDefaultAsync(obj=>obj.MainCode == id && obj.SubCodeFr == subFr);
            AccSubCodeEditDTO accSubCodeEditDTO = _mapper.Map<AccSubCodeEditDTO>(accSubCodeEdit);
            return accSubCodeEditDTO;
        }
        public async Task EditAccSubCodeEditAsync(string id, int fYear, int branch, AccSubCodeEditDTO accSubCodeEditDTO)
        {
            var subCodeFr = accSubCodeEditDTO.SubCodeFr;
            var subCodeTo = accSubCodeEditDTO.SubCodeTo;
            var branchId = branch; // Assuming accSubCodeEditDTO contains BranchId
            var fiscalYear = fYear; // Assuming accSubCodeEditDTO contains FiscalYear

            // Handle AccSubCodeEdit separately
            var accSubCodeEdit = await _Context.AccSubCodeEdits
                .FirstOrDefaultAsync(obj => obj.MainCode == id && obj.SubCodeFr == subCodeFr);

            if (accSubCodeEdit != null)
            {
                _Context.AccSubCodeEdits.Remove(accSubCodeEdit);
                await _Context.SaveChangesAsync();

                var newAccSubCodeEdit = new AccSubCodeEdit
                {
                    MainCode = id,
                    SubCodeFr = subCodeFr,
                    SubCodeTo = subCodeTo
                };
                _Context.AccSubCodeEdits.Add(newAccSubCodeEdit);
            }

            // Update acc_start_bal
            var accStartBal = await _Context.AccStartBals
                .Where(a => a.MainCode == id && a.SubCode == subCodeFr && a.CoCode == branchId && a.FYear == fiscalYear)
                .ToListAsync();
            accStartBal.ForEach(a => a.SubCode = subCodeTo);

            // Update acc_trans_detail
            var accTransDetail = await _Context.AccTransDetails
                .Where(a => a.MainCode == id && a.SubCode == subCodeFr && a.CoCode == branchId && a.FYear == fiscalYear)
                .ToListAsync();
            accTransDetail.ForEach(a => a.SubCode = subCodeTo);

            // Update acc_bal_all
            var accBalAll = await _Context.AccBalAlls
                .Where(a => a.MainCode == id && a.SubCode == subCodeFr)
                .ToListAsync();
            accBalAll.ForEach(a => a.SubCode = subCodeTo);

            // Handle AccSubCode separately
            var accSubCode = await _Context.AccSubCodes
                .Where(a => a.MainCode == id && a.SubCode == subCodeFr)
                .ToListAsync();
            if (accSubCode.Any())
            {
                _Context.AccSubCodes.RemoveRange(accSubCode);
                await _Context.SaveChangesAsync();
                foreach (var a in accSubCode)
                {
                    a.SubCode = subCodeTo;
                    _Context.AccSubCodes.Add(a);
                }
            }

            // Update acc_safe_cash
            var accSafeCash = await _Context.AccSafeCashes
                .Where(a => a.MainCode == id && a.SubCode == subCodeFr && a.BranchId == branchId && a.FYear == fiscalYear)
                .ToListAsync();
            accSafeCash.ForEach(a => a.SubCode = subCodeTo);

            // Update acc_safe_check
            var accSafeCheck = await _Context.AccSafeChecks
                .Where(a => a.MainCode == id && a.SubCode == subCodeFr && a.BranchId == branchId && a.FYear == fiscalYear)
                .ToListAsync();
            accSafeCheck.ForEach(a => a.SubCode = subCodeTo);

            // Update pat_admission
            var patAdmission = await _Context.PatAdmissions
                .Where(a => a.MainCode == id && a.SubCode == subCodeFr && a.BranchId == branchId && a.ExYear == fiscalYear)
                .ToListAsync();
            patAdmission.ForEach(a => a.SubCode = subCodeTo);

            // Update company
            var company = await _Context.Companies
                .Where(a => a.MainCode == id && a.SubCode == subCodeFr && a.BranchId == branchId)
                .ToListAsync();
            company.ForEach(a => a.SubCode = subCodeTo);

            // Update doctors
            var doctors = await _Context.Doctors
                .Where(a => a.MainCode == id && a.SubCode == subCodeFr && a.BranchId == branchId)
                .ToListAsync();
            doctors.ForEach(a => a.SubCode = subCodeTo);

            // Save changes to all entities
            await _Context.SaveChangesAsync();
        }

    }
}
