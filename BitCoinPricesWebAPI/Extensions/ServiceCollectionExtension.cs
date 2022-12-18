using AutoMapper;
using BitCoinPricesWebAPI.Repo.Data;
using BitCoinPricesWebAPI.Repo.Interface;
using BitCoinPricesWebAPI.Repo.Repository;
using BitCoinPricesWebAPI.Service.Filters.ActionFilters;
using BitCoinPricesWebAPI.Service.Interfaces;
using BitCoinPricesWebAPI.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
namespace BitCoinPricesWebAPI.Extensions;
public static class ServiceExtension
{
    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddScoped<ILoggerManagerService, LoggerManagerService>();
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(
            opts => opts.UseSqlServer(configuration.GetConnectionString("BTCDB")));
    public static void ConfigureDapperContext(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();
    }
    public static void ConfigureMapping(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        var mapperConfig = new MapperConfiguration(map =>
        {
            //map.AddProfile<ShiftMappingProfile>();
            //map.AddProfile<LocationMappingProfile>();
            //map.AddProfile<AttendanceMappingProfile>();
            //map.AddProfile<OvertimeMappingProfile>();
        });
        services.AddSingleton(mapperConfig.CreateMapper());
    }
    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddTransient<IErrorLogService, ErrorLogService>();
        services.AddTransient<ILoggerManagerService, LoggerManagerService>();
        services.AddTransient<ISystemUserService, SystemUserService>();
        services.AddTransient<ISearchService, SearchService>();
        services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
        services.AddScoped<ILoggerManagerRepository, LoggerManagerRepository>();
        services.AddScoped<ISystemUserRepository, SystemUserRepository>();
        services.AddScoped<ISearchRepository, SearchRepository>();
    }
    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers(config =>
        {
            config.CacheProfiles.Add("30SecondsCaching", new CacheProfile
            {
                Duration = 30
            });
        });
    }
    public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BitCoin Prices Search Tool",
                Version = "v1",
                Description = "BitCoin Prices Search Tool API Services.",
                Contact = new OpenApiContact
                {
                    Name = "BitCoin Prices Search Tool"
                },
            });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            //c.OperationFilter<SwaggerFileOperationFilter>();
        });
    }
    public static void RegisterDependencies(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilterAttribute>();
    }
}
