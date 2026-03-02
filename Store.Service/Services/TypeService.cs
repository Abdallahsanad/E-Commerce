using AutoMapper;
using Store.Core.Dtos.Products;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services
{
    public class TypeService :ITypeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }


        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            var Type = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            var mappedTypes = _mapper.Map<IEnumerable<TypeBrandDto>>(Type);
            return mappedTypes;
        }


        public async Task<TypeBrandDto> AddTypeAsync(TypeBrandDto type)
        {
            if (type is null) return null;
            var mapped = _mapper.Map<ProductType>(type);
            await _unitOfWork.Repository<ProductType, int>().AddAsync(mapped);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            var final = _mapper.Map<TypeBrandDto>(mapped);
            return final;
        }

        public async Task<bool> DeleteType(int id)
        {
            var type=await _unitOfWork.Repository<ProductType,int>().GetByIdAsync(id);
            if(type is null) return false;
            _unitOfWork.Repository<ProductType,int>().Delete(type);
            var result=await _unitOfWork.CompleteAsync();
            if (result <= 0) return false;
            return result>0;
        }

        public async Task<TypeBrandDto> UpdateType(int id, TypeBrandDto typeDto)
        {
            var type = await _unitOfWork.Repository<ProductType, int>().GetByIdAsync(id);
            if (type is null) return null;
            _mapper.Map(typeDto, type);
            _unitOfWork.Repository<ProductType, int>().Update(type);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            var final = _mapper.Map<TypeBrandDto>(type);
            return final;
        }
    }
}
