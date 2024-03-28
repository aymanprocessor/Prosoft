using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.Analysis;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class AnalysisDetailRepo : IAnalysisDetailRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public AnalysisDetailRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<List<AnalysiDtlViewDTO>> GetAnalysisDtlByClinicTransAsync(int visitId)
        {
            ClinicTran clickedTrans = await _Context.ClinicTrans.FirstOrDefaultAsync(obj => obj.CheckId == visitId);
            var subcode = clickedTrans.SubCode;
            var maincode = clickedTrans.MainCode;
            List<Analdetail> analysis = await _Context.Analdetails
                .Where(obj => obj.Codeanalcode == subcode && obj.SubCode == maincode
                    && obj.MasterId == clickedTrans.MasterId)
                .ToListAsync();
            List<AnalysiDtlViewDTO> analysisDtlDTO = _mapper.Map<List<AnalysiDtlViewDTO>>(analysis);
            return analysisDtlDTO;
        }

        public async Task<AnalysisDtlEditDTO> GetAnalysisDtlByIdAsync(int id)
        {
            Analdetail analdetail = await _Context.Analdetails
                .FirstOrDefaultAsync(obj => obj.Id == id);
            AnalysisDtlEditDTO analdetailDTO = _mapper.Map<AnalysisDtlEditDTO>(analdetail);
            return analdetailDTO;
        }

        public async Task EditAnalysisDtl(int id, AnalysisDtlEditDTO analysisDtlEditDTO)
        {
            Analdetail existingAnalysisDtl = await _Context.Analdetails
                .FirstOrDefaultAsync(obj => obj.Id == id);

            _mapper.Map(analysisDtlEditDTO, existingAnalysisDtl);
      
            _Context.Update(existingAnalysisDtl);
            await _Context.SaveChangesAsync();
        }

        public async Task<AnalysisPrintDTO> PrintAnalysisDtlAsync(int clinicTransID)
        {
            ClinicTran clickedTrans = await _Context.ClinicTrans
                 .Include(obj => obj.Serv)
                 .FirstOrDefaultAsync(obj => obj.CheckId == clinicTransID);
            var subcode = clickedTrans.SubCode;
            var maincode = clickedTrans.MainCode;

            //for get patient
            Pat pat = await _Context.Pats.FirstOrDefaultAsync(obj => obj.PatId == clickedTrans.PatId);
            var patName = pat.PatName;
            var patId = pat.PatId;
            var age = pat.NewOld;

            //for get patAdmission
            PatAdmission patAdmition = await _Context.PatAdmissions.FirstOrDefaultAsync(obj => obj.MasterId == clickedTrans.MasterId);
            var admissonDate = patAdmition.PatAdDate;
            // for comp and compDetail
            var compid = patAdmition.CompId;
            var compidDetail = patAdmition.CompIdDtl;
            //get id for comp and compDetail
            Company comp = await _Context.Companies.FirstOrDefaultAsync(obj => obj.CompId == compid);
            CompanyDtl compdetail = await _Context.CompanyDtls.FirstOrDefaultAsync(obj => obj.CompIdDtl == compidDetail);
            var compName = comp.CompName;
            var compNameDetail = compdetail.CompNameDtl;
            var analysisName = clickedTrans.Serv.ServDesc;

            //for get doctor
            Doctor doctor = await _Context.Doctors.FirstOrDefaultAsync(obj => obj.DrId == patAdmition.DrCode);
            var drName = doctor.DrDesc;
            var drId = patAdmition.DrCode;


            List<Analdetail> analysis = await _Context.Analdetails
                .Where(obj => obj.Codeanalcode == subcode && obj.SubCode == maincode
                    && obj.MasterId == clickedTrans.MasterId)
                .ToListAsync();
            List<AnalysiDtlViewDTO> analysisDTO = _mapper.Map<List<AnalysiDtlViewDTO>>(analysis);

            AnalysisPrintDTO prinDatatDTO = new AnalysisPrintDTO ()
            {
               patName= patName,
               patId=patId,
               age= (double)age,
               admissonDate = (DateTime)admissonDate,
               drName= drName, 
               drId= (int)drId,
               compName = compName, 
               compNameDetail= compNameDetail,
               analysisName= analysisName,
               analysiDtlViewDTOs= analysisDTO
            };
            
            return prinDatatDTO;
        }
    }
}
