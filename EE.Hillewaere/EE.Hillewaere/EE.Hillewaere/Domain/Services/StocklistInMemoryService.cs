using EE.Hillewaere.Domain.Models;
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
                Description = "Soft drinks, hot drinks, alcohol"
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Snacks",
                Description = "Hamburgers, fricandelles, croquettes"
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Brochettes",
                Description = "Chicken, porc, fish"
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Sauces",
                Description = "Cold sauces, hot sauces, fresh sauces"
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
                Name = "Alcohol",
                Category = categoryLists[0]
            },
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Popular snacks",
                Category = categoryLists[1]
            },
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Special snacks",
                Category = categoryLists[1]
            },
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Croquettes",
                Category = categoryLists[1]
            },
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Chicken",
                Category = categoryLists[2]
            },
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Porc",
                Category = categoryLists[2]
            },
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Fish",
                Category = categoryLists[2]
            },
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Cold sauces",
                Category = categoryLists[3]
            },
            new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = "Hot sauces",
                Category = categoryLists[3]
            },
        };

        private static List<Product> productLists = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Coca-Cola",
                Price = 16M,
                Code = "A1234",
                ExtraInfo = "24 x 33cl",
                SubCategory = subCategoryLists[0],
                Category = categoryLists[0]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Coca-Cola Zero",
                Price = 16M,
                Code = "B1234",
                ExtraInfo = "24 x 33cl",
                SubCategory = subCategoryLists[0],
                Category = categoryLists[0]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Coca-Cola Light",
                Price = 16M,
                Code = "C1234",
                ExtraInfo = "24 x 33cl",
                SubCategory = subCategoryLists[0],
                Category = categoryLists[0]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Coffee beans",
                Price = 14M,
                Code = "D1234",
                ExtraInfo = "FairTrade 5kg",
                SubCategory = subCategoryLists[1],
                Category = categoryLists[0]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Cécémel",
                Price = 13M,
                Code = "E1234",
                ExtraInfo = "Milk Chocolate 6 x 2l ",
                SubCategory = subCategoryLists[1],
                Category = categoryLists[0]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Tea bags",
                Price = 8M,
                Code = "F1234",
                ExtraInfo = "Lemon 100 pcs",
                SubCategory = subCategoryLists[1],
                Category = categoryLists[0]
            },
            new Product
            {
              Id = Guid.NewGuid(),
              Name = "Jupiler",
              Price = 11M,
              Code = "G1234",
              ExtraInfo = "24 x 33cl",
              SubCategory = subCategoryLists[2],
              Category = categoryLists[0]
            },
            new Product
            {
              Id = Guid.NewGuid(),
              Name = "Kriek Belle Vue",
              Price = 13M,
              Code = "H1234",
              ExtraInfo = "24 x 25cl",
              SubCategory = subCategoryLists[2],
              Category = categoryLists[0]
            },
            new Product
            {
              Id = Guid.NewGuid(),
              Name = "White wine",
              Price = 12M,
              Code = "I1234",
              ExtraInfo = "J.P. Chenet 6 x 25cl",
              SubCategory = subCategoryLists[2],
              Category = categoryLists[0]
            },
            new Product
            {
              Id = Guid.NewGuid(),
              Name = "Fricandel original",
              Price = 11M,
              Code = "J1234",
              ExtraInfo = "Vanreusel 100gr 20pcs",
              SubCategory = subCategoryLists[3],
              Category = categoryLists[1]
            },
            new Product
            {
              Id = Guid.NewGuid(),
              Name = "Viandelle",
              Price = 13M,
              Code = "K1234",
              ExtraInfo = "Mora 100 gr 27pcs",
              SubCategory = subCategoryLists[3],
              Category = categoryLists[1]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Chickennuggets",
                Price = 20M,
                Code = "L1234",
                ExtraInfo = "Ovi 120pcs",
                SubCategory = subCategoryLists[3],
                Category = categoryLists[1]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Lucifer Mini",
                Price = 19M,
                Code = "M1234",
                ExtraInfo = "Mora 60pcs",
                SubCategory = subCategoryLists[4],
                Category = categoryLists[1]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Viandelle Spicy",
                Price = 18M,
                Code = "N1234",
                ExtraInfo = "Mora 100gr 27pcs",
                SubCategory = subCategoryLists[4],
                Category = categoryLists[1]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Bami",
                Price = 8M,
                Code = "O1234",
                ExtraInfo = "Elite 130gr 18pcs",
                SubCategory = subCategoryLists[4],
                Category = categoryLists[1]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Cheese croquette Oud-Brugge",
                Price = 13M,
                Code = "P1234",
                ExtraInfo = "Ottimo 65gr 20pcs",
                SubCategory = subCategoryLists[5],
                Category = categoryLists[1]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Goulash croquette",
                Price = 9M,
                Code = "Q1234",
                ExtraInfo = "Devries 90gr 16pcs",
                SubCategory = subCategoryLists[5],
                Category = categoryLists[1]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Shrimp croquette",
                Price = 60M,
                Code = "R1234",
                ExtraInfo = "Mestdagh 65gr 32pcs",
                SubCategory = subCategoryLists[5],
                Category = categoryLists[1]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sito Gold",
                Price = 23M,
                Code = "S1234",
                ExtraInfo = "Mora 125gr 21pcs",
                SubCategory = subCategoryLists[6],
                Category = categoryLists[2]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Chicken Wings",
                Price = 20M,
                Code = "T1234",
                ExtraInfo = "Hofkip 125gr 15pcs",
                SubCategory = subCategoryLists[6],
                Category = categoryLists[2]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Chickero",
                Price = 15M,
                Code = "U1234",
                ExtraInfo = "Hofkip 90gr 20pcs",
                SubCategory = subCategoryLists[6],
                Category = categoryLists[2]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sate Excellent",
                Price = 39M,
                Code = "AA123",
                ExtraInfo = "Mora 85gr 32pcs",
                SubCategory = subCategoryLists[7],
                Category = categoryLists[2]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sate Fresh",
                Price = 10M,
                Code = "BB134",
                ExtraInfo = "Nick 150gr 6pcs",
                SubCategory = subCategoryLists[7],
                Category = categoryLists[2]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Merguez",
                Price = 37M,
                Code = "CC123",
                ExtraInfo = "Vanreusel 95gr 30pcs",
                SubCategory = subCategoryLists[7],
                Category = categoryLists[2]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Picknicker",
                Price = 27M,
                Code = "V1234",
                ExtraInfo = "Mora 110gr 24pcs",
                SubCategory = subCategoryLists[8],
                Category = categoryLists[2]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Fish Brochette",
                Price = 23M,
                Code = "W1234",
                ExtraInfo = "Vanreusel 110gr 22pcs",
                SubCategory = subCategoryLists[8],
                Category = categoryLists[2]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Scampi Brochette",
                Price = 18M,
                Code = "X1234",
                ExtraInfo = "Oceon Prid 150gr 10pcs",
                SubCategory = subCategoryLists[8],
                Category = categoryLists[2]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Mayonaise",
                Price = 22M,
                Code = "DD123",
                ExtraInfo = "Pauwels 10l",
                SubCategory = subCategoryLists[9],
                Category = categoryLists[3]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Tartaar",
                Price = 27M,
                Code = "EE123",
                ExtraInfo = "La William 10kg",
                SubCategory = subCategoryLists[9],
                Category = categoryLists[3]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Andalouse Chef",
                Price = 37M,
                Code = "FF123",
                ExtraInfo = "La William 9,8kg",
                SubCategory = subCategoryLists[9],
                Category = categoryLists[3]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Bearnaise Warm Up",
                Price = 22M,
                Code = "GG123",
                ExtraInfo = "Verstegen 3 x 1l",
                SubCategory = subCategoryLists[10],
                Category = categoryLists[3]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Pepper Sauce Garde D'Or",
                Price = 6M,
                Code = "HH123",
                ExtraInfo = "Knorr 1l",
                SubCategory = subCategoryLists[10],
                Category = categoryLists[3]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Beef Stew Sauce",
                Price = 8M,
                Code = "GG123",
                ExtraInfo = "Coertjens 2,7kg",
                SubCategory = subCategoryLists[10],
                Category = categoryLists[3]
            },
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
            savedProduct.ExtraInfo = product.ExtraInfo;
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

        public async Task DeleteOrderProduct(Guid orderProductId)
        {
            await Task.Delay(0);
            var orderProduct = orderList.FirstOrDefault(o => o.Id == orderProductId);
            orderList.Remove(orderProduct);
        }

        public decimal CalculateTotalPrice()
        {
            return orderList.Sum(o => o.PricePerProduct);
        }
    }
}
