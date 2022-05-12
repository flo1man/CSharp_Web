using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Services
{
    public interface IProductsService
    {
        void AddProduct(ProductInputModel model);

        IEnumerable<ProductViewModel> GetAll();

        ProductViewModel GetProductById(int id);

        void DeleteProduct(int id);
    }
}
