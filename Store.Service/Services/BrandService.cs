using AutoMapper;
using Store.Core.Dtos.Products;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services
{
    public class BrandService : IBrandService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());
        }

        public async Task<TypeBrandDto> AddBrandAsync(TypeBrandDto brand)
        {
            if (brand is null) return null;
            var mapped = _mapper.Map<ProductBrand>(brand);
            await _unitOfWork.Repository<ProductBrand, int>().AddAsync(mapped);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            var final = _mapper.Map<TypeBrandDto>(mapped);
            return final;
        }


        public async Task<bool> DeleteBrand(int id)
        {
            var brand = await _unitOfWork.Repository<ProductBrand, int>().GetByIdAsync(id);
            if (brand is null) return false;
            _unitOfWork.Repository<ProductBrand, int>().Delete(brand);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return false;
            return result > 0;
        }

        public async Task<TypeBrandDto> UpdateBrand(int id, TypeBrandDto brandDto)
        {
            var brand=  await _unitOfWork.Repository<ProductBrand, int>().GetByIdAsync(id);
            if (brand is null) return null;
            _mapper.Map(brandDto, brand);
            _unitOfWork.Repository<ProductBrand, int>().Update(brand);
            var result= await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            var final=_mapper.Map<TypeBrandDto>(brand);
            return final;
        }
    }
}
