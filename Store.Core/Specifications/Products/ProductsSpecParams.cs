using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Products
{
    public class ProductsSpecParams
    {
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? TypeId { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 5;
    }
}
