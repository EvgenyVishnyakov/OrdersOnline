using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using OnlineOrder.Db;
using OnlineOrder.Db.Interface;
using OnlineOrder.Db.Repository;
using OnlineOrderWebApp.Service;
using Serilog;

try
{
    Log.Information("starting server.");
    Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    var connection = builder.Configuration.GetConnectionString("online_order_Vishnyakov");

    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });
    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseNpgsql(connection));

    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    builder.Services.AddScoped<OrderService>();
    builder.Services.AddScoped<ProductService>();

    builder.Services.AddTransient<IOrderDbRepository, OrderDbRepository>();
    builder.Services.AddTransient<IProductRepository, ProductDbRepository>();

    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", "Online Order");
    });

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Online Order API",
            Description = "Online Order ASP.NET Core Web API"
        });
        options.UseInlineDefinitionsForEnums();
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Order API v1");
        });
    }

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<DatabaseContext>();
        await dbContext.Database.MigrateAsync();
    }

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.MapControllerRoute(
       name: "MyArea",
       pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
