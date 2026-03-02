using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Store.Api.Errors;
using Store.Core.Entities.Identity;
using Store.Core.Mapping.Basket;
using Store.Core.Mapping.Products;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Repository;
using Store.Repository.Data.Context;
using Store.Repository.Identity.Context;
using Store.Repository.Repositories;
using Store.Service.Services;
using Store.Service.Services.Caches;

namespace Store.Api.Helper
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependencyInjection(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddSwaggerService();
            services.AddDbContextService(configuration);
            services.AddUserDefinedService();
            services.AddAutoMapperService(configuration);
            services.ConfigInvalidModelStateResponseService();
            services.AddRedisService(configuration);
            services.AddIdentityService();
            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        private static IServiceCollection AddDbContextService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            
            return services;
        }

        private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }

        private static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new BasketProfile()));
            return services;
        }

        private static IServiceCollection ConfigInvalidModelStateResponseService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            }
            );
            return services;
        }
        private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>( (ServiceProvider) =>
            {
                var connection = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            return services;
        }

        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>()
                .AddSignInManager<SignInManager<AppUser>>();
            return services;
        }

    }
}
