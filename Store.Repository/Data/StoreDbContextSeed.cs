using Store.Core.Entities;
using Store.Repository.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public async static Task SeedAsync(StoreDbContext _context)
        {
            if (_context.ProductBrands.Count() == 0)
            {
                var BrandData = File.ReadAllText(@"..\Store.Repository\Data\DataSeed\brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

                if (brands is not null && brands.Count() > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }

            if (_context.ProductTypes.Count() == 0)
            {
                var TypeData = File.ReadAllText(@"..\Store.Repository\Data\DataSeed\types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                if (types is not null && types.Count() > 0)
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }
            }

            if (_context.Products.Count() == 0)
            {
                var ProductData = File.ReadAllText(@"..\Store.Repository\Data\DataSeed\products.json");

                var Producta = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (Producta is not null && Producta.Count() > 0)
                {
                    await _context.Products.AddRangeAsync(Producta);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
