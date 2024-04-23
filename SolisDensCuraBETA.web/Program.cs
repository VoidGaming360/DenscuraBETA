using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories;
using SolisDensCuraBETA.repositories.Implementation;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.services;
using SolisDensCuraBETA.utilities;

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
builder.Services.AddTransient<IDentistService, DentistService>();
builder.Services.AddTransient<IRoom, RoomService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
builder.Services.AddTransient<ISupplies, SuppliesService>();
builder.Services.AddTransient<IAppointment, AppointmentService>();
builder.Services.AddTransient<ITreatmentService, TreatmentService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IChatService, ChatService>();

// Add SignalR services
builder.Services.AddSignalR();

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

// Custom middleware to log user login
app.Use(async (context, next) =>
{
    var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
    var notificationService = context.RequestServices.GetRequiredService<INotificationService>();

    // Check if user is authenticated
    if (context.User.Identity.IsAuthenticated)
    {
        var user = await userManager.GetUserAsync(context.User);
        var userId = user.Id;
        var userName = user.Name;

        // Create notification for user login
        await notificationService.CreateNotificationAsync(userId, $"{userName} has successfully logged in.");
    }

    await next();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notificationHub");
    

    endpoints.MapHub<ChatHub>("/chatHub");
    endpoints.MapRazorPages();

    endpoints.MapControllerRoute(
        name: "adminArea",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


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
