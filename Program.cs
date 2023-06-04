using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/healthz", () => HealthCheck.GetHealthCheck());
        app.MapGet("/users", () => Users.GetUsers());

        app.Run();
    }
}