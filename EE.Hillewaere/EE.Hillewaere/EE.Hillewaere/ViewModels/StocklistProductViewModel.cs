using EE.Hillewaere.Domain.Models;
using EE.Hillewaere.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EE.Hillewaere.ViewModels
{
    public class StocklistProductViewModel : INotifyPropertyChanged
    {
        private CategoriesInMemoryService categoryService;
        private SubCategory currentSubCategory;
        private INavigation navigation;

        public StocklistProductViewModel(SubCategory subCategory, INavigation navigation)
        {
            this.navigation = navigation;
            this.currentSubCategory = subCategory;
            categoryService = new CategoriesInMemoryService();
            RefreshSubCategories();
        }

        private async Task RefreshSubCategories()
        {
            if (currentSubCategory != null)
            {
                PageTitle = currentSubCategory.Name;
                //currentSubCategory = await categoryService.GetSubCategoryById(currentSubCategory.Id);
            }
            else
            {
                PageTitle = "New Subcategory List";
                currentSubCategory = new SubCategory();
                currentSubCategory.Id = Guid.NewGuid();
                currentSubCategory.Products = new List<Product>();
            }
            LoadSubCategoryState();
        }

        private void LoadSubCategoryState()
        {
            Name = currentSubCategory.Name;
            products = new ObservableCollection<Product>(currentSubCategory.Products);
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

    }
}
