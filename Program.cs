using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ProjectsMecsaSPA.Areas.Identity;
using ProjectsMecsaSPA.Data;
using ProjectsMecsaSPA.Hubs;
using ProjectsMecsaSPA.Model;
using ProjectsMecsaSPA.Services;
using ProjectsMecsaSPA.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var UsersConnection = builder.Configuration.GetConnectionString("UsersConnection");
var ProjectsConnection = builder.Configuration.GetConnectionString("ProjectsConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
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

var app = builder.Build();

var telegramService = new TelegramService();    


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
