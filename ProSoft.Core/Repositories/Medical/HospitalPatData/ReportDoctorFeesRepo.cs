using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.Report;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class ReportDoctorFeesRepo : IReportDoctorFeesRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public ReportDoctorFeesRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<ReportDoctorFeesDTO> GetAllDataAsync()
        {
            ReportDoctorFeesDTO reportDoctorFeesDTO = new ReportDoctorFeesDTO();
            List<Doctor> doctors = await _Context.Doctors.ToListAsync();
            reportDoctorFeesDTO.Doctors = _mapper.Map<List<DoctorViewDTO>>(doctors);
            return reportDoctorFeesDTO;
        }

        public async Task<List<DoctorFeesFromToDTO>> GetReportDoctorFeesData(int drCode,  DateTime fromPeriod, DateTime toPeriod, int branchId)
        {
            List<DoctorFeesFromToDTO> doctorFeesFromToDTOs = new List<DoctorFeesFromToDTO>();
            var clinicTranss = await _Context.ClinicTrans.Where(obj => obj.BranchId == branchId &&
                      (obj.PatAdDate >= fromPeriod && obj.PatAdDate <= toPeriod)&&
                      (drCode == 100 || obj.DrSend == drCode) && obj.DrDueVal > 0 )
                   .ToListAsync();
            foreach (var cli in clinicTranss)
            {
                var patAddmission = await _Context.PatAdmissions.FirstOrDefaultAsync(obj=>obj.MasterId == cli.MasterId);
                var pat = await _Context.Pats.FirstOrDefaultAsync(obj=>obj.PatId == patAddmission.PatId);
                var regionName = (await _Context.Regions.FirstOrDefaultAsync(obj=>obj.RegionCode == patAddmission.SendFr)).RegionDesc;
                var companyName = (await _Context.Companies.FirstOrDefaultAsync(obj => obj.CompId == (patAddmission.CompId ?? 0))).CompName;
                var ServDec = (await _Context.ServiceClinics.FirstOrDefaultAsync(obj=>obj.ServId ==cli.ServId)).ServDesc;
                var dto = new DoctorFeesFromToDTO
                {
                    RegionDesc = regionName,
                    PatId = pat.PatId,
                    PatName =pat.PatName,
                    CompanyName = companyName,
                    ServDesc = ServDec,
                    MainInvNo = patAddmission.MainInvNo,
                    PatAdDate = cli.PatAdDate,
                    DrDueVal = cli.DrDueVal,
                    DrTax = 0,
                    DrSendVal = cli.DrSendVal,
                };
                doctorFeesFromToDTOs.Add(dto);
            }
            return doctorFeesFromToDTOs;
        }
    }
}
