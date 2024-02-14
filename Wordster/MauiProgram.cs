using Maui.NeoControls;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Mopups.Hosting;

namespace Wordster
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                   .ConfigureMopups()
                   .UseNeoControls().ConfigureFonts(fonts =>
                   {
                       fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                       fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                       fonts.AddFont("ComicShark.otf", "ComicShark");
                       fonts.AddFont("Font_Awesome_6_Brands-Regular-400.otf", "FaBrands");
                       fonts.AddFont("Font_Awesome_6_Free-Regular-400.otf", "FaRegular");
                       fonts.AddFont("Font_Awesome_6_Free-Solid-900.otf", "FaSolid");
                       fonts.AddFont("SonicComics.ttf", "SonicComics");
                       fonts.AddFont("MouseMemoirs-Regular.ttf", "MouseMemoirs-Regular");
                   }).UseMauiCommunityToolkit();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}