using Microsoft.AspNetCore.Components.WebView.Maui;
using DiagramBuilderMAUI.Data;
using DiagramBuilderMAUI.Shared;
using Syncfusion.Blazor;

namespace DiagramBuilderMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.RegisterBlazorMauiWebView()
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddBlazorWebView();
		builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = false; });
		builder.Services.AddScoped<SampleService>();
		return builder.Build();
	}
}
