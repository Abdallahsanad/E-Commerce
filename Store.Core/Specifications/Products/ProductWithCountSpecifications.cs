using Store.Core.Entities;
using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Products
{
    public class ProductWithCountSpecifications : BaseSpecifications<Product,int>
    {
        public ProductWithCountSpecifications(ProductsSpecParams productsSpec):base(
            P =>
           (!productsSpec.brandId.HasValue || productsSpec.brandId == P.BrandId)
            &&
            (!productsSpec.TypeId.HasValue || productsSpec.TypeId == P.TypeId)
            )

        { }
    }
}
