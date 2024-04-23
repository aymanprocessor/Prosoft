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
/////////////////Stock/////////////
builder.Services.AddScoped<IStockTypeRepo, StockTypeRepo>();
builder.Services.AddScoped<IBranchRepo, BranchRepo>();
builder.Services.AddScoped<IStockRepo, StockRepo>();
builder.Services.AddScoped<ISideRepo, SideRepo>();
builder.Services.AddScoped<IUnitCodeRepo, UnitCodeRepo>();
builder.Services.AddScoped<IClassificationCustRepo, ClassificationCustRepo>();
builder.Services.AddScoped<ISectionRepo, SectionRepo>();
builder.Services.AddScoped<IGeneralTableRepo, GeneralTableRepo>();
builder.Services.AddScoped<IUserTransRepo, UserTransRepo>();

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
