using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Calculus;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Calculus;
using ProSoft.EF.Models.Medical.Analysis;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.AutoMapper
{
    public class AutoMap: Profile
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
            CreateMap<MainClinicViewDTO, MainClinic>().ReverseMap();
            CreateMap<MainItemViewDTO, MainItem>().ReverseMap();
            CreateMap<DoctorViewDTO, Doctor>().ReverseMap();
            CreateMap<DoctorEditAddDTO, Doctor>().ReverseMap();
            ///////////////////////////////////////
            CreateMap<SubClinicViewDTO, SubClinic>().ReverseMap();
            CreateMap<SubItemViewDTO, SubItem>().ReverseMap();
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
            CreateMap<CostCenterViewDTO, CostCenter>().ReverseMap();
            CreateMap<EisSectionTypeDTO, EisSectionType>().ReverseMap();
            CreateMap<DoctorPrecentViewDTO, DoctorsPercent>().ReverseMap();
            CreateMap<DoctorPrecentEditAddDTO, DoctorsPercent>().ReverseMap();
            CreateMap<PriceListViewDTO, PriceList>().ReverseMap();
            CreateMap<PriceListEditAddDTO, PriceList>().ReverseMap();
            CreateMap<TermsPriceListViewDTO, PriceListDetail>().ReverseMap();
            CreateMap<TermsPriceListEditAddDTO, PriceListDetail>().ReverseMap();
            CreateMap<CompanyGroupDTO, CompanyGroup>().ReverseMap();
            CreateMap<CompanyViewDTO, Company>().ReverseMap();
            CreateMap<CompanyEditAddDTO, Company>().ReverseMap();
            CreateMap<CompanyDtlEditAddDTO, CompanyDtl>().ReverseMap();
            //////////Stocks////////////////////
            CreateMap<KindStore, KindStoreDTO>().ReverseMap();
            CreateMap<Branch, BranchDTO>().ReverseMap();
            CreateMap<Stock, StockEditAddDTO>().ReverseMap();
            CreateMap<StockViewDTO, Stock>().ReverseMap();
            CreateMap<JournalType, JournalTypeDTO>().ReverseMap();
            CreateMap<Side, SideDTO>().ReverseMap();
            CreateMap<UnitCode, UnitCodeDTO>().ReverseMap();
            CreateMap<Sections2, SectionViewDTO>().ReverseMap();
            CreateMap<Sections2, SectionEditAddDTO>().ReverseMap();
            CreateMap<GeneralCode, PermissionDefViewDTO>().ReverseMap();
            CreateMap<GeneralCode, PermissionDefEditAddDTO>().ReverseMap();
            CreateMap<AppUser, UserTransViewDTO>().ReverseMap();
            CreateMap<UserTranss, UserTransEditAddDTO>().ReverseMap();
            CreateMap<StoreTran, StoreTransDTO>().ReverseMap();
            CreateMap<UserTranss, UserTransEditAddDTO>().ReverseMap();
            CreateMap<SupCode, SupCodeEditAddDTO>().ReverseMap();
            CreateMap<CityCode, CityCodeDTO>().ReverseMap();
            CreateMap<PlaceCode, PlaceCodeDTO>().ReverseMap();
            ////////// System ////////////////////
            CreateMap<EisPosting, EisPostingViewDTO>().ReverseMap();
            CreateMap<EisPosting, EisPostingEditAddDTO>().ReverseMap();
            ////////// Calculus ////////////////////
            CreateMap<AccMainCode, AccMainCodeDTO>().ReverseMap();
            CreateMap<AccSubCode, AccSubCodeDTO>().ReverseMap();
        }
    }
}
