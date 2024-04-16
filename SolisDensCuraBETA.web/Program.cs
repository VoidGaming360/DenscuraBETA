using SolisDensCuraBETA.repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SolisDensCuraBETA.utilities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.repositories.Implementation;
using Microsoft.AspNetCore.Identity.UI.Services;
using SolisDensCuraBETA.services;
using SolisDensCuraBETA.model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configure identity options here if needed
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddTransient<IClinic, ClinicService>();
builder.Services.AddTransient<IDentistService, DentistService>(); // Fixed registration
builder.Services.AddTransient<IRoom, RoomService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
builder.Services.AddTransient<ISupplies, SuppliesService>();
builder.Services.AddRazorPages();

builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<DentistService>();

// Add authentication services
builder.Services.AddAuthentication()
    .AddCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
});

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
app.UseAuthentication();
app.UseAuthorization();




app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "adminArea",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

// Data seeding should be performed after authentication and authorization middleware
DataSeeding(app);

app.Run();

void DataSeeding(WebApplication app) // Pass app instance to DataSeeding
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider
            .GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}

