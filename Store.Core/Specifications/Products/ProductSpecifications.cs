using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Products
{
    public class ProductSpecifications : BaseSpecifications<Product,int>
    {

        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }

        public ProductSpecifications(ProductsSpecParams productsSpec) : base(
            p =>
            (string.IsNullOrEmpty(productsSpec.Search) || p.Name.ToLower().Contains(productsSpec.Search))
            &&
            (!productsSpec.brandId.HasValue || productsSpec.brandId == p.BrandId)
            &&
            (!productsSpec.TypeId.HasValue ||productsSpec.TypeId == p.TypeId)
            )
        {
            if (!string.IsNullOrEmpty(productsSpec.sort))
            {

                switch (productsSpec.sort)
                {
                    case "PriceASc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "PriceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;

                    case "NameDesc":
                        AddOrderByDescending(p => p.Name);
                        break;

                    case "CreateAtAsc":
                        AddOrderBy(p => p.CreateAt);
                        break;

                    case "CreateAtDesc":
                        AddOrderByDescending(p => p.CreateAt);
                        break;

                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
            ApplyIncludes();

            ApplyPagination(productsSpec.pageSize * (productsSpec.pageIndex - 1), productsSpec.pageSize);

        }


        public void ApplyIncludes()
        {
            Includes.Add(p=>p.Brand);
            Includes.Add(p=>p.Type);
        }
    }

}
