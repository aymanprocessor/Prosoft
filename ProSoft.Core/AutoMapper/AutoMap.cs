﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.Analysis;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.Models.Treasury;
using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using ProSoft.EF.DTOs.Treasury.Report;
using ProSoft.EF.DTOs.Stocks.ExpenseData;

namespace ProSoft.Core.AutoMapper
{
    public class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<UserRegisterDTO, AppUser>().ReverseMap();
            CreateMap<UserDTO, AppUser>().ReverseMap();
            CreateMap<UserEditDTO, AppUser>().ReverseMap();
            CreateMap<RoleDTO, IdentityRole>().ReverseMap();
            /////////////////
            CreateMap<MainViewDTO, MedicalAnalysisMain>().ReverseMap();
            CreateMap<MainEditAddDTO, MedicalAnalysisMain>().ReverseMap();
            CreateMap<MainEditAddDTO, MainViewDTO>().ReverseMap();
            CreateMap<LabUnitDTO, LabUnit>().ReverseMap();
            CreateMap<SubViewDTO, MedicalAnalysisSub>().ReverseMap();
            CreateMap<SubEditAddDTO, MedicalAnalysisSub>().ReverseMap();
            CreateMap<SubEditAddDTO, SubViewDTO>().ReverseMap();
            CreateMap<ItemViewDTO, Itemanalysis>().ReverseMap();
            CreateMap<ItemEditAddDTO, Itemanalysis>().ReverseMap();
            CreateMap<ItemEditAddDTO, ItemViewDTO>().ReverseMap();
            /////////////////
            CreateMap<PatViewDTO, Pat>().ReverseMap();
            CreateMap<PatEditAddDTO, Pat>().ReverseMap();
            CreateMap<PatAdmissionViewDTO, PatAdmission>().ReverseMap();
            CreateMap<PatAdmissionEditAddDTO, PatAdmission>().ReverseMap();
            CreateMap<PatAdmissionEditAddDTO, PatAdmissionRequestDTO>().ReverseMap();
            CreateMap<MainClinicViewDTO, MainClinic>().ReverseMap();
            CreateMap<MainItemViewDTO, MainItem>().ReverseMap();
            CreateMap<MainItemEditAddDTO, MainItem>().ReverseMap();
            CreateMap<MainItemEditAddDTO, MainItemEditAddDTO>().ReverseMap();
            CreateMap<DoctorViewDTO, Doctor>().ReverseMap();
            CreateMap<DoctorEditAddDTO, Doctor>().ReverseMap();
            ///////////////////////////////////////
            CreateMap<SubClinicViewDTO, SubClinic>().ReverseMap();
            CreateMap<SubItemViewDTO, SubItem>().ReverseMap();
            CreateMap<SubItemEditAddDTO, SubItem>().ReverseMap();
            CreateMap<SubItemEditAddDTO, SubItemEditAddDTO>().ReverseMap();
            CreateMap<MainItem, SubItemEditAddDTO>().ReverseMap();
            CreateMap<MainItem, SubItem>().ReverseMap();
            CreateMap<ServiceClinicViewDTO, ServiceClinic>().ReverseMap();
            ////////////////////////////////////
            CreateMap<ClinicTransEditAddDTO, PatAdmission>().ReverseMap();
            CreateMap<ClinicTransEditAddDTO, ClinicTran>().ReverseMap();
            CreateMap<AnalysiDtlViewDTO, Analdetail>().ReverseMap();
            CreateMap<AnalysisDtlEditDTO, Analdetail>().ReverseMap();
            ///////////////////////////
            CreateMap<ClassificationViewDTO, ClassificationCust>().ReverseMap();
            CreateMap<CompanyViewDTO, Company>().ReverseMap();
            CreateMap<CompanyDtlViewDTO, CompanyDtl>().ReverseMap();
            CreateMap<RegionViewDTO, Region>().ReverseMap();
            ////////////////////////
            CreateMap<DrDiscountViewDTO, DrDiscount>().ReverseMap();
            CreateMap<DrDiscountRequestDTO, DrDiscount>().ReverseMap();
            CreateMap<DegreeRequestDTO, DegreeCode>().ReverseMap();
            CreateMap<RoomRequestDTO, RoomCode>().ReverseMap();
            CreateMap<BedRequestDTO, BedCode>().ReverseMap();
            CreateMap<NationalityDTO, NationalityEi>().ReverseMap();
            CreateMap<DrDegreeDTO, DrDegree>().ReverseMap();
            CreateMap<DocSubDtlViewDTO, DocSubDtl>().ReverseMap();
            CreateMap<DocSubDtlEditAddDTO, DocSubDtl>().ReverseMap();
            CreateMap<MainClinicEditAddDTO, MainClinic>().ReverseMap();
            CreateMap<SubClinicEditAddDTO, SubClinic>().ReverseMap();
            CreateMap<SubClinicViewDTO, SubClinic>().ReverseMap();
            CreateMap<ServiceClinicViewDTO, ServiceClinic>().ReverseMap();
            CreateMap<ServClinicEditAddDTO, ServiceClinic>().ReverseMap();
            CreateMap<ServiceTypeViewDTO, ServiceType>().ReverseMap();
            CreateMap<EisSectionTypeDTO, EisSectionType>().ReverseMap();
            CreateMap<DoctorPrecentViewDTO, DoctorsPercent>().ReverseMap();
            CreateMap<DoctorPrecentEditAddDTO, DoctorsPercent>().ReverseMap();
            CreateMap<PriceListViewDTO, PriceList>().ReverseMap();
            CreateMap<PriceListEditAddDTO, PriceList>().ReverseMap();
            CreateMap<TermsPriceListViewDTO, PriceListDetail>().ReverseMap();
            CreateMap<TermsPriceListEditAddDTO, PriceListDetail>().ReverseMap();
            CreateMap<TermsPriceListEditAddDTO, PriceListDetailDTO>().ReverseMap();
            CreateMap<PriceListDetailDTO, PriceListDetail>().ReverseMap();
            CreateMap<CompanyGroupDTO, CompanyGroup>().ReverseMap();
            CreateMap<CompanyViewDTO, Company>().ReverseMap();
            CreateMap<CompanyEditAddDTO, Company>().ReverseMap();
            CreateMap<CompanyDtlEditAddDTO, CompanyDtl>().ReverseMap();
            CreateMap<DepositViewDTO, Deposit>().ReverseMap();
            CreateMap<DepositEditAddDTO, Deposit>().ReverseMap();
            CreateMap<DrTimeSheetDTO, Drtimsheet>().ReverseMap();
            CreateMap<UsersSectionDTO, UsersSection>().ReverseMap();
            CreateMap<CheckupClinicDTO, CheckupClinic>().ReverseMap();
            ////////// Stocks ////////////////////
            CreateMap<AdjectiveCust, AdjectiveCustDTO>().ReverseMap();
            CreateMap<KindStore, KindStoreDTO>().ReverseMap();
            CreateMap<Branch, BranchDTO>().ReverseMap();
            CreateMap<Stock, StockEditAddDTO>().ReverseMap();
            CreateMap<StockViewDTO, Stock>().ReverseMap();
            CreateMap<Side, SideDTO>().ReverseMap();
            CreateMap<UnitCode, UnitCodeDTO>().ReverseMap();
            CreateMap<Sections2, SectionViewDTO>().ReverseMap();
            CreateMap<Sections2, SectionEditAddDTO>().ReverseMap();
            CreateMap<GeneralCode, PermissionDefViewDTO>().ReverseMap();
            CreateMap<GeneralCode, PermissionDefEditAddDTO>().ReverseMap();
            CreateMap<AppUser, UserTransViewDTO>().ReverseMap();
            CreateMap<UserTranss, UserTransEditAddDTO>().ReverseMap();
            CreateMap<StoreTran, StoreTransDTO>().ReverseMap();
            CreateMap<UserTranss, UserTransViewDTO>().ReverseMap();
            CreateMap<SupCode, SupCodeEditAddDTO>().ReverseMap();
            CreateMap<SupCode, SupCodeViewDTO>().ReverseMap();
            CreateMap<CityCode, CityCodeDTO>().ReverseMap();
            CreateMap<PlaceCode, PlaceCodeDTO>().ReverseMap();
            CreateMap<CustCode, CustCodeViewDTO>().ReverseMap();
            CreateMap<CustCode, CustCodeEditAddDTO>().ReverseMap();
            CreateMap<StockEmp, StockEmpViewDTO>().ReverseMap();
            CreateMap<StockEmp, StockEmpEditAddDTO>().ReverseMap();
            CreateMap<StockEmpFlag, StockEmpFlagViewDTO>().ReverseMap();
            CreateMap<TransMaster, TransMasterViewDTO>().ReverseMap();
            CreateMap<TransMaster, TransMasterEditAddDTO>().ReverseMap();
            CreateMap<TransMaster, TransMaster>().ReverseMap();
            CreateMap<TransDtl, TransDtl>().ReverseMap();
            CreateMap<TransDtl, TransDtlDTO>().ReverseMap();
            CreateMap<TransDtlDTO, TransDtlDTO>().ReverseMap();
            CreateMap<TransDtl, TransDtlWithPriceDTO>().ReverseMap();
            CreateMap<TransDtlWithPriceDTO, TransDtlWithPriceDTO>().ReverseMap();
            CreateMap<TransDtlWithPriceDTO, TransMaster>().ReverseMap();
            CreateMap<TransDtlDTO, TransMaster>().ReverseMap();
            CreateMap<TransDtl, TransMaster>().ReverseMap();
            CreateMap<ItemBatch, ItemBatchHistory>().ReverseMap();
            CreateMap<ItmReorder, ItmReorderViewDTO>().ReverseMap();
            CreateMap<ItmReorder, ItmReorderEditDTO>().ReverseMap();
            CreateMap<StentDes, StentDesDTO>().ReverseMap();
            CreateMap<MainItemStockDTO, MainItemStockDTO>().ReverseMap();
            CreateMap<SubItemDtl, SubItemDtlDTO>().ReverseMap();
            CreateMap<SubItemDtlDTO, SubItemDtlDTO>().ReverseMap();
            CreateMap<MainItem, SubItemDtlDTO>().ReverseMap();
            CreateMap<SubItem, SubItemDtlDTO>().ReverseMap();
            CreateMap<RegionsViewDTO, Region>().ReverseMap();
            CreateMap<RegionsEditAddDTO, Region>().ReverseMap();
            CreateMap<UserSideViewDTO, UserSide>().ReverseMap();
            CreateMap<UserSideEditAddDTO, UserSide>().ReverseMap();
            CreateMap<StockBalanceViewDTO, Stkbalance>().ReverseMap();
            CreateMap<ExpenseDataAddEditDTO, ExpenseData>().ReverseMap();
            ////////// System ////////////////////
            CreateMap<EisPosting, EisPostingViewDTO>().ReverseMap();
            CreateMap<EisPosting, EisPostingEditAddDTO>().ReverseMap();
            ////////// Accounts ////////////////////
            CreateMap<AccMainCode, AccMainCodeDTO>().ReverseMap();
            CreateMap<AccMainCode, AccMainCodeEditAddDTO>().ReverseMap();
            CreateMap<AccMainCodeEditAddDTO, AccMainCodeDTO>().ReverseMap();
            CreateMap<AccSubCode, AccSubCodeDTO>().ReverseMap();
            CreateMap<AccMainCodeDtl, AccMainCodeDtlDTO>().ReverseMap();
            CreateMap<AccSubCodeEdit, AccSubCodeEditDTO>().ReverseMap();
            CreateMap<JournalTypeDTO, JournalType>().ReverseMap();
            CreateMap<UserJournalTypeDTO, UserJournalType>().ReverseMap();
            CreateMap<AccTransMasterViewDTO, AccTransMaster>().ReverseMap();
            CreateMap<AccTransMasterEditAddDTO, AccTransMaster>().ReverseMap();
            CreateMap<AccTransDetailViewDTO, AccTransDetail>().ReverseMap();
            CreateMap<AccTransDetailEditAddDTO, AccTransDetail>().ReverseMap();
            CreateMap<CostCenterViewDTO, CostCenter>().ReverseMap();
            CreateMap<CostCenterEditAddDTO, CostCenter>().ReverseMap();
            CreateMap<AccStartBalViewDTO, AccStartBal>().ReverseMap();
            CreateMap<AccStartBalEditAddDTO, AccStartBal>().ReverseMap();
            ////////// Treasury ////////////////////
            CreateMap<TreasuryNameViewDTO, SafeName>().ReverseMap();
            CreateMap<TreasuryNameEditAddDTO, SafeName>().ReverseMap();
            CreateMap<AccSafeCashViewDTO, AccSafeCash>().ReverseMap();
            CreateMap<AccSafeCashEditAddDTO, AccSafeCash>().ReverseMap();
            CreateMap<AccSafeCheckViewDTO, AccSafeCheck>().ReverseMap();
            CreateMap<AccSafeCheckEditAddDTO, AccSafeCheck>().ReverseMap();
            CreateMap<CustCollectionsDiscountViewDTO, CustCollectionsDiscount>().ReverseMap();
            CreateMap<CustCollectionsDiscountEditAddDTO, CustCollectionsDiscount>().ReverseMap();
            CreateMap<UserCashNoViewDTO, UserCashNo>().ReverseMap();
            CreateMap<UserCashNoEditAddDTO, UserCashNo>().ReverseMap();
            CreateMap<BankDataDTO, BankData>().ReverseMap();
            CreateMap<AccGlobalDefDTO, AccGlobalDef>().ReverseMap();
            CreateMap<GTablelDTO, GTable>().ReverseMap();
            CreateMap<CashTreasuryDataDTO, AccSafeCash>().ReverseMap();
            ////////// PatRecord ////////////////////
            CreateMap<CoronaryAngiographyReport, CoronaryDTO>().ReverseMap();
            CreateMap<PastHistory, PastHistoryDTO>().ReverseMap();
            CreateMap<EcgAndEcho, EcgAndEchoDTO>().ReverseMap();
            CreateMap<Echo, EchoDTO>().ReverseMap();
            CreateMap<DischargeSummery, DischargeDTO>().ReverseMap();
            CreateMap<DailyFollowUpDTO, DailyFollowUpCcuChant>().ReverseMap();
            CreateMap<HistoryExaminationDTO, HistoryExamination>().ReverseMap();
            CreateMap<LabReportDTO, LabReport>().ReverseMap();
            CreateMap<MedicationDTO, MedicationAtCcu>().ReverseMap();
            CreateMap<PciReportDTO, PciReport>().ReverseMap();

