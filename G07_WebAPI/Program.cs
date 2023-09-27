namespace G07_WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        Configurator.Configurate();

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers(options => options.Filters.Add<ExceptionHendler>());

        var app = builder.Build();
        app.MapControllers();

        app.Run();
    }
}