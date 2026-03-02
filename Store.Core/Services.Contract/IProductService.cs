using Store.Core.Dtos.Products;
using Store.Core.Entities;
using Store.Core.Helper;
using Store.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services.Contract
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductsDto>> GetAllProductsAsync(ProductsSpecParams productsSpec);

        Task<ProductsDto> GetProductByIdAsync(int id);
        Task<ProductsDto> AddProductAsync(CreateProductsDto product);

        Task<bool> DeleteProductAsync(int id);

        Task<ProductsDto> UpdateProductAsync(int id, CreateProductsDto productDto);

    }
}
