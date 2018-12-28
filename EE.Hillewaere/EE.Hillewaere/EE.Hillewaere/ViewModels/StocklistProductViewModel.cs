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
    public class StocklistProductViewModel : INotifyPropertyChanged
    {
        private IStocklistService stocklistService;
        private SubCategory currentSubCategory;
        private INavigation navigation;

        public StocklistProductViewModel(SubCategory subCategory, INavigation navigation, IStocklistService slService)
        {
            this.navigation = navigation;
            this.currentSubCategory = subCategory;
            stocklistService = slService;
            RefreshProducts();
        }

        private async Task RefreshProducts()
        {
            if (currentSubCategory != null)
            {
                PageTitle = currentSubCategory.Name;
                var products = await stocklistService.GetProductListById(currentSubCategory.Id);
                Products = null;
                Products = new ObservableCollection<Product>(products);
            }
            else
            {
                PageTitle = "New Subcategory List";
                currentSubCategory = new SubCategory();
                currentSubCategory.Id = Guid.NewGuid();
                currentSubCategory.Products = new List<Product>();
            }
            LoadProductState();
        }

        private void LoadProductState()
        {
            Name = currentSubCategory.Name;
            Products = new ObservableCollection<Product>(currentSubCategory.Products);
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

        public ICommand ViewProductCommand => new Command<Product>(
            async (Product product) =>
            {
                if (product == null)
                {
                    product = new Product();
                    product.SubCategory = new SubCategory();
                    product.SubCategory.Name = currentSubCategory.Name;
                    product.SubCategory.Id = currentSubCategory.Id;
                    product.Category = new Category();
                    product.Category.Name = currentSubCategory.Category.Name;
                    product.Category.Id = currentSubCategory.Category.Id;
                }
                await navigation.PushAsync(new StocklistEditProductView(product));
            });

        public ICommand DeleteProductCommand => new Command<Product>(
        async (Product product) =>
        {
            Debug.WriteLine(product.Name);
            await stocklistService.DeleteProduct(product.Id);
            var products = await stocklistService.GetProductListById(currentSubCategory.Id);
            Products = null;
            Products = new ObservableCollection<Product>(products);
        });

    }
}
