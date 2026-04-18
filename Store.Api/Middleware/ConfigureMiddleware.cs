//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Store.Core.Entities.Identity;
//using Store.Repository.Data;
//using Store.Repository.Data.Context;
//using Store.Repository.Identity;
//using Store.Repository.Identity.Context;

//namespace Store.Api.Middleware
//{
//    public static class ConfigureMiddleware
//    {
//        public async static Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
//        {
//            using var scope = app.Services.CreateScope();
//            var service = scope.ServiceProvider;
//            var context = service.GetService<StoreDbContext>();
//            var identity = service.GetService<StoreIdentityDbContext>();
//            var userManager = service.GetRequiredService<UserManager<AppUser>>();
//            var loggerFactory = service.GetService<LoggerFactory>();
//            try
//            {
//                await context.Database.MigrateAsync();
//                await StoreDbContextSeed.SeedAsync(context);
//                await identity.Database.MigrateAsync();
//                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManager);

//            }
//            catch (Exception ex)
//            {
//                var logger = loggerFactory.CreateLogger<Program>();
//                logger.LogError(ex, "There are Problem During Migration");
//            }

//            app.UseMiddleware<ExceptionMiddleware>();
//            app.UseStatusCodePagesWithReExecute("/error/{0}");

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }



//            app.UseStaticFiles();

//            app.UseHttpsRedirection();

//            app.UseAuthorization();


//            app.MapControllers();

//            return app;
//        }
//    }
//}


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities.Identity;
using Store.Repository.Data;
using Store.Repository.Data.Context;
using Store.Repository.Identity;
using Store.Repository.Identity.Context;
using Microsoft.Extensions.Logging; // تأكد من إضافة هذا السطر

namespace Store.Api.Middleware
{
    public static class ConfigureMiddleware
    {
        public async static Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;

            // جلب الخدمات الأساسية
            var context = service.GetRequiredService<StoreDbContext>();
            var identity = service.GetRequiredService<StoreIdentityDbContext>();
            var userManager = service.GetRequiredService<UserManager<AppUser>>();

            // التصحيح: استخدام ILoggerFactory بدلاً من LoggerFactory
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                // تنفيذ الـ Migrations
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
                await identity.Database.MigrateAsync();
                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManager);
            }
            catch (Exception ex)
            {
                // الآن الـ loggerFactory لن يكون null
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There are Problems During Migration");
            }

            // إعدادات الـ Middleware
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}
