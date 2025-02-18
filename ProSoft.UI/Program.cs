using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ProSoft.Core.AutoMapper;
using ProSoft.Core.Repositories;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.Core.Repositories.Medical.Analysis;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.Core.Repositories.Medical.HospitalPatData.Reports;
using ProSoft.Core.Repositories.MedicalRecords;
using ProSoft.Core.Repositories.Shared;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.Core.Repositories.Stocks.Reports;
using ProSoft.Core.Repositories.Treasury;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.IRepositories.Medical.Analysis;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData.Reports;
using ProSoft.EF.IRepositories.MedicalRecords;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Stocks;
using ProSoft.UI.Areas.Accounts;
using ProSoft.UI.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//Configration fo context connectio string
//var sqlServerConnection = builder.Configuration.GetConnectionString("SqlServerConnection") ?? throw new InvalidOperationException("Connection string 'Sql Server Connection' not found.");
var dbProvider = builder.Configuration["DatabaseProvider"];
var connectionString = builder.Configuration.GetConnectionString($"{dbProvider}Connection");
if (string.IsNullOrEmpty(dbProvider) || string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database provider or connection string not configured.");
}

builder.Services.AddScoped(sp =>
{
    return new AppDbContext(connectionString, dbProvider);
});
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(sqlServerConnection));

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseOracle(oracleConnection));

// Register for AutoMapper service
builder.Services.AddAutoMapper(typeof(AutoMap)); //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//JsonConvert.DefaultSettings = () => new JsonSerializerSettings
//{
  
//    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
//};

// Add services to the container.
//builder.Services.AddControllersWithViews().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.PropertyNamingPolicy = null; // This enforces PascalCase

//    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//});

// Register for Identity Package
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Register for Authentications Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
        
        options.SlidingExpiration = true;
        options.LoginPath = "/Account/Login";
    });

builder.Services.ConfigureOptions<ConfigureSecurityStampOptions>();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(1000000);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});
builder.Services.AddFastReport();
// General Response Register

builder.Services.AddScoped<AuditScope>();
builder.Services.AddScoped<IGeneralResponse<StockEmpFlag>, GeneralResponse<StockEmpFlag>>();

//Register for Interfaces
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IPriceRepo, PriceRepo>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

/////////////////
builder.Services.AddScoped<ILabUnitRepo, LabUnitRepo>();
builder.Services.AddScoped<ISubRepo, SubRepo>();
builder.Services.AddScoped<IItemAnalysisRepo, ItemAnalysisRepo>();
/////////////////

builder.Services.AddScoped<IReceiptInquiryRepo, ReceiptInquiryRepo>();
builder.Services.AddScoped<IDoctorsAppointmentsInquiryRepo, DoctorsAppointmentsInquiryRepo>();
builder.Services.AddScoped<IClinicsAppointmentsInquiryRepo, ClinicsAppointmentsInquiryRepo>();
builder.Services.AddScoped<IDrDiscountRepo, DrDiscountRepo>();
builder.Services.AddScoped<IDegreeCodeRepo, DegreeCodeRepo>();
builder.Services.AddScoped<IRoomCodeRepo, RoomCodeRepo>();
builder.Services.AddScoped<IBedCodeRepo, BedCodeRepo>();
builder.Services.AddScoped<IStentsRepo, StentsRepo>();
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
builder.Services.AddScoped<ICheckupClinicRepo, CheckupClinicRepo>();
builder.Services.AddScoped<IEisSectionTypeRepo, EisSectionTypeRepo>();
builder.Services.AddScoped<IRegionRepo, RegionRepo>();
///////////////// Stock /////////////

builder.Services.AddScoped<IPostSuppliesToStocksRepo, PostSuppliesToStocksRepo>();
builder.Services.AddScoped<ITotalCustomerTransactionReportRepo, TotalCustomerTransactionReportRepo>();
builder.Services.AddScoped<ICustomerTransactionReportRepo, CustomerTransactionReportRepo>();
builder.Services.AddScoped<IAnalyticalCustomerTransactionReportRepo, AnalyticalCustomerTransactionReportRepo>();
builder.Services.AddScoped<IReportTransferAndReceiptTransactionRepo, ReportTransferAndReceiptTransactionRepo>();
builder.Services.AddScoped<ITotalItemCardsRepo, TotalItemCardRepo>();
builder.Services.AddScoped<IRequestLimitItemsReportRepo, RequestLimitItemsReportRepo>();
builder.Services.AddScoped<IStockTypeRepo, StockTypeRepo>();
builder.Services.AddScoped<IStockBalanceReportRepo, StockBalanceReportRepo>();
builder.Services.AddScoped<ITransactionReportRepo, TransactionReportRepo>();
builder.Services.AddScoped<IItemsCustPriceRepo, ItemsCustPriceRepo>();
builder.Services.AddScoped<IAdjectiveCustRepo, AdjectiveCustRepo>();
builder.Services.AddScoped<IStockEmpFlagRepo, StockEmpFlagRepo>();
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
builder.Services.AddScoped<IMainItemRepo, MainItemRepo>();
builder.Services.AddScoped<ISubItemRepo, SubItemRepo>();
builder.Services.AddScoped<ISubItemDtlRepo, SubItemDtlRepo>();
builder.Services.AddScoped<IDepartmentsWithSectionsRepo, DepartmentsWithSectionsRepo>();
builder.Services.AddScoped<IOpenColseTransactionRepo, OpenColseTransactionRepo>();
builder.Services.AddScoped<IReportQuantityClassCardRepo, ReportQuantityClassCardRepo>();
builder.Services.AddScoped<IUserSideRepo, UserSideRepo>();
builder.Services.AddScoped<IUsersGroupRepo, UsersGroupRepo>();
builder.Services.AddScoped<IStockBalanceRepo, StockBalanceRepo>();
builder.Services.AddScoped<IItemUnitsRepo, ItemUnitsRepo>();
builder.Services.AddScoped<IExpenseDataRepo, ExpenseDataRepo>();
///////////////// System ///////////////
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
builder.Services.AddScoped<IAccMainCodeDtlRepo, AccMainCodeDtlRepo>();
builder.Services.AddScoped<IAccSubCodeEditRepo, AccSubCodeEditRepo>();
builder.Services.AddScoped<IReportAssistantProfessorRepo, ReportAssistantProfessorRepo>();
builder.Services.AddScoped<IAmericanDailyRepo, AmericanDailyRepo>();
builder.Services.AddScoped<IReportExpenseAnalysisRepo, ReportExpenseAnalysisRepo>();
builder.Services.AddScoped<IReportGeneralProfessorFacilityRepo, ReportGeneralProfessorFacilityRepo>();
builder.Services.AddScoped<ICancelJournalVoucherRepo, CancelJournalVoucherRepo>();
builder.Services.AddScoped<IMonthlyClosingRepo, MonthlyClosingRepo>();
builder.Services.AddScoped<IReportDailyAssistanceRepo, ReportDailyAssistanceRepo>();
builder.Services.AddScoped<IReportTransactionAccountJournalRepo, ReportTransactionAccountJournalRepo>();
builder.Services.AddScoped<IReportReviewJournalVouchersRepo, ReportReviewJournalVouchersRepo>();
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
//app.UseMiddleware<AuditLogMiddleware>();

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
//app.UseSession(); // Ensure session middleware is added

// for using browser language
app.UseRequestCulture();

app.MapControllerRoute(
  name: "area",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
app.UseFastReport();
app.Run();
