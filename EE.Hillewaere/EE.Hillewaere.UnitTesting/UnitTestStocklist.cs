using EE.Hillewaere.Domain.Models;
using EE.Hillewaere.Domain.Services;
using EE.Hillewaere.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xunit;

namespace EE.Hillewaere.UnitTesting
{
    public class UnitTestStocklist
    {
        [Fact]
        public void TestCategoriesNotEmpty()
        {
            var stocklistService = new StocklistInMemoryService();
            var result = stocklistService.GetCategoryList().Result;
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestSubCategoryChickenNotEmpty()
        {
            var stocklistService = new StocklistInMemoryService();
            var subCategory = new SubCategory();
            subCategory.Name = "Chicken";
            var result = stocklistService.GetProductListBySub(subCategory.Name).Result;
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestSubCategorySoftDrinksNotEmpty()
        {
            var stocklistService = new StocklistInMemoryService();
            var subCategory = new SubCategory();
            subCategory.Name = "Soft drinks";
            var result = stocklistService.GetProductListBySub(subCategory.Name).Result;
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestSubCategoryColdSaucesNotEmpty()
        {
            var stocklistService = new StocklistInMemoryService();
            var subCategory = new SubCategory();
            subCategory.Name = "Cold sauces";
            var result = stocklistService.GetProductListBySub(subCategory.Name).Result;
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestSubCategoryFakeNotEmpty()
        {
            var stocklistService = new StocklistInMemoryService();
            var subCategory = new SubCategory();
            subCategory.Name = "Fake subcategory";
            var result = stocklistService.GetProductListBySub(subCategory.Name).Result;
            Assert.Empty(result);
        }

        [Fact]
        public async void TestSaveToOrder()
        {
            var stocklistService = new StocklistInMemoryService();
            var orderList = new List<Order>();
            var order = new Order();
            order.Id = Guid.NewGuid();
            order.Name = "Coca-Cola";
            order.Price = 16M;
            order.PricePerProduct = 16M;
            order.TotalPrice = 16M;
            await stocklistService.SaveToOrder(order);
            var result = stocklistService.GetOrderList().Result;
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void TestCalculateTotalPriceOfOrderList()
        {
            var stocklistService = new StocklistInMemoryService();
            var orderList = new List<Order>();
            var order = new Order();
            order.Id = Guid.NewGuid();
            order.Name = "Coca-Cola";
            order.Price = 16M;
            order.Amount = 1;
            order.PricePerProduct = 16M;
            order.TotalPrice = 16M;
            await stocklistService.SaveToOrder(order);
            var result = stocklistService.CalculateTotalPrice();
            Assert.Equal(16, result);
        }

        [Fact]
        public void TestDeleteCategoryCommand()
        {
            var stocklistService = new StocklistInMemoryService();
            var vm = new StocklistViewModel(null, stocklistService);
            var categories = stocklistService.GetCategoryList().Result;
            var categoryOne = categories.FirstOrDefault();
            vm.DeleteCategoryCommand.Execute(categoryOne);
            Assert.True(vm.Categories.Count == 3);
        }
    }
}
