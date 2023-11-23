using Hotel_Booking.Data;
using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Hotel_Booking.Repo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.AspNetCore.Identity;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;
using System;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        
        builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddRazorPages();

        builder.Services.AddScoped<IAdminInitializer, AdminInitializer>();
        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.AddScoped<IRoomRepo, RoomService>();
        builder.Services.AddScoped<IHotelRepo, HotelService>();
        builder.Services.AddScoped<ICustomer_RoomRepo,Customer_RoomService>(); 
        builder.Services.AddScoped<ICustomer_PaymentRepo, Customer_PaymentService>();




        // Register UserManager and SignInManager with specific type
        builder.Services.AddScoped<UserManager<AppUser>>();
        builder.Services.AddScoped<SignInManager<AppUser>>();


        // Build the app
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
        DataSedding();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapAreaControllerRoute(
        name: "Inst",
        areaName: "Admin",
        pattern: "{controller}/{action}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");



        app.MapAreaControllerRoute(
         name: "default",
         areaName: "Identity",
         pattern: "{controller=Account}/{action=Login}");


        app.MapRazorPages();

        app.Run();

        void DataSedding()
        {
            using (var scope = app.Services.CreateScope())
            {
                var Intializer = scope.ServiceProvider.GetRequiredService<IAdminInitializer>();
                Intializer.Initialize();
            }
        }
    }
}