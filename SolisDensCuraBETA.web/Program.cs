using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories;
using SolisDensCuraBETA.repositories.Implementation;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.services;
using SolisDensCuraBETA.services.Interface;
using SolisDensCuraBETA.utilities;
using System.Security.Claims;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
builder.Services.AddTransient<ICostService, CostService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<ImageOperations>();

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
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Add the session middleware before authentication and authorization middlewares
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// Custom middleware to log user login
app.Use(async (context, next) =>
{
    var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
    var notificationService = context.RequestServices.GetRequiredService<INotificationService>();

    if (context.User.Identity.IsAuthenticated)
    {
        var user = await userManager.GetUserAsync(context.User);
        if (user != null)
        {
            var userId = user.Id;
            var userName = user.Name;

            await notificationService.CreateNotificationAsync(userId, $"{userName} has successfully logged in.");
        }
        else
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogWarning("Authenticated user not found in UserManager. UserId: {UserId}", context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
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

await DataSeeding(app);

app.Run();

async Task DataSeeding(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dbInitializer.InitializeAsync();
}
