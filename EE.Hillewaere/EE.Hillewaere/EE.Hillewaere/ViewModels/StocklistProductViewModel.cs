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
using System.Xml.Serialization;
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
            LoadFile();

            RefreshProducts();

            MessagingCenter.Subscribe(this, MessageNames.ProductSaved,
                async (StocklistEditProductViewModel sender, Product product) =>
                {
                    Products = new ObservableCollection<Product>(await stocklistService.GetProductListById(currentSubCategory.Id));
                });
        }

        private async void LoadFile()
        {
            string fileName = "products.xml";
            IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
            ExistenceCheckResult result = await folder.CheckExistsAsync(fileName);
            if (result == ExistenceCheckResult.FileExists)
            {
                try
                {
                    IFile file = await folder.GetFileAsync(fileName);
                    string text = await file.ReadAllTextAsync();
                    using (var reader = new StringReader(text))
                    {
                        var serializer = new XmlSerializer(typeof(Product));
                        Product product = (Product)serializer.Deserialize(reader);
                        this.currentSubCategory.Products.Add(product);
                        //this.Products.Add(product);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error reading location: {ex.Message}");
                }
            }
        }
        private async Task RefreshProducts()
        {
            if (currentSubCategory != null)
            {
                PageTitle = "Overview Stocklist - " + currentSubCategory.Name;
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
        #region Properties
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
        #endregion

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
