using EE.Hillewaere.Domain.Models;
using EE.Hillewaere.Domain.Services;
using EE.Hillewaere.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EE.Hillewaere.ViewModels
{
    public class OrderProductViewModel
    {
        private IStocklistService stocklistService;
        private SubCategory currentSubCategory;
        private INavigation navigation;

        public OrderProductViewModel(SubCategory subCategory, INavigation navigation, IStocklistService slService)
        {
            this.navigation = navigation;
            this.currentSubCategory = subCategory;
            stocklistService = slService;
            Initialize();
        }

        private async Task Initialize()
        {
            PageTitle = "New Order - Products";
            var products = await stocklistService.GetProductListById(currentSubCategory.Id);
            Products = null;
            Products = new ObservableCollection<Product>(products);
            var orderList = await stocklistService.GetOrderList();
            OrderList = null;
            OrderList = new ObservableCollection<Order>(orderList);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        private decimal pricePerProduct;
        public decimal PricePerProduct
        {
            get { return pricePerProduct; }
            set
            {
                pricePerProduct = value;
                RaisePropertyChanged(nameof(PricePerProduct));
            }
        }

        private decimal totalPrice;
        public decimal TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                RaisePropertyChanged(nameof(TotalPrice));
            }
        }

        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                RaisePropertyChanged(nameof(PageTitle));
            }
        }

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                RaisePropertyChanged(nameof(Products));
            }
        }

        private ObservableCollection<Order> orderList;
        public ObservableCollection<Order> OrderList
        {
            get { return orderList; }
            set {
                orderList = value;
                RaisePropertyChanged(nameof(OrderList));
            }
        }

        public ICommand AddToOrderCommand => new Command<Product>(
            async (Product product) =>
            {
                await DependencyService.Get<ISoundPlayer>().PlaySound();
                DependencyService.Get<IToastNotification>().Show("Product added to order");

                product.Amount++;
                var order = new Order
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Amount = product.Amount,
                    PricePerProduct = product.Price * product.Amount
                };
                OrderList.Add(order);
                await stocklistService.SaveToOrder(order);
                var orderList = await stocklistService.GetOrderList();
                OrderList = new ObservableCollection<Order>(orderList);
                Initialize();
            });

        public ICommand ViewOrder => new Command(
            async () =>
            {
                await navigation.PushAsync(new OrderListView());
            });
    }
}
