using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using MudBlazor.Services;
using NotyAI.Notes;
using NotyAI.OpenAI;
using NotyAI.Photos;
using Plugin.Maui.Audio;

namespace NotyAI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.AddAudio();
        builder.Services.AddMudServices();

        builder.Services.AddSingleton<CurrentPhoto>();
        builder.Services.AddSingleton<Note>();

        builder.Services.AddSingleton<IPhotoService, PhotoService>();
        builder.Services.AddOpenAiService();
        
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}