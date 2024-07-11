using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.Core.AutoMapper;
using ProSoft.EF.Models;
using System;
using ProSoft.EF.IRepositories;
using ProSoft.Core.Repositories;
using ProSoft.EF.IRepositories.Medical.Analysis;
using ProSoft.Core.Repositories.Medical.Analysis;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using Microsoft.Extensions.Localization;
using ProSoft.UI.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.Core.Repositories.Shared;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.Core.Repositories.Treasury;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.EF.IRepositories.MedicalRecords;
using ProSoft.Core.Repositories.MedicalRecords;


var builder = WebApplication.CreateBuilder(args);

//Configration fo context connectio string
var connectionString = builder.Configuration.GetConnectionString("MyConnection") ?? throw new InvalidOperationException("Connection string 'MyConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register for AutoMapper service
builder.Services.AddAutoMapper(typeof(AutoMap)); //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register for Identity Package
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Register for Authentications Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

//Register for Interfaces
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();
builder.Services.AddScoped<IMainRepo, MainRepo>();
/////////////////
builder.Services.AddScoped<ILabUnitRepo, LabUnitRepo>();
builder.Services.AddScoped<ISubRepo, SubRepo>();
builder.Services.AddScoped<IItemAnalysisRepo, ItemAnalysisRepo>();
/////////////////
builder.Services.AddScoped<IPatientRepo, PatientRepo>();
builder.Services.AddScoped<IPatAdmissionRepo, PatAdmissionRepo>();
builder.Services.AddScoped<IClinicTransRepo, ClinicTransRepo>();
builder.Services.AddScoped<IAnalysisDetailRepo, AnalysisDetailRepo>();
builder.Services.AddScoped<INationalityRepo, NationalityRepo>();
builder.Services.AddScoped<IDrDegreeRepo, DrDegreeRepo>();
builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
builder.Services.AddScoped<IDocSubDtlRepo, DocSubDtlRepo>();
builder.Services.AddScoped<IMainClinicRepo, MainClinicRepo>();
builder.Services.AddScoped<ISubClinicRepo, SubClinicRepo>();
builder.Services.AddScoped<IServiceClinicRepo, ServiceClinicRepo>();
builder.Services.AddScoped<IDoctorsPersentageRepo, DoctorsPersentageRepo>();
builder.Services.AddScoped<IPriceListRepo, PriceListRepo>();
builder.Services.AddScoped<ITermsPriceListRepo, TermsPriceListRepo>();
builder.Services.AddScoped<ICompanyGroupRepo, CompanyGroupRepo>();
builder.Services.AddScoped<ICompanyRepo, CompanyRepo>();
builder.Services.AddScoped<ICompanyDtlRepo, CompanyDtlRepo>();
builder.Services.AddScoped<IDepositVisitRepo, DepositVisitRepo>();
builder.Services.AddScoped<IkinshipRepo, kinshipRepo>();
builder.Services.AddScoped<IDrTimeSheetRepo, DrTimeSheetRepo>();
builder.Services.AddScoped<IUsersSectionRepo, UsersSectionRepo>();
builder.Services.AddScoped<IReportDoctorFeesRepo, ReportDoctorFeesRepo>();
///////////////// Stock /////////////
builder.Services.AddScoped<IStockTypeRepo, StockTypeRepo>();
builder.Services.AddScoped<IBranchRepo, BranchRepo>();
builder.Services.AddScoped<IStockRepo, StockRepo>();
builder.Services.AddScoped<ISideRepo, SideRepo>();
builder.Services.AddScoped<IUnitCodeRepo, UnitCodeRepo>();
builder.Services.AddScoped<IClassificationCustRepo, ClassificationCustRepo>();
builder.Services.AddScoped<ISectionRepo, SectionRepo>();
builder.Services.AddScoped<IGeneralTableRepo, GeneralTableRepo>();
builder.Services.AddScoped<IUserTransRepo, UserTransRepo>();
builder.Services.AddScoped<ITransTypeRepo, TransTypeRepo>();
builder.Services.AddScoped<ISupplierRepo, SupplierRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IStockEmpRepo, StockEmpRepo>();
builder.Services.AddScoped<ITransMasterRepo, TransMasterRepo>();
builder.Services.AddScoped<ITransDtlRepo, TransDtlRepo>();
builder.Services.AddScoped<IOrderLimitRepo, OrderLimitRepo>();
///////////////// System /////////////
builder.Services.AddScoped<IEisPostingRepo, EisPostingRepo>();
///////////////// Treasury /////////////
builder.Services.AddScoped<ITreasuryNameRepo, TreasuryNameRepo>();
builder.Services.AddScoped<IJournalTypeRepo, JournalTypeRepo>();
builder.Services.AddScoped<IAccSafeCashRepo, AccSafeCashRepo>();
builder.Services.AddScoped<IAccSafeCheckRepo, AccSafeCheckRepo>();
builder.Services.AddScoped<ICustCollectionsDiscountRepo, CustCollectionsDiscountRepo>();
builder.Services.AddScoped<IUserCashNoRepo, UserCashNoRepo>();
builder.Services.AddScoped<IBankDataRepo, BankDataRepo>();
builder.Services.AddScoped<IAccGlobalDefRepo, AccGlobalDefRepo>();
builder.Services.AddScoped<IGTableRepo, GTableRepo>();
builder.Services.AddScoped<IReportCashAndChecksRepo, ReportCashAndChecksRepo>();
////////// Accounts ////////////////////
builder.Services.AddScoped<IUserJournalTypeRepo, UserJournalTypeRepo>();
builder.Services.AddScoped<IAccTransMasterRepo, AccTransMasterRepo>();
builder.Services.AddScoped<IAccTransDetailRepo, AccTransDetailRepo>();
builder.Services.AddScoped<ICostCenterRepo, CostCenterRepo>();
builder.Services.AddScoped<IReportFromToVoucherRepo, ReportFromToVoucherRepo>();
builder.Services.AddScoped<IAccStartBalRepo, AccStartBalRepo>();
builder.Services.AddScoped<IAccMainCodeRepo, AccMainCodeRepo>();
builder.Services.AddScoped<IAccSubCodeRepo, AccSubCodeRepo>();
////////// PatRecord ////////////////////
builder.Services.AddScoped<ICoronaryRepo, CoronaryRepo>();
builder.Services.AddScoped<IPastHistoryRepo, PastHistoryRepo>();
builder.Services.AddScoped<IEcgAndEchoRepo, EcgAndEchoRepo>();
builder.Services.AddScoped<IEchoRepo, EchoRepo>();
builder.Services.AddScoped<IDischargeRepo, DischargeRepo>();
builder.Services.AddScoped<IDailyFollowUpRepo, DailyFollowUpRepo>();
builder.Services.AddScoped<IHistoryExaminationRepo, HistoryExaminationRepo>();
builder.Services.AddScoped<ILabReportRepo, LabReportRepo>();
builder.Services.AddScoped<IMedicationRepo, MedicationRepo>();
builder.Services.AddScoped<IPciReportRepo, PciReportRepo>();

///For Localization
builder.Services.AddLocalization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(JsonStringLocalizerFactory));
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCulture = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG"),
    };
    //options.DefaultRequestCulture = new RequestCulture(culture: supportedCulture[0], uiCulture: supportedCulture[0]);
    options.SupportedCultures= supportedCulture;
    options.SupportedUICultures= supportedCulture;
});
/////////////////


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

///For Localization
var supportedCulture = new[] { "en-US", "ar-EG" };
var localizationsOptions =new RequestLocalizationOptions()
    //.SetDefaultCulture(supportedCulture[0])
    .AddSupportedCultures(supportedCulture)
    .AddSupportedUICultures(supportedCulture);
    
app.UseRequestLocalization(localizationsOptions);
////////////////


app.UseAuthentication();
app.UseAuthorization();

// for using browser language
app.UseRequestCulture();

app.MapControllerRoute(
  name: "area",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
