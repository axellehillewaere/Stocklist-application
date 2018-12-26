using EE.Hillewaere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE.Hillewaere.Domain.Services
{
    public class StocklistInMemoryService
    {
        private static List<Category> categoryLists = new List<Category>
        {
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Drinks",
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Snacks",
            }
        };

        private static List<SubCategory> subCategoryLists = new List<SubCategory>
        {
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Soft drinks",
                Category = categoryLists[0]
            },
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Hot drinks",
                Category = categoryLists[0]
            },
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Chicken",
                Category = categoryLists[1]
            },
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Fish",
                Category = categoryLists[1]
            }
        };

        private static List<Product> productLists = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Coca-Cola",
                Price = 10M,
                SubCategory = subCategoryLists[0]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Coca-Cola Zero",
                Price = 12M,
                SubCategory = subCategoryLists[0]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Coffee beans",
                Price = 14M,
                SubCategory = subCategoryLists[1]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Cécémel",
                Price = 13M,
                SubCategory = subCategoryLists[1]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Chicken Nuggets (Nuggiez)",
                Price = 15M,
                SubCategory = subCategoryLists[2]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Fish Burger (Mora)",
                Price = 14M,
                SubCategory = subCategoryLists[3]
            }
        };

        public async Task<IEnumerable<Category>> GetCategoryList()
        {
            await Task.Delay(0);
            return categoryLists.ToList();
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategoryList()
        {
            await Task.Delay(0);
            return subCategoryLists.ToList();
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategoryListById(Guid subCategoryId)
        {
            await Task.Delay(0);
            return subCategoryLists.Where(s => s.Category.Id == subCategoryId);
        }

        public async Task<IEnumerable<Product>> GetProductListById(Guid productId)
        {
            await Task.Delay(0);
            return productLists.Where(s => s.SubCategory.Id == productId);
        }

        public async Task<Category> GetById(Guid categoryId)
        {
            await Task.Delay(0);
            return categoryLists.FirstOrDefault(c => c.Id == categoryId);
        }

        public async Task<SubCategory> GetSubCategoryById(Guid subCategoryId)
        {
            await Task.Delay(0);
            return categoryLists.FirstOrDefault(c => c.Id == subCategoryId).SubCategories.FirstOrDefault(ci => ci.Id == subCategoryId);
        }


        public async Task<IEnumerable<Product>> GetProductList()
        {
            await Task.Delay(0);
            return categoryLists.FirstOrDefault().SubCategories.FirstOrDefault().Products.ToList();
        }

        public async Task SaveCategoryList(Category category)
        {
            await Task.Delay(0);
            var savedCategory = categoryLists.FirstOrDefault(c => c.Id == category.Id);
            if (savedCategory == null)
            {
                savedCategory = category;
                savedCategory.Id = Guid.NewGuid();
                categoryLists.Add(savedCategory);
            }
            savedCategory.Name = category.Name;
            savedCategory.SubCategories = category.SubCategories;
        }

        public async Task SaveProduct(Product product)
        {
            await Task.Delay(0);
            var savedProduct = productLists.FirstOrDefault(s => s.Id == product.Id);
            if (savedProduct == null)
            {
                savedProduct = product;
                savedProduct.Id = Guid.NewGuid();
                productLists.Add(savedProduct);
            }
            savedProduct.Name = product.Name;
            savedProduct.Price = product.Price;
            savedProduct.Description = product.Description;
            savedProduct.SubCategory = product.SubCategory;
        }

        public async Task DeleteProduct(Guid productId)
        {
            await Task.Delay(0);
            var product = productLists.FirstOrDefault(p => p.Id == productId);
            productLists.Remove(product);
        }

        public async Task DeleteCategoryList(Guid categoryId)
        {
            await Task.Delay(0);
            var category = categoryLists.FirstOrDefault(c => c.Id == categoryId);
        }
    }
}
