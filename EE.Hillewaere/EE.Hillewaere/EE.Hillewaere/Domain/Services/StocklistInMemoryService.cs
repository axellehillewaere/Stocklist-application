﻿using EE.Hillewaere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE.Hillewaere.Domain.Services
{
    public class StocklistInMemoryService : IStocklistService
    {
        private static List<Order> orderList = new List<Order>();

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
            },

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
                Description = "24 x 33cl",
                Code = "A1234",
                SubCategory = subCategoryLists[0],
                Category = categoryLists[0]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Coca-Cola Zero",
                Price = 12M,
                SubCategory = subCategoryLists[0],
                Category = categoryLists[0]
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

        public async Task<IEnumerable<Order>> GetOrderList()
        {
            await Task.Delay(0);
            return orderList;

        }

        public async Task<IEnumerable<Category>> GetCategoryList()
        {
            await Task.Delay(0);
            return categoryLists.ToList();
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

        public async Task<IEnumerable<Product>> GetProductListBySub(string name)
        {
            await Task.Delay(0);
            return productLists.Where(s => s.SubCategory.Name == name);
        }

        public async Task SaveToOrder(Order order)
        {
            await Task.Delay(0);
            var savedOrder = orderList.FirstOrDefault(o => o.Id == order.Id);
            if (savedOrder == null)
            {
                savedOrder = order;
                savedOrder.Id = order.Id;
                orderList.Add(savedOrder);
            }
            savedOrder.Name = order.Name;
            savedOrder.Price = order.Price;
            savedOrder.Amount = order.Amount;
            savedOrder.PricePerProduct = order.PricePerProduct;
            savedOrder.TotalPrice = CalculateTotalPrice();
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
            savedProduct.Code = product.Code;
            savedProduct.Description = product.Description;
            savedProduct.SubCategory = product.SubCategory;
            savedProduct.Category = product.Category;
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            await Task.Delay(0);
            var category = categoryLists.FirstOrDefault(c => c.Id == categoryId);
            categoryLists.Remove(category);
        }

        public async Task DeleteSubCategory(Guid subCategoryId)
        {
            await Task.Delay(0);
            var subCategory = subCategoryLists.FirstOrDefault(c => c.Id == subCategoryId);
            subCategoryLists.Remove(subCategory);
        }

        public async Task DeleteProduct(Guid productId)
        {
            await Task.Delay(0);
            var product = productLists.FirstOrDefault(p => p.Id == productId);
            productLists.Remove(product);
        }

        public decimal CalculateTotalPrice()
        {
            return orderList.Sum(o => o.PricePerProduct);
        }
    }
}
