using Serilog;
using LibraryRepository;

namespace G07_WebAPI;

public static class Configurator
{
    public static void Configurate()
    {
        IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

        LibraryDbContext.Configuration = configuration; 

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}
