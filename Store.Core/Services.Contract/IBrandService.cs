using Store.Core.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services.Contract
{
    public interface IBrandService
    {
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        Task<TypeBrandDto> AddBrandAsync(TypeBrandDto brand);
        Task<bool> DeleteBrand(int id);
        Task<TypeBrandDto> UpdateBrand(int id,TypeBrandDto brand);

    }
}
