
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store.Api.Errors;
using Store.Api.Helper;
using Store.Api.Middleware;
using Store.Core.Mapping.Products;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Repository;
using Store.Repository.Data;
using Store.Repository.Data.Context;
using Store.Service.Services;
using System.Threading.Tasks;

namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependencyInjection(builder.Configuration);

            var app = builder.Build();



            await app.ConfigureMiddlewareAsync();

            app.Run();
        }
    }
}
