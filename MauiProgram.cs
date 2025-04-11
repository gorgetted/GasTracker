using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace GasTracker
{
    public static class MauiProgram
    {
        public static string GOOGLE_API_KEY;
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .Configuration.AddUserSecrets<App>();
            GOOGLE_API_KEY = builder.Configuration["GOOGLE_PLACES_API_KEY"];
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
