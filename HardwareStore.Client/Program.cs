using HardwareStore.Data;
using HardwareStore.Domain.Extensions;
using HardwareStore.Domain.Settings;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
var encryptionSettings = new EncryptionSettings();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Configuration.Bind(nameof(encryptionSettings), encryptionSettings);
builder.Services.AddSingleton(encryptionSettings);
builder.Services
    .RegisterDatabaseSources(builder.Configuration)
    .AddDomainServices(builder.Configuration)
    .AddDataRepositories(builder.Configuration)
    .AddMigrations(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
