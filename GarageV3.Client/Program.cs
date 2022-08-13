using GarageV3.AutoMapper;
using GarageV3.Client.Extensions;
using GarageV3.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GarageDBContext>(options =>
options.UseSqlServer(connectionString));


builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();



// Changes are made in appSettings.json concerning keys around seeddata
if (builder.Configuration.GetValue<bool>("IsSeedDatabase"))
{
    await app.AddSeedData(builder.Configuration.GetValue<int>("NumberOfSeedItems"));
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vehicles}/{action=Index}/{id?}");

app.Run();
