using Store.Core.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services.Contract
{
    public interface ITypeService
    {
        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();

        Task<TypeBrandDto> AddTypeAsync(TypeBrandDto brand);
        Task<bool> DeleteType(int id);

        Task<TypeBrandDto> UpdateType(int id, TypeBrandDto brand);


    }
}
