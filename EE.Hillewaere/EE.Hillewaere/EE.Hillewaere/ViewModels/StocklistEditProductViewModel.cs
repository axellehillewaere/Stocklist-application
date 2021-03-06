﻿using EE.Hillewaere.Constants;
using EE.Hillewaere.Domain.Models;
using EE.Hillewaere.Domain.Services;
using EE.Hillewaere.Domain.Validators;
using EE.Hillewaere.Views;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Essentials;
using Android.Widget;

namespace EE.Hillewaere.ViewModels
{
    public class StocklistEditProductViewModel : INotifyPropertyChanged
    {
        private IStocklistService stocklistService;
        private Product currentProduct;
        private INavigation navigation;
        private ProductValidator productValidator;
        public event PropertyChangedEventHandler PropertyChanged;


        public StocklistEditProductViewModel(Product product, INavigation navigation, IStocklistService slService)
        {
            this.navigation = navigation;
            this.currentProduct = product;
            stocklistService =slService;
            productValidator = new ProductValidator();
            RefreshProducts();

        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task RefreshProducts()
        { 
            if (currentProduct.Name != null)
            {
                PageTitle = "Overview Stocklist - Edit " + currentProduct.Name;;
            }
            else
            {
                PageTitle = "Overview Stocklist - New Product";
            }
            LoadProductsState();
        }

        private void LoadProductsState()
        {
            Name = currentProduct.Name;
            Price = currentProduct.Price;
            Code = currentProduct.Code;
            ExtraInfo = currentProduct.ExtraInfo;
            SubCategoryName = currentProduct.SubCategory.Name;
            Category = currentProduct.Category;
            Products = null;
            Products = new ObservableCollection<Product>(stocklistService.GetProductListBySub(currentProduct.SubCategory.Name).Result);
            LoadFile();
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
                        Products.Add(product);

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error reading location: {ex.Message}");
                }
            }
        }

        #region properties
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

        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                RaisePropertyChanged(nameof(Code));
            }
        }

        private string extraInfo;
        public string ExtraInfo
        {
            get { return extraInfo; }
            set
            {
                extraInfo = value;
                RaisePropertyChanged(nameof(ExtraInfo));
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

        private string nameError;
        public string NameError
        {
            get { return nameError; }
            set
            {
                nameError = value;
                RaisePropertyChanged(nameof(NameError));
                RaisePropertyChanged(nameof(NameErrorVisible));
            }
        }

        private string priceError;
        public string PriceError
        {
            get { return priceError; }
            set
            {
                priceError = value;
                RaisePropertyChanged(nameof(PriceError));
                RaisePropertyChanged(nameof(PriceErrorVisible));
            }
        }

        private string codeError;
        public string CodeError
        {
            get { return codeError; }
            set
            {
                codeError = value;
                RaisePropertyChanged(nameof(CodeError));
                RaisePropertyChanged(nameof(CodeErrorVisible));
            }
        }

        private string extraInfoError;
        public string ExtraInfoError
        {
            get { return extraInfoError; }
            set
            {
                extraInfoError = value;
                RaisePropertyChanged(nameof(ExtraInfoError));
                RaisePropertyChanged(nameof(ExtraInfoErrorVisible));
            }
        }

        public bool NameErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(NameError); }
        }

        public bool PriceErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(PriceError); }
        }

        public bool CodeErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(CodeError); }
        }

        public bool ExtraInfoErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(ExtraInfoError); }
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

        private SubCategory subcategory;
        public SubCategory SubCategory
        {
            get { return subcategory; }
            set
            {
                subcategory = value;
                RaisePropertyChanged(nameof(SubCategory));
            }
        }

        private Category category;
        public Category Category
        {
            get { return category; }
            set
            {
                category = value;
                RaisePropertyChanged(nameof(Category));
            }
        }

        private ObservableCollection<SubCategory> subCategories;
        public ObservableCollection<SubCategory> SubCategories
        {
            get { return subCategories; }
            set
            {
                subCategories = value;
                RaisePropertyChanged(nameof(SubCategories));
            }
        }

        private string subCategoryName;
        public string SubCategoryName
        {
            get { return subCategoryName; }
            set
            {
                subCategoryName = value;
                RaisePropertyChanged(nameof(SubCategoryName));
            }
        }
        #endregion

        private void SaveProductState()
        {
            currentProduct.Name = Name;
            currentProduct.Price = Price;
            currentProduct.Code = Code;
            currentProduct.ExtraInfo = ExtraInfo;
            currentProduct.SubCategory.Name = SubCategoryName;
        }

        public ICommand SaveProductCommand => new Command(
            async () =>
            {
                SaveProductState();
                if (Validate(currentProduct))
                {
                    await DependencyService.Get<ISoundPlayer>().PlaySound();
                    SaveFile();
                    await stocklistService.SaveProduct(currentProduct);
                    MessagingCenter.Send<StocklistEditProductViewModel, Product>(this,
                        MessageNames.ProductSaved, currentProduct);
                }
                else
                {
                    try
                    {
                        Vibration.Vibrate();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
            }
            );


        private async void SaveFile()
        {
            var serializer = new XmlSerializer(typeof(Product));
            string productAsXml = "";
            using (var stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    serializer.Serialize(writer, currentProduct);
                    productAsXml = stringWriter.ToString();
                }
            }
            IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync("products.xml", CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(productAsXml);
            Debug.WriteLine(folder);
            Debug.WriteLine(file);
        }

        private bool Validate(Product product)
        {
            var validationResult = productValidator.Validate(product);
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(product.Name))
                {
                    NameError = error.ErrorMessage;
                }
                if (error.PropertyName == nameof(product.Price))
                {
                    PriceError = error.ErrorMessage;
                }
                if (error.PropertyName == nameof(product.Code))
                {
                    CodeError = error.ErrorMessage;
                }
                if (error.PropertyName == nameof(product.ExtraInfo))
                {
                    ExtraInfoError = error.ErrorMessage;
                }
            }
            return validationResult.IsValid;
        }
    }
}
