﻿using EE.Hillewaere.Constants;
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
    public class OrderListViewModel
    {
        private IStocklistService stocklistService;
        private INavigation navigation;
        private Order currentOrder;

        public OrderListViewModel(INavigation navigation, IStocklistService slService)
        {
            this.navigation = navigation;
            stocklistService = slService;
            Initialize();
        }

        private async Task Initialize()
        {
            PageTitle = "New Order - Summary";
            var orderList = await stocklistService.GetOrderList();
            OrderList = null;
            OrderList = new ObservableCollection<Order>(orderList);
            TotalPrice = stocklistService.CalculateTotalPrice();
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

        private ObservableCollection<Order> orderList;
        public ObservableCollection<Order> OrderList
        {
            get { return orderList; }
            set
            {
                orderList = value;
                RaisePropertyChanged(nameof(OrderList));
            }
        }

        public ICommand SendOrderCommand => new Command(
            async () =>
            {
                await DependencyService.Get<ISoundPlayer>().PlaySound();
                DependencyService.Get<IToastNotification>().Show("Order has been sent so supplier");
                OrderList.Clear();
                await navigation.PushAsync(new MainView());
            });

        public ICommand DeleteOrderProductCommand => new Command<Order>(
            async (Order orderProduct) =>
            {
                orderProduct.Amount--;
                OrderList.Remove(orderProduct);
                Debug.WriteLine(orderProduct.Name);
                await stocklistService.DeleteOrderProduct(orderProduct.Id);
                var orderList = await stocklistService.GetOrderList();
                OrderList = null;
                OrderList = new ObservableCollection<Order>(orderList);
            });
    }
}