            //************** CUSTOM MAP ****************//
            CreateMap<ClinicTransRequestDTO, ClinicTransEditAddDTO>()
          .ForMember(dest => dest.ExYear, opt => opt.Ignore()) // Not present in source
          .ForMember(dest => dest.ExInvoiceNo, opt => opt.Ignore()) // Not present in source
          .ForMember(dest => dest.ServDesc, opt => opt.Ignore())
          .ForMember(dest => dest.DrDesc, opt => opt.Ignore())
          .ForMember(dest => dest.DrCode, opt => opt.Ignore())
          .ForMember(dest => dest.DrValPat, opt => opt.Ignore())
          .ForMember(dest => dest.HoValPat, opt => opt.Ignore())
          .ForMember(dest => dest.MainId, opt => opt.Ignore())
          .ForMember(dest => dest.SubId, opt => opt.Ignore())
          .ForMember(dest => dest.ClinicId, opt => opt.MapFrom(src => (int?)src.ClinicId))
          .ForMember(dest => dest.SClinicId, opt => opt.MapFrom(src => (int?)src.SClinicId))
          .ForMember(dest => dest.ServId, opt => opt.MapFrom(src => (int?)src.ServId))
          .ForMember(dest => dest.DrSend, opt => opt.MapFrom(src => (int?)src.DrSendId))
          .ForMember(dest => dest.ValueService, opt => opt.MapFrom(src => (decimal?)src.ValueService))
          .ForMember(dest => dest.StockCode, opt => opt.MapFrom(src => src.StockId))
          // Map optional fields
          .ForMember(dest => dest.PatId, opt => opt.Ignore())
          .ForMember(dest => dest.CompId, opt => opt.Ignore())
          .ForMember(dest => dest.CompIdDtl, opt => opt.Ignore())
          .ForMember(dest => dest.SendTo, opt => opt.Ignore())
          .ForMember(dest => dest.SendFr, opt => opt.Ignore())
          .ForMember(dest => dest.MainInvNo, opt => opt.Ignore())
          .ForMember(dest => dest.SessionNo, opt => opt.Ignore())
          .ForMember(dest => dest.Flag, opt => opt.Ignore())
          .ForMember(dest => dest.Counter, opt => opt.Ignore())
          .ForMember(dest => dest.MasterId, opt => opt.MapFrom(src => (int?)src.MasterId))
          .ForMember(dest => dest.SubCode, opt => opt.Ignore())
          .ForMember(dest => dest.MainCode, opt => opt.Ignore());
        }
    }
}
