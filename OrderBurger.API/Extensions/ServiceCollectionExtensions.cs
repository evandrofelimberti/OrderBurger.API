using OrderBurger.API.Data;
using OrderBurger.API.Mappings;
using OrderBurger.API.Repositories;
using OrderBurger.API.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OrderBurger.API.Business.OrderDiscount;
using OrderBurger.API.Validators;

namespace OrderBurger.API.Extensions;

public static class ServiceCollectionExtensions
{
public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            options.UseSqlite(connectionString, sqliteOptions =>
            {
                sqliteOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            });
            
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IBusinessPartnerRepository, BusinessPartnerRepository>();
        services.AddScoped<IInventoryTransactionRepository, InventoryTransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderDiscountStrategy,ComboOrderDiscountStrategy>();
        return services;
    }
    

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IBusinessPartnerService, BusinessPartnerService>();
        services.AddScoped<IInventoryTransactionService, InventoryTransactionService>();
        services.AddScoped<IUserService, UserService>();
        //services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }

    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductProfile).Assembly);
        services.AddAutoMapper(typeof(OrderProfile).Assembly);
        services.AddAutoMapper(typeof(BusinessPartnerProfile).Assembly);
        services.AddAutoMapper(typeof(InventoryTransactionProfile).Assembly);       
        return services;
    }

    public static IServiceCollection AddFluentValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<ProductValidator>();
        services.AddValidatorsFromAssemblyContaining<OrderValidator>();
        services.AddValidatorsFromAssemblyContaining<OrderItemValidator>();
        services.AddValidatorsFromAssemblyContaining<BusinessPartnerValidator>();
        services.AddValidatorsFromAssemblyContaining<InventoryTransactionValidator>();
        return services;
    }

    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title       = "OrderBurger.API",
                Version     = "v1",
                Description = "API RESTful para gerenciamento de pedidos de hamburger — .NET 9"
            });
            options.EnableAnnotations();
            
            var securityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Insira o token JWT no formato: Bearer {seu token}",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            
            options.AddSecurityDefinition("Bearer", securityScheme);
            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            });
            
        });

        return services;
    } 
}