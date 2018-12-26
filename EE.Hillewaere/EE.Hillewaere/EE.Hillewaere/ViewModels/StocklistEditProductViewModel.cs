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
    public class StocklistEditProductViewModel : INotifyPropertyChanged
    {
        private StocklistInMemoryService stocklistService;
        private Product currentProduct;
        private INavigation navigation;
        public event PropertyChangedEventHandler PropertyChanged;


        public StocklistEditProductViewModel(Product product, INavigation navigation)
        {
            this.navigation = navigation;
            this.currentProduct = product;
            stocklistService = new StocklistInMemoryService();
            RefreshProducts();
        }

        private async Task RefreshProducts()
        {
            if (currentProduct != null)
            {
                PageTitle = currentProduct.Name;

                //currentProduct = await stocklistService.GetProductListById(currentProduct.Id);
            }
            else
            {
                PageTitle = "New Product";
                currentProduct = new Product();
                currentProduct.Id = Guid.NewGuid();
            }
            LoadProductsState();
        }

        private void LoadProductsState()
        {
            Name = currentProduct.Name;
            Price = currentProduct.Price;
            Description = currentProduct.Description;
        }

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

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
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

        private ObservableCollection<SubCategory> sub;
        public ObservableCollection<SubCategory> Sub
        {
            get { return sub; }
            set
            {
                sub = value;
                RaisePropertyChanged(nameof(Sub));
            }
        }

        private void SaveProductState()
        {
            currentProduct.Name = Name;
            currentProduct.Price = Price;
            currentProduct.Description = Description;
        }

        public ICommand SaveProductCommand => new Command(
            async () =>
            {
                SaveProductState();
                await stocklistService.SaveProduct(currentProduct);
                await navigation.PushAsync(new MainView());
            }
            );

        public ICommand DeleteProductCommand => new Command<Product>(
            async (Product product) =>
            {
                product.SubCategory.Products.Remove(product);
                //currentProduct.SubCategory.Products.Remove(currentProduct);
                //await stocklistService.SaveCategoryList(product.)
                await RefreshProducts();
            }
            );
    }
}
