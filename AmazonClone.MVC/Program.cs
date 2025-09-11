using AmazonClone.MVC.Models;
using AmazonClone.MVC.Services;
using AmazonClone.MVC.Services.Interfaces;
using AmazonClone.MVC.Validators;
using FluentValidation;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IValidator<User>, RegisterModelValidator>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/logMVC-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Add session services HERE (before Build())
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpClient<ICartService, CartService>();
builder.Services.AddHttpClient<IUserService, UserService>();
builder.Services.AddHttpClient<ICategoryService, CategoryService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

app.UseCors("AllowAll");
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();