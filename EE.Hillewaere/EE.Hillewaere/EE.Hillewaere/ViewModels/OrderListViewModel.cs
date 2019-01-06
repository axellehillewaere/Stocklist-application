using EE.Hillewaere.Constants;
using EE.Hillewaere.Domain.Models;
using EE.Hillewaere.Domain.Services;
using EE.Hillewaere.Views;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace EE.Hillewaere.ViewModels
{
    public class OrderListViewModel
    {
        private IStocklistService stocklistService;
        private INavigation navigation;

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
                SaveFile();
                await stocklistService.SendOrder();
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
                TotalPrice = stocklistService.CalculateTotalPrice();
            });

        private async void SaveFile()
        {
            var orderList = new OrderList { Id = Guid.NewGuid(), Name = "Orderlist of " + DateTime.Now.ToShortDateString(), Orders = this.OrderList, Price = this.TotalPrice };
            var serializer = new XmlSerializer(typeof(OrderList));
            string orderListAsXml = "";
            using (var stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    serializer.Serialize(writer, orderList);
                    orderListAsXml = stringWriter.ToString();
                }
            }
            IFolder folder = await PCLStorage.FileSystem.Current.LocalStorage.CreateFolderAsync("orders", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("orderlist.xml", CreationCollisionOption.GenerateUniqueName);
            await file.WriteAllTextAsync(orderListAsXml);
        }
    }
}
