using Microsoft.AspNetCore.Components.WebView.Maui;
using DiagramBuilderMAUI.Data;
using DiagramBuilder.Shared;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

namespace DiagramBuilder;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder			
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddSyncfusionBlazor();
        builder.Services.AddScoped<SfDialogService>();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        builder.Services.AddSingleton<SampleService>();
		return builder.Build();
	}
}
