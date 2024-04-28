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
    public class ClinicTransRepo : IClinicTransRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public ClinicTransRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<List<ClinicTransViewDTO>> GetClinicTransByAdmissionAsync(int visitId, int flag)
        {
            List<ClinicTransViewDTO> clinicTransDTO = await _Context.ClinicTrans
               .Where(obj => obj.MasterId == visitId && obj.Flag == flag)
               .Select(obj => new ClinicTransViewDTO()
               {
                   CheckId = (int)obj.CheckId,
                   Counter = (int)obj.Counter,
                   ItmServFlag = (int)obj.ItmServFlag,
                   SubName = obj.Sub.SubName,
                   SClinicId = Convert.ToInt32(obj.SClinicId),
                   ServDesc = obj.Serv.ServDesc,
                   DrDesc = obj.DrSendNavigation.DrDesc,
                   UnitPrice = Convert.ToDecimal(obj.UnitPrice),
                   PatientValue = Convert.ToDecimal(obj.PatientValue),
                   ExtraVal = Convert.ToDecimal(obj.ExtraVal),
                   ExtraVal2 = Convert.ToDecimal(obj.ExtraVal2),
                   CompValue = Convert.ToDecimal(obj.CompValue),
                   DiscountVal = Convert.ToDecimal(obj.DiscountVal),
                   ApprovalPeriod = Convert.ToInt32(obj.ApprovalPeriod),
                   CheckIdCancel = Convert.ToInt32(obj.CheckIdCancel)
               })
               .ToListAsync(); 

            return clinicTransDTO;
        }

        public async Task<ClinicTransEditAddDTO> GetClinicTransByIdAsync(int checkId)
        {
            ClinicTran cliniccTrans = await _Context.ClinicTrans
                .FirstOrDefaultAsync(obj => obj.CheckId == checkId);
            ClinicTransEditAddDTO cliniccTransDTO = _mapper.Map<ClinicTransEditAddDTO>(cliniccTrans);

            List<MainClinic> mainClinics = await _Context.MainClinics.ToListAsync();
            List<MainItem> mainItems = await _Context.MainItems.ToListAsync();
            List<Doctor> doctors = await _Context.Doctors.ToListAsync();
            List<SubClinic> subClinics = await _Context.SubClinics.ToListAsync();
            List<ServiceClinic> serviceClinics = await _Context.ServiceClinics.ToListAsync();
            List<SubItem> subItems = await _Context.SubItems.ToListAsync();

            cliniccTransDTO.MainClinics = _mapper.Map<List<MainClinicViewDTO>>(mainClinics);
            cliniccTransDTO.MainItems = _mapper.Map<List<MainItemViewDTO>>(mainItems);
            cliniccTransDTO.Doctors = _mapper.Map<List<DoctorViewDTO>>(doctors);
            cliniccTransDTO.SubClinics = _mapper.Map<List<SubClinicViewDTO>>(subClinics);
            cliniccTransDTO.ServiceClinics = _mapper.Map<List<ServiceClinicViewDTO>>(serviceClinics);
            cliniccTransDTO.SubItems = _mapper.Map<List<SubItemViewDTO>>(subItems);

            return cliniccTransDTO;
        }

        public async Task<ClinicTransEditAddDTO> GetEmptyClinicTransAsync()
        {
            ClinicTransEditAddDTO clinicTransEditAddDTO = new ClinicTransEditAddDTO();

            List<MainClinic> mainClinics =await _Context.MainClinics.ToListAsync();
            List<MainItem> mainItems =await _Context.MainItems.ToListAsync();
            List<Doctor> doctors =await _Context.Doctors.ToListAsync();

            clinicTransEditAddDTO.MainClinics=_mapper.Map<List<MainClinicViewDTO>>(mainClinics);
            clinicTransEditAddDTO.MainItems=_mapper.Map<List<MainItemViewDTO>>(mainItems);
            clinicTransEditAddDTO.Doctors=_mapper.Map<List<DoctorViewDTO>>(doctors);

            return clinicTransEditAddDTO;
        }

        ///////////////////////////////////Get for Ajax//////////////////////////////////
        public async Task<List<SubClinicViewDTO>> GetSubClinic(int id)
        {
           List<SubClinic> subClinics =await _Context.SubClinics.Where(obj => obj.ClinicId == id).ToListAsync();
           List<SubClinicViewDTO> subClinicsDTO = _mapper.Map<List<SubClinicViewDTO>>(subClinics);
           return subClinicsDTO;
        }

        public async Task<List<SubItemViewDTO>> GetSubItem(int id)
        {
            List<SubItem> subItems = await _Context.SubItems.Where(obj => obj.MainId == id).ToListAsync();
            List<SubItemViewDTO> subItemsDTO =_mapper.Map<List<SubItemViewDTO>>(subItems);
            return subItemsDTO;
        }

        public async Task<List<ServiceClinicViewDTO>> GetServeClinic(int id)
        {
            List<ServiceClinic> serviceClinics = await _Context.ServiceClinics.Where(obj => obj.SClinicId == id).ToListAsync();
            List<ServiceClinicViewDTO> serviceClinicsDTO = _mapper.Map<List<ServiceClinicViewDTO>>(serviceClinics);
            return serviceClinicsDTO;

        }

        public async Task<ClinicTransEditAddDTO> GetPricesDetails(int id,int clincID,int sClincID,int servID)
        {
            PatAdmission patAdmission = await _Context.PatAdmissions.FirstOrDefaultAsync(obj => obj.MasterId == id);
            Company company = await _Context.Companies.FirstOrDefaultAsync(obj=>obj.CompId ==patAdmission.CompId);
            PriceList priceList = await _Context.PriceLists.FirstOrDefaultAsync(obj=>obj.PLId ==company.PLId);
           
            PriceListDetail priceListDetail = await _Context.PriceListDetails
                .FirstOrDefaultAsync(obj => obj.ClinicId== clincID && obj.SClinicId ==sClincID && obj.ServId ==servID && obj.PLId == priceList.PLId);
            if (priceListDetail != null)
            {
                
            }
            ClinicTransEditAddDTO clinicTransEditAddDTO= new ClinicTransEditAddDTO();
            return clinicTransEditAddDTO;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        public async Task AddClinicTransAsync(int visitId, int flag, ClinicTransEditAddDTO clinicTransDTO)
        {
            PatAdmission patAdmission = await _Context.PatAdmissions
                     .FirstOrDefaultAsync(obj => obj.MasterId == visitId);
            clinicTransDTO.PatId = patAdmission.PatId;
            clinicTransDTO.CompId = patAdmission.CompId;
            clinicTransDTO.CompIdDtl = patAdmission.CompIdDtl;
            clinicTransDTO.SendFr = patAdmission.SendFr;
            clinicTransDTO.SendTo = patAdmission.SendTo;
            clinicTransDTO.MainInvNo = patAdmission.MainInvNo;
            clinicTransDTO.SessionNo = patAdmission.SessionNo;

            clinicTransDTO.Flag = flag;

            ClinicTran lastClinicTrans = await _Context.ClinicTrans.OrderBy(obj => obj.Counter)
                    .LastOrDefaultAsync(obj => obj.MasterId == visitId);
            //for set counter
            if (lastClinicTrans != null)
                clinicTransDTO.Counter = lastClinicTrans.Counter + 1;
            else
                clinicTransDTO.Counter = 1;

            clinicTransDTO.MasterId = visitId;

            //for analysis
            if (clinicTransDTO.SClinicId == 5)
            {
                // Get serviceClinic object by clinicTran
                ServiceClinic serviceClinic = await _Context.ServiceClinics
                    .FirstOrDefaultAsync(obj => obj.ServId == clinicTransDTO.ServId);

                // Get subAnalysis object by serviceClinic
                MedicalAnalysisSub subAnalysis = await _Context.MedicalAnalysisSubs
                    .FirstOrDefaultAsync(obj => obj.SubName == serviceClinic.ServDesc);

                // Set props in variables
                var subCode = subAnalysis.SubCode;
                var mainCode = subAnalysis.MainCode;

                clinicTransDTO.SubCode = subCode;
                clinicTransDTO.MainCode = mainCode;

                /////////////////////////////////////////

                // Get itemAnalysis by variables
                List<Itemanalysis> itemAnalysis = await _Context.Itemanalyses
                    .Where(obj => obj.Codeanalcode == subCode && obj.Subanalcode == mainCode)
                    .ToListAsync();

                // Set props in new Analdetail
                foreach (var item in itemAnalysis)
                {
                    var newDetail = new Analdetail();
                    newDetail.PatId = patAdmission.PatId;
                    newDetail.Codeanalcode = item.Codeanalcode;
                    newDetail.Itemanalcode = item.Itemanalcode;
                    newDetail.Itemanalname = item.Itemanalname;
                    newDetail.MasterId = visitId;
                    newDetail.SubCode = item.Subanalcode;
                    newDetail.MainCode = item.Mainanalcode;

                    _Context.Add(newDetail);
                }
            }
            ClinicTran clinicTran = _mapper.Map<ClinicTran>(clinicTransDTO);
            await _Context.AddAsync(clinicTran);
            await _Context.SaveChangesAsync();
        }
        
        public async Task EditClinicTransAsync(int checkId, ClinicTransEditAddDTO clinicTransDTO)
        {
            ClinicTran myClinicTran = await _Context.ClinicTrans
                .FirstOrDefaultAsync(obj => obj.CheckId == checkId);
            var admissionId = myClinicTran.MasterId;
            var counter = myClinicTran.Counter;
            var itemServflag = myClinicTran.ItmServFlag;
            var flag = myClinicTran.Flag;

            //mapper
            _mapper.Map(clinicTransDTO, myClinicTran);
            myClinicTran.CheckId = checkId;
            myClinicTran.MasterId = admissionId;
            myClinicTran.Counter = counter;
            myClinicTran.ItmServFlag = itemServflag;
            myClinicTran.Flag = flag;

            _Context.Update(myClinicTran);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteClinicTransAsync(int id)
        {
            ClinicTran clinicTran = await _Context.ClinicTrans.FirstOrDefaultAsync(obj => obj.CheckId == id);
            _Context.Remove(clinicTran);
            await _Context.SaveChangesAsync();
        }
    }
}
