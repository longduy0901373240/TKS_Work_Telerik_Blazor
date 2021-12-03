using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Utility_6;
using Web_Net_6.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String = builder.Configuration.GetConnectionString("TKS_Thuc_Tap_Data_Conn_String").ToString());
builder.Services.AddSingleton(CConfig.DateTime_Format_String = builder.Configuration.GetValue<string>("DateTime_Format_String"));
builder.Services.AddSingleton(CConfig.Number_Format_String = builder.Configuration.GetValue<string>("Number_Format_String"));
//more code may be present here

builder.Services.AddTelerikBlazor();

//more code may be present here   
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//more code may be present here

//make sure this is present to enable static files from a package
app.UseStaticFiles();
//more code may be present here    
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
