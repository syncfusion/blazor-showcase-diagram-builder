using DiagramBuilder.Components;
using DiagramBuilder.Shared;
using Microsoft.AspNetCore.Localization;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSignalR(e =>
{
    e.MaximumReceiveMessageSize = 102400000;
});

builder.Services.AddSyncfusionBlazor();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<SampleService>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<SfDialogService>();


//Register the Syncfusion locale service to customize the SyncfusionBlazor component locale culture
//builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // Define the list of cultures your app will support
    List<CultureInfo> supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de"),
                    new CultureInfo("fr"),
                    new CultureInfo("ar"),
                    new CultureInfo("zh"),
                };
    // Set the default culture
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();