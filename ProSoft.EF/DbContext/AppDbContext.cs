using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.Analysis;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.MedicalRecords;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.EF.DbContext
{
    public class AppDbContext :IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>(option =>
            {
                option.ToTable(name: "USERS");
            });
            builder.Entity<AppUser>()
               .Property(u => u.UserName)
               .HasColumnName("USER_NAME");
        }
        // Shared //
        public DbSet<NationalityEi> NationalityEis { get; set; }
        /////
        // Analysis //
        public DbSet<Itemanalysis> Itemanalyses { get; set; }
        public DbSet<MedicalAnalysisMain> MedicalAnalysisMains { get; set; }
        public DbSet<MedicalAnalysisSub> MedicalAnalysisSubs { get; set; }
        public DbSet<LabUnit> LabUnits { get; set; }
        public DbSet<Analdetail> Analdetails { get; set; }
        /////
        // Hospital Patient data //
        public DbSet<ClassificationCust> ClassificationCusts { get; set; }

        public DbSet<ClinicTran> ClinicTrans { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<CompanyDtl> CompanyDtls { get; set; }

        public DbSet<CompanyGroup> CompanyGroups { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DocSubDtl> DocSubDtls { get; set; }


        public DbSet<MainClinic> MainClinics { get; set; }

        public DbSet<MainItem> MainItems { get; set; }

        public DbSet<Pat> Pats { get; set; }

        public DbSet<PatAdmission> PatAdmissions { get; set; }

        public DbSet<Deposit> Deposits { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<ServiceClinic> ServiceClinics { get; set; }

        public DbSet<SubClinic> SubClinics { get; set; }

        public DbSet<SubItem> SubItems { get; set; }
        public DbSet<DrDegree> DrDegrees { get; set; }
        public DbSet<DoctorsPercent> DoctorsPercents { get; set; }
        public DbSet<EisSectionType> EisSectionTypes { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }

        public DbSet<PriceListDetail> PriceListDetails { get; set; }

        ////////////////////
        // Medical Records //
        public DbSet<CoronaryAngiographyReport> CoronaryAngiographyReports { get; set; }
        public DbSet<DailyFollowUpCcuChant> DailyFollowUpCcuChants { get; set; }
        public DbSet<DischargeSummery> DischargeSummerys { get; set; }
        public DbSet<EcgAndEcho> EcgAndEchos { get; set; }
        public DbSet<Echo> Echos { get; set; }
        public DbSet<HistoryExamination> HistoryExaminations { get; set; }
        public DbSet<LabReport> LabReports { get; set; }
        public DbSet<MedicationAtCcu> MedicationAtCcus { get; set; }
        public DbSet<PastHistory> PastHistorys { get; set; }
        public DbSet<DischargeSummery> DischargeSummeries { get; set; }
        public DbSet<EcgAndEcho> EcgAndEchoes { get; set; }
        public DbSet<Echo> Echoes { get; set; }
        public DbSet<PastHistory> PastHistories { get; set; }
        public DbSet<PciReport> PciReports { get; set; }


        ////////////////////
        // Stocks //
        public DbSet<KindStore> KindStores { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<JournalType> JournalTypes { get; set; }
        public DbSet<Side> Sides { get; set; }
        public DbSet<UnitCode> UnitCodes { get; set; }
        public DbSet<Sections2> Sections2s { get; set; }
        public DbSet<GeneralCode> GeneralCodes { get; set; }
        public DbSet<StoreTran> StoreTrans { get; set; }
        public DbSet<UserTranss> UserTransactions { get; set; }
        public DbSet<CityCode> CityCodes { get; set; }
        public DbSet<PlaceCode> PlaceCodes { get; set; }
        public DbSet<SupCode> SupCodes { get; set; }
        public DbSet<CustCode> CustCodes { get; set; }
        public DbSet<AdjectiveCust> AdjectiveCusts { get; set; }
        public DbSet<StockEmp> StockEmps { get; set; }
        public DbSet<TransMaster> TransMasters { get; set; }

        ////////////////////
        // Accounts //
        public DbSet<AccMainCode> AccMainCodes { get; set; }
        public DbSet<AccSubCode> AccSubCodes { get; set; }
        ////////////////////
        // System //
        public DbSet<EisPosting> EisPostings { get; set; }

        ////////////////////
        // Treasury //
        public DbSet<SafeName> SafeNames { get; set; }
        public DbSet<GTable> gTables { get; set; }
        public DbSet<AccGlobalDef> accGlobalDefs { get; set; }
        public DbSet<AccSafeCash> AccSafeCashes { get; set; }
        public DbSet<CustCollectionsDiscount> custCollectionsDiscounts { get; set; }
        public DbSet<UserCashNo> userCashNos { get; set; }
        public DbSet<AccSafeCheck> AccSafeChecks { get; set; }
        public DbSet<BankData> bankDatas { get; set; }
    }
}
