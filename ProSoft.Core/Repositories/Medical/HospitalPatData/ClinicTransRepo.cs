using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.Analysis;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Stocks;
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
        public async Task<IEnumerable<ClinicTran>> GetClinicTransAsync()
        {
            return await _Context.ClinicTrans.ToListAsync();
        }
        //public IEnumerable<ClinicTran> GetClinicTransactionsWithDoctorAndPatient()
        //{
        //    var Trans = from ct in _Context.ClinicTrans
        //                join d in _Context.Doctors on new { x1 = ct.BranchId, x2 = ct.DrCode } equals new { x1 = (int?)d.BranchId, x2 = (int?)d.DrId }
        //                join p in _Context.Pats on new { x1 = ct.BranchId, x2 = ct.PatId } equals new { x1 = (int?)p.BranchId, x2 = (int?)p.PatId }
        //                select
        //}


        public async Task<IEnumerable<ClinicTran>> ClinicTransRangeFilter(string range)
        {
            var today = DateTime.Today;
            var Trans = _Context.ClinicTrans.AsQueryable();
            switch (range)
            {
                case "today":
                   return await Trans.Where(c => c.ExDate!.Value.Date == today.Date).ToListAsync();
                case "tomorrow":
                    return await Trans.Where(c => c.ExDate!.Value.Date == today.AddDays(1).Date).ToListAsync();
                case "week":
                    var endOfWeek = today.AddDays(7);
                    return await Trans.Where(c => c.ExDate!.Value.Date >= today.Date && c.ExDate!.Value.Date <= endOfWeek.Date).ToListAsync();
                case "month":
                    var endOfMonth = today.AddMonths(1);
                    return await Trans.Where(c => c.ExDate!.Value.Date >= today.Date && c.ExDate!.Value.Date <= endOfMonth.Date).ToListAsync();
                default:
                    return await Trans.ToListAsync();

            }
        }
        public int ClinicTransCounts()
        {
            return _Context.ClinicTrans.Count();
        }
        public int ClinicTransCountsDaily()
        {
            return _Context.ClinicTrans.Count(c => c.ExDate!.Value.Date == DateTime.Now.Date );

        }
        public int ClinicTransCountsWeekly()
        {
            var today = DateTime.Now;
            var startOfWeek = today.AddDays(-today.DayOfYear);
            return _Context.ClinicTrans.Count(c => c.ExDate!.Value.Date <= today.Date && c.ExDate!.Value.Date >= startOfWeek.Date);

        }
        public async Task<List<ClinicTransViewDTO>> GetClinicTransByAdmissionAsync(int visitId, int flag)
        {
            List<ClinicTransViewDTO> clinicTransDTO = await _Context.ClinicTrans
               .Where(obj => obj.MasterId == visitId && obj.Flag == flag)
               .Select(obj => new ClinicTransViewDTO()
               {
                   CheckId = (int)obj.CheckId,
                   MasterId = (int)obj.MasterId,
                   ItmServFlag = (int)obj.ItmServFlag,
                   ExDate = (DateTime)obj.ExDate,
                   ClinicId = (int)obj.ClinicId,
                   SClinicId = Convert.ToInt32(obj.SClinicId),
                   ServId = Convert.ToInt32(obj.ServId),
                   SubId = Convert.ToInt16(obj.SubId),
                   DrSendId = (int)obj.DrSend,
                   Qty = (int)obj.Qty,
                   UnitPrice = Convert.ToDecimal(obj.UnitPrice),
                   ValueService = Convert.ToDecimal(obj.ValueService),
                   PatientValue = Convert.ToDecimal(obj.PatientValue),
                   CompValue = Convert.ToDecimal(obj.CompValue),
                   DoctorValue = Convert.ToDecimal(obj.DrVal),
                   HospitalValue = Convert.ToDecimal(obj.HoVal),
                   DiscountVal = Convert.ToDecimal(obj.DiscountVal),
                   ExtraVal = Convert.ToDecimal(obj.ExtraVal),
                   ExtraVal2 = Convert.ToDecimal(obj.ExtraVal2),
                   ApprovalPeriod = Convert.ToInt32(obj.ApprovalPeriod),
                   CheckIdCancel = Convert.ToInt32(obj.CheckIdCancel),
                   Counter = (int)obj.Counter,
                   StockId = (int)obj.StockCode,
                   SubName = obj.Sub.SubName,
                   
                   ServDesc = obj.Serv.ServDesc,
                   DrDesc = obj.DrSendNavigation.DrDesc,
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
            //to set invoice number
            ClinicTran clinicTran = await _Context.ClinicTrans.OrderBy(obj => obj.ExInvoiceNo).LastOrDefaultAsync();
            clinicTransEditAddDTO.ExInvoiceNo = clinicTran != null? (int)(clinicTran.ExInvoiceNo + 1):1;

            return clinicTransEditAddDTO;
        }

        /////////////////////////////////// Get for Ajax //////////////////////////////////
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

        public async Task<TermsPriceListViewDTO> GetPricesDetails(int id,int clincID,int sClincID,int servID)
        {
            PatAdmission patAdmission = await _Context.PatAdmissions.FirstOrDefaultAsync(obj => obj.MasterId == id);
            Company company = await _Context.Companies.FirstOrDefaultAsync(obj=>obj.CompId ==patAdmission.CompId);
            PriceList priceList = await _Context.PriceLists.FirstOrDefaultAsync(obj=>obj.PLId ==company.PLId);
           
            PriceListDetail priceListDetail = await _Context.PriceListDetails
                .FirstOrDefaultAsync(obj => obj.ClinicId== clincID && obj.SClinicId ==sClincID && obj.ServId ==servID && obj.PLId == priceList.PLId);

            TermsPriceListViewDTO TermsPriceListDTO = _mapper.Map<TermsPriceListViewDTO>(priceListDetail);
            return TermsPriceListDTO;
        }
        //get service clink to get percentage of doctor
        public async Task<ServiceClinicViewDTO> GetServiceClinicByIDs(int id, int sClincID, int servID)
        {
            ServiceClinic serviceClinic = await _Context.ServiceClinics
                .FirstOrDefaultAsync(obj=>obj.ClinicId ==id && obj.SClinicId ==sClincID && obj.ServId ==servID);
            ServiceClinicViewDTO serviceClinicDTO = _mapper.Map<ServiceClinicViewDTO>(serviceClinic);
            return serviceClinicDTO;
        }
        //get patAdmission for Know Private or contract
        public async Task<PatAdmissionEditAddDTO> GetPatAdmissionByIdAsync(int id)
        {
            PatAdmission patAdmission = await _Context.PatAdmissions
               .FirstOrDefaultAsync(obj => obj.MasterId == id);

            PatAdmissionEditAddDTO patAdmissionDTO = _mapper.Map<PatAdmissionEditAddDTO>(patAdmission);
            return patAdmissionDTO;
        }
        public async Task<DoctorPrecentViewDTO> GetDoctorPrices(int id, int sClincID, int servID)
        {
            DoctorsPercent doctorsPercent = await _Context.DoctorsPercents
                .FirstOrDefaultAsync(obj=>obj.DrCode==id && obj.SubCode == sClincID && obj.SubDetailCodeL1 == servID);
            DoctorPrecentViewDTO doctorPrecentDTO = _mapper.Map<DoctorPrecentViewDTO>(doctorsPercent);
           
            return doctorPrecentDTO;
        }
        //get all prices of services belong to visit for patient
        public async Task<decimal> GetPricesOfServices(int visitId, int flag)
        {
           List<ClinicTran> clinicTrans = await _Context.ClinicTrans
                .Where(obj => obj.MasterId == visitId && obj.Flag == flag).ToListAsync();

            decimal totalPrice = 0;
            foreach (var item in clinicTrans)
            {
                   totalPrice += Convert.ToDecimal(item.ValueService ?? 0);
            }
            return totalPrice;
        }
        //get all prices of services belong to visit for patient
        public async Task<decimal> GetAllDepositForVisit(int visitId)
        {
           List<Deposit> deposits = await _Context.Deposits
                .Where(obj => obj.MasterId == visitId && obj.PostRecipt == 1).ToListAsync();

            decimal totalDeposit = 0;
            foreach (var item in deposits)
            {
                totalDeposit += Convert.ToDecimal(item.DpsVal ?? 0);
            }
            return totalDeposit;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        public async Task AddClinicTransAsync(int visitId, int flag, ClinicTransEditAddDTO clinicTransDTO)
        {
            PatAdmission patAdmission = await _Context.PatAdmissions
                     .FirstOrDefaultAsync(obj => obj.MasterId == visitId);
            var SubClinic = await _Context.SubClinics.FirstOrDefaultAsync(s => s.ClinicId == clinicTransDTO.ClinicId && s.SClinicId == clinicTransDTO.SClinicId);
            if( SubClinic != null)
            {

            clinicTransDTO.StockCode = (int)SubClinic.StockCd;
            }
            clinicTransDTO.PatId = patAdmission.PatId;
            clinicTransDTO.CompId = patAdmission.CompId;
            clinicTransDTO.CompIdDtl = patAdmission.CompIdDtl;
            clinicTransDTO.SendFr = patAdmission.SendFr;
            clinicTransDTO.SendTo = patAdmission.SendTo;
            clinicTransDTO.MainInvNo = patAdmission.MainInvNo;
            clinicTransDTO.SessionNo = patAdmission.SessionNo;
            clinicTransDTO.DrCode = patAdmission.DrCode;

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

            if (clinicTran.ItmServFlag == 2)
            {
                var SubItem = await _Context.SubItems.FirstOrDefaultAsync(s => s.SubId == clinicTran.SubId);
                if (SubItem != null)
                {
                    clinicTran.ItemMaster = SubItem.ItemCode;
                    
                }
            }

                await _Context.AddAsync(clinicTran);
            await _Context.SaveChangesAsync();

            //Belong to Deposit
            // Call GetPricesOfServices to get the total price of services
            decimal totalPrice = await GetPricesOfServices(visitId, flag);

            // Call GetAllDepositForVisit to get the total Deposit of visit
            decimal totalDeposit = await GetAllDepositForVisit(visitId);

            if (totalPrice > totalDeposit)
            {
                //delete service deposite 
                Deposit depositWillDeleted = await _Context.Deposits.FirstOrDefaultAsync(obj=>obj.MasterId == visitId && obj.PostRecipt != 1);
                if (depositWillDeleted != null)
                {
                    _Context.Remove(depositWillDeleted);
                    await _Context.SaveChangesAsync();    
                }

                //add new service
                DepositEditAddDTO depositDTO = new DepositEditAddDTO();
                depositDTO.DpsType = 1;
                depositDTO.DpsVal = Convert.ToDecimal(totalPrice - totalDeposit);
                depositDTO.DpsType = 1;
                Deposit deposit = _mapper.Map<Deposit>(depositDTO);
                deposit.MasterId = visitId;
                deposit.ModId = 11;
                deposit.DpsDate = clinicTran.ExDate;
                deposit.PatId = patAdmission.PatId;

                await _Context.AddAsync(deposit);
                await _Context.SaveChangesAsync();
                //amanat
                patAdmission.AmanatRetPat = 0;
            }
            else if (totalPrice < totalDeposit)
            {
                //amanat
                patAdmission.AmanatRetPat = 0;
                patAdmission.AmanatRetPat = Convert.ToDecimal(totalDeposit - totalPrice);
            }

        }

        private async Task UpdatePatientAndCompTotal(int masterId)
        {
            var patAdmission = await _Context.PatAdmissions
                        .FirstOrDefaultAsync(obj => obj.MasterId == masterId);

            var ClinicTrans = await _Context.ClinicTrans
                    .Where(obj => obj.MasterId == masterId && (obj.CheckIdCancel == 1 || obj.CheckIdCancel == 2)).ToListAsync();

            decimal? patientValueTotal =  0;
            decimal? compValueTotal =  0;
            foreach (var clinic in ClinicTrans)
            {
                patientValueTotal += clinic.PatientValue;
                compValueTotal += clinic.CompValue;
            }
            patAdmission.PatientValue = patientValueTotal;
            patAdmission.CompValue = compValueTotal;
            await _Context.SaveChangesAsync();

        }

        private async Task UpdateDoctorValueAndHospitalValue(int masterId)
        {
            var patAdmission = await _Context.PatAdmissions
                .FirstOrDefaultAsync(obj => obj.MasterId == masterId);

            var ClinicTrans = await _Context.ClinicTrans
                .Where(obj => obj.MasterId == masterId ).ToListAsync();

            foreach (var clinic in ClinicTrans)
            {
                var doctorPercent =
                    await _Context.DoctorsPercents.FirstOrDefaultAsync(dr =>
                        dr.DrCode == patAdmission.DrCode && dr.SubCode == clinic.SClinicId && dr.SubDetailCodeL1 == clinic.ServId);
                var drPerc = patAdmission.Deal switch
                {
                    1 => doctorPercent.DrPerc,
                    2=> doctorPercent.DrPercContract
                };

                var drValue = drPerc * clinic.ValueService / 100;
                clinic.DrVal =  drValue;
                clinic.HoVal = clinic.ValueService - drValue;
            }
            await _Context.SaveChangesAsync();
           

        }
        //********************** BATCH ADD CLINIC TRANS **********************//
        public async Task AddClinicTransListAsync(int visitId, int flag, List<ClinicTransEditAddDTO> clinicTransDTOList)
        {
            using var transaction = await _Context.Database.BeginTransactionAsync();

            try
            {
                // Get common data once for all rows
                PatAdmission patAdmission = await _Context.PatAdmissions
                         .FirstOrDefaultAsync(obj => obj.MasterId == visitId);

                if (patAdmission == null)
                {
                    throw new Exception($"PatAdmission not found for visitId: {visitId}");
                }

                // Get the current counter
                ClinicTran lastClinicTrans = await _Context.ClinicTrans
                        .OrderBy(obj => obj.Counter)
                        .LastOrDefaultAsync(obj => obj.MasterId == visitId);

                int currentCounter = lastClinicTrans?.Counter ?? 0;
                
                // Process each row
                foreach (var clinicTransDTO in clinicTransDTOList)
                {
                  
                    // Set counter
                    currentCounter++;
                    clinicTransDTO.Counter = currentCounter;

                    // Get SubClinic data
                    var SubClinic = await _Context.SubClinics
                        .FirstOrDefaultAsync(s => s.ClinicId == clinicTransDTO.ClinicId && s.SClinicId == clinicTransDTO.SClinicId);

                    if (SubClinic != null)
                    {
                        clinicTransDTO.StockCode = (int)SubClinic.StockCd;
                    }

                    // Set common properties from patAdmission
                    clinicTransDTO.PatId = patAdmission.PatId;
                    clinicTransDTO.CompId = patAdmission.CompId;
                    clinicTransDTO.CompIdDtl = patAdmission.CompIdDtl;
                    clinicTransDTO.SendFr = patAdmission.SendFr;
                    clinicTransDTO.SendTo = patAdmission.SendTo;
                    clinicTransDTO.MainInvNo = patAdmission.MainInvNo;
                    clinicTransDTO.SessionNo = patAdmission.SessionNo;
                    clinicTransDTO.DrCode = patAdmission.DrCode;
                    clinicTransDTO.Flag = flag;
                    clinicTransDTO.MasterId = visitId;

                    // Handle analysis logic (SClinicId == 5)
                    if (clinicTransDTO.SClinicId == 5)
                    {
                        await ProcessAnalysisForRow(clinicTransDTO, visitId,(int) patAdmission.PatId);
                    }

                    // Map to entity
                    ClinicTran clinicTran = _mapper.Map<ClinicTran>(clinicTransDTO);

                    // Handle SubItem logic
                    if (clinicTran.ItmServFlag == 2)
                    {
                        var SubItem = await _Context.SubItems
                            .FirstOrDefaultAsync(s => s.SubId == clinicTran.SubId);
                        if (SubItem != null)
                        {
                            clinicTran.ItemMaster = SubItem.ItemCode;
                        }
                    }
                    // Add to context
                    await _Context.AddAsync(clinicTran);
                }

              

                // Save all changes
                await _Context.SaveChangesAsync();

                await UpdatePatientAndCompTotal(visitId);

                await UpdateDoctorValueAndHospitalValue(visitId);
                // Handle Deposit logic once for all rows
                await ProcessDepositForVisit(visitId, flag, patAdmission);

                // Commit transaction
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error adding clinic trans list: {ex.Message}", ex);
            }
        }



        // Separate method for analysis processing
        private async Task ProcessAnalysisForRow(ClinicTransEditAddDTO clinicTransDTO, int visitId, int patId)
        {
            // Get serviceClinic object by clinicTran
            ServiceClinic serviceClinic = await _Context.ServiceClinics
                .FirstOrDefaultAsync(obj => obj.ServId == clinicTransDTO.ServId);

            if (serviceClinic == null) return;

            // Get subAnalysis object by serviceClinic
            MedicalAnalysisSub subAnalysis = await _Context.MedicalAnalysisSubs
                .FirstOrDefaultAsync(obj => obj.SubName == serviceClinic.ServDesc);

            if (subAnalysis == null) return;

            // Set props in variables
            var subCode = subAnalysis.SubCode;
            var mainCode = subAnalysis.MainCode;

            clinicTransDTO.SubCode = subCode;
            clinicTransDTO.MainCode = mainCode;

            // Get itemAnalysis by variables
            List<Itemanalysis> itemAnalysisList = await _Context.Itemanalyses
                .Where(obj => obj.Codeanalcode == subCode && obj.Subanalcode == mainCode)
                .ToListAsync();

            // Set props in new Analdetail
            foreach (var item in itemAnalysisList)
            {
                var newDetail = new Analdetail
                {
                    PatId = patId,
                    Codeanalcode = item.Codeanalcode,
                    Itemanalcode = item.Itemanalcode,
                    Itemanalname = item.Itemanalname,
                    MasterId = visitId,
                    SubCode = item.Subanalcode,
                    MainCode = item.Mainanalcode
                };

                _Context.Add(newDetail);
            }
        }

        // Separate method for deposit processing
        private async Task ProcessDepositForVisit(int visitId, int flag, PatAdmission patAdmission)
        {
            // Call GetPricesOfServices to get the total price of services
            decimal totalPrice = await GetPricesOfServices(visitId, flag);

            // Call GetAllDepositForVisit to get the total Deposit of visit
            decimal totalDeposit = await GetAllDepositForVisit(visitId);

            if (totalPrice > totalDeposit)
            {
                // Delete service deposit 
                Deposit depositWillDeleted = await _Context.Deposits
                    .FirstOrDefaultAsync(obj => obj.MasterId == visitId && obj.PostRecipt != 1);

                if (depositWillDeleted != null)
                {
                    _Context.Remove(depositWillDeleted);
                    await _Context.SaveChangesAsync();
                }

                // Add new service
                DepositEditAddDTO depositDTO = new DepositEditAddDTO
                {
                    DpsType = 1,
                    DpsVal = Convert.ToDecimal(totalPrice - totalDeposit)
                };

                Deposit deposit = _mapper.Map<Deposit>(depositDTO);
                deposit.MasterId = visitId;
                deposit.ModId = 11;
                deposit.DpsDate = DateTime.Now; // Assuming current date
                deposit.PatId = patAdmission.PatId;

                await _Context.AddAsync(deposit);
                await _Context.SaveChangesAsync();

                // Reset amanat
                patAdmission.AmanatRetPat = 0;
            }
            else if (totalPrice < totalDeposit)
            {
                // Set amanat
                patAdmission.AmanatRetPat = Convert.ToDecimal(totalDeposit - totalPrice);
            }
            else
            {
                // Equal amounts
                patAdmission.AmanatRetPat = 0;
            }
        }


        //***********************************//
        public async Task EditClinicTransAsync(int checkId, ClinicTransEditAddDTO clinicTransDTO)
        {
            ClinicTran myClinicTran = await _Context.ClinicTrans
                .FirstOrDefaultAsync(obj => obj.CheckId == checkId);
            var admissionId = myClinicTran.MasterId;
            var counter = myClinicTran.Counter;
            var itemServflag = myClinicTran.ItmServFlag;
            var flag = myClinicTran.Flag;
            var patId = myClinicTran.PatId;
            var stockCode = myClinicTran.StockCode;

            //mapper
            _mapper.Map(clinicTransDTO, myClinicTran);
            myClinicTran.CheckId = checkId;
            myClinicTran.MasterId = admissionId;
            myClinicTran.Counter = counter;
            myClinicTran.ItmServFlag = itemServflag;
            myClinicTran.Flag = flag;
            myClinicTran.PatId = patId;
            myClinicTran.StockCode = stockCode;

            if (myClinicTran.ItmServFlag == 2)
            {
                var SubItem = await _Context.SubItems.FirstOrDefaultAsync(s => s.SubId == myClinicTran.SubId);
                if (SubItem != null)
                {
                    myClinicTran.ItemMaster = SubItem.ItemCode;

                }
            }
            _Context.Update(myClinicTran);
            await _Context.SaveChangesAsync();
        }


        public async Task EditClinicTransBatchAsync(List<ClinicTransEditAddDTO> clinicTransDTOs)
        {
            var checkIds = clinicTransDTOs.Select(dto => dto.CheckId).ToList();

            // Get common data once for all rows
            PatAdmission patAdmission = await _Context.PatAdmissions
                     .FirstOrDefaultAsync(obj => obj.MasterId == clinicTransDTOs[0].MasterId);

            if (patAdmission == null)
            {
                throw new Exception($"PatAdmission not found for visitId: {clinicTransDTOs[0].MasterId}");
            }

            // Fetch all ClinicTrans records in one query
            var clinicTransList = await _Context.ClinicTrans
                .Where(ct => checkIds.Contains(ct.CheckId))
                .ToListAsync();



            foreach (var dto in clinicTransDTOs)
            {

           

                var myClinicTran = clinicTransList.FirstOrDefault(ct => ct.CheckId == dto.CheckId);
                if (myClinicTran == null)
                    continue; // or handle as needed

                // Preserve properties
                var admissionId = myClinicTran.MasterId;
                var counter = myClinicTran.Counter;
                var itemServflag = myClinicTran.ItmServFlag;
                var flag = myClinicTran.Flag;
                var patId = myClinicTran.PatId;
                var stockCode = myClinicTran.StockCode;

                // Map new values from DTO
                _mapper.Map(dto, myClinicTran);

                // Restore preserved values
                myClinicTran.CheckId = dto.CheckId;
                myClinicTran.MasterId = admissionId;
                myClinicTran.Counter = counter;
                myClinicTran.ItmServFlag = itemServflag;
                myClinicTran.Flag = flag;
                myClinicTran.PatId = patId;
                myClinicTran.StockCode = stockCode;

                // Special logic for ItmServFlag == 2
                if (myClinicTran.ItmServFlag == 2)
                {
                    var subItem = await _Context.SubItems.FirstOrDefaultAsync(s => s.SubId == myClinicTran.SubId);
                    if (subItem != null)
                    {
                        myClinicTran.ItemMaster = subItem.ItemCode;
                    }
                }

                _Context.Update(myClinicTran);
            }
       

            await _Context.SaveChangesAsync();
            await UpdatePatientAndCompTotal((int)clinicTransDTOs[0].MasterId);
            await UpdateDoctorValueAndHospitalValue((int)clinicTransDTOs[0].MasterId);

        }

        public async Task DeleteClinicTransAsync(int id)
        {
            ClinicTran clinicTran = await _Context.ClinicTrans.FirstOrDefaultAsync(obj => obj.CheckId == id);
            _Context.Remove(clinicTran);
            await _Context.SaveChangesAsync();
        }

    }
}
