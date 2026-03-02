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
        

        public async Task<ProductsDto> GetProductByIdAsync(int id)
        {
            var spec =new ProductSpecifications(id);

            var product =await _unitOfWork.Repository<Product,int>().GetByIdWithSpecAsync(spec);
            var maapedProduct=_mapper.Map<ProductsDto>(product);
            return maapedProduct;
        }

        

        public async Task<ProductsDto> AddProductAsync(CreateProductsDto product)
        {
            if (product is null) return null;
            var mappedProduct = _mapper.Map<Product>(product);
            await _unitOfWork.Repository<Product, int>().AddAsync(mappedProduct);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return _mapper.Map<ProductsDto>(mappedProduct);
        }

        


        public async Task<bool> DeleteProductAsync(int id)
        { 
            var product= await _unitOfWork.Repository<Product, int>().GetByIdAsync(id);
            if (product is null) return false;
            _unitOfWork.Repository<Product,int>().Delete(product);
            var deleted=await _unitOfWork.CompleteAsync();  
            if (deleted <= 0) return false;
            return deleted>0;
        }

        public async Task<ProductsDto> UpdateProductAsync(int id, CreateProductsDto productDto)
        {
            var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(id);
            if (product is null) return null;
            _mapper.Map(productDto, product);
            _unitOfWork.Repository<Product, int>().Update(product);
            var result=await _unitOfWork.CompleteAsync();
            if (result<=0) return null;
            var mapped=_mapper.Map<ProductsDto>(product);
            return mapped;
        }
    }
}
