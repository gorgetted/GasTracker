using GasTracker.Backend;
using GasTracker.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
namespace GasTracker {
    public static class MauiProgram {
        public static string GOOGLE_API_KEY;
        public static readonly string deviceId = GetDeviceId();
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            LocalStoarge.CreateFiles();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
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

        public static string GetDeviceId() {
            string deviceId = Preferences.Get("my_device_id", string.Empty);
            if (string.IsNullOrEmpty(deviceId)) {
                deviceId = Guid.NewGuid().ToString();
                Preferences.Set("my_device_id", deviceId);
            }
            return deviceId;
        }
    }
}
