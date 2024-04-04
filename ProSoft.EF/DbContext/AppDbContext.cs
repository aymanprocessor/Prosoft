using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Medical.Analysis;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DocSubDtl> DocSubDtls { get; set; }


        public DbSet<MainClinic> MainClinics { get; set; }

        public DbSet<MainItem> MainItems { get; set; }

        public DbSet<Pat> Pats { get; set; }

        public DbSet<PatAdmission> PatAdmissions { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<ServiceClinic> ServiceClinics { get; set; }

        public DbSet<SubClinic> SubClinics { get; set; }

        public DbSet<SubItem> SubItems { get; set; }
        public DbSet<DrDegree> DrDegrees { get; set; }
        public DbSet<EisSectionType> EisSectionTypes { get; set; }

        ////////////////////
        // Stocks //
        public DbSet<KindStore> KindStores { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<JournalType> JournalTypes { get; set; }
    }
}
