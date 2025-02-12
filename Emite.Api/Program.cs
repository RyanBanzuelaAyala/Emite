using Emite.Startup;

var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();

var builder = WebApplication.CreateBuilder(args)
    .EmiteStartup(configuration);

builder.Build().EmiteApp(configuration).Run();
