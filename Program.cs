using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProjectsMecsaSPA.Areas.Identity;
using ProjectsMecsaSPA.Data;
using ProjectsMecsaSPA.Hubs;
using ProjectsMecsaSPA.Model;
using ProjectsMecsaSPA.Services;
using ProjectsMecsaSPA.Utilities;
using System.Net.NetworkInformation;
using static ProjectsMecsaSPA.Components.Config.AppSettingsConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));



var UsersConnection = builder.Configuration.GetConnectionString("UsersConnection");
var ProjectsConnection = builder.Configuration.GetConnectionString("ProjectsConnection");

#region Limites
builder.Services.Configure<HubOptions>(options =>
{
    options.MaximumReceiveMessageSize = 20 * 1024 * 1024; // 20 MB
});

builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = 20 * 1024 * 1024; // 20 MB
});

#endregion


#region API Connections
builder.Services.AddHttpClient<APICarServices>(client =>
{
    client.BaseAddress = new Uri("https://checarsv2.stevengazo.co.cr/api/");
});

#endregion


builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(UsersConnection));

builder.Services.AddDbContextFactory<ProjectsDBContext>(options =>
    options.UseSqlServer(ProjectsConnection));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<UserIdentityEx>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddScoped<ChatService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<UserIdentityEx>>();

builder.Services.AddBlazorBootstrap();
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();
// Telegram
// Cargar las configuraciones desde appsettings.json
//builder.Services.Configure<TelegramSettings>(builder.Configuration.GetSection("TelegramSettings"));
//builder.Services.AddHostedService<TelegramService>();
// Central Bank Service
builder.Services.AddHttpClient<Bitrix24ClientService>();
builder.Services.AddHttpClient<CentralBankService>();
builder.Services.AddSingleton<FileStorageService>();


var app = builder.Build();


#region Databases
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<ProjectsDBContext>();
        if (!db.Database.CanConnect())
        {
            db.Database.Migrate();
        }
        
        Console.WriteLine(nameof(ProjectsDBContext) + "Created");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error in the Database Connection. {e.Message}");
        Console.WriteLine($"Error in the Database Connection. {e.InnerException}");
        throw;
    }
}
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (!db.Database.CanConnect())
        {
            db.Database.Migrate();
        }
     
        Console.WriteLine(nameof(ApplicationDbContext) + "Created");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error in the Database Connection. {e.Message}");
        Console.WriteLine($"Error in the Database Connection. {e.InnerException}");
        throw;
    }
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


var path = Path.Combine(Directory.GetCurrentDirectory(), "projectsdata");
if (!Directory.Exists(path))
{
    Directory.CreateDirectory(path);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(path),
    RequestPath = "/drive-facturas"
});


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
// Map SignalR Hubs
app.MapHub<ChatHub>("/chathub");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");



app.Run();




