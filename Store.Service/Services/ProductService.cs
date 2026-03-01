using AutoMapper;
using Store.Core.Dtos.Products;
using Store.Core.Entities;
using Store.Core.Helper;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Products;
using Store.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;

        }

        public async Task<PaginationResponse<ProductsDto>> GetAllProductsAsync(ProductsSpecParams productsSpec)
        {
            var spec =new ProductSpecifications(productsSpec);
            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);
            var mapped= _mapper.Map<IEnumerable<ProductsDto>>(products);
            var countSpec = new ProductWithCountSpecifications(productsSpec);
            var count=await _unitOfWork.Repository<Product,int>().GetByCountAsync(countSpec);
            return new PaginationResponse<ProductsDto>(productsSpec.pageSize, productsSpec.pageIndex, count,mapped);

        }
        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());
        }


        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            var Type=await _unitOfWork.Repository<ProductType,int>().GetAllAsync();
            var mappedTypes=_mapper.Map<IEnumerable<TypeBrandDto>>(Type);
            return mappedTypes;
        }

        public async Task<ProductsDto> GetProductByIdAsync(int id)
        {
            var spec =new ProductSpecifications(id);

            var product =await _unitOfWork.Repository<Product,int>().GetByIdWithSpecAsync(spec);
            var maapedProduct=_mapper.Map<ProductsDto>(product);
            return maapedProduct;
        }

       
    }
}
