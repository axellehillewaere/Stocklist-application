using EE.Hillewaere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE.Hillewaere.Domain.Services
{
    public class CategoriesInMemoryService
    {
        private static List<Category> categoryLists = new List<Category>
        {
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Drinks",
                SubCategories = new List<SubCategory>
                {
                    new SubCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Soft drinks",
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Coca-Cola",
                                Price = 10M
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Coca-Cola Zero",
                                Price = 12M
                            }
                        }
                    },
                    new SubCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Hot drinks",
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Coffee beans",
                                Price = 15M
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Cécémel",
                                Price = 8M
                            }
                        }
                    }
                }
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Snacks",
                SubCategories = new List<SubCategory>
                {
                    new SubCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Chicken",
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Chicken Nuggets (Nuggiez)",
                                Price = 14M
                            }
                        }
                    },
                    new SubCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Fish",
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Fish burger (Mora)",
                                Price = 15M
                            }
                        }
                    }
                }
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
            return categoryLists.FirstOrDefault().SubCategories.ToList();
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

        public async Task DeleteCategoryList(Guid categoryId)
        {
            await Task.Delay(0);
            var category = categoryLists.FirstOrDefault(c => c.Id == categoryId);
        }
    }
}
