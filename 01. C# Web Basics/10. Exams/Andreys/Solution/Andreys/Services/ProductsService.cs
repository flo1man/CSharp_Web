using Andreys.Data;
using Andreys.Models;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andreys.Services
{
    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void AddProduct(ProductInputModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Category = Enum.Parse<Category>(model.Category),
                Gender = Enum.Parse<Gender>(model.Gender),
                Price = model.Price,
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.Id == id);

            this.db.Products.Remove(product);
            this.db.SaveChanges();
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            var products = db.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                Name = x.Name,
                Gender = x.Gender.ToString(),
                Category = x.Category.ToString(),
            }).ToList();

            return products;
        }

        public ProductViewModel GetProductById(int id)
        {
            var product = this.db.Products.Where(x => x.Id == id)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price,
                    Name = x.Name,
                    Gender = x.Gender.ToString(),
                    Category = x.Category.ToString(),
                })
                .FirstOrDefault();

            return product;
        }
    }
}
