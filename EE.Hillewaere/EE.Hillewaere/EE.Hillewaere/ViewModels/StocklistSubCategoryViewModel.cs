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
    public class StocklistSubCategoryViewModel : INotifyPropertyChanged
    {
        private IStocklistService stocklistService;
        private Category currentCategory;
        private INavigation navigation;

        public StocklistSubCategoryViewModel(Category category, INavigation navigation, IStocklistService slService)
        {
            this.navigation = navigation;
            this.currentCategory = category;
            stocklistService = slService;
            RefreshSubCategories();
        }

        private async Task RefreshSubCategories()
        {
            if (currentCategory != null)
            {
                PageTitle = "Overview Stocklist - " + currentCategory.Name;
                var subCategories = await stocklistService.GetSubCategoryListById(currentCategory.Id);
                SubCategories = null;
                SubCategories = new ObservableCollection<SubCategory>(subCategories);
            }
            else
            {
                PageTitle = "New Category List";
                currentCategory = new Category();
                currentCategory.Id = Guid.NewGuid();
                currentCategory.SubCategories = new List<SubCategory>();
            }
            LoadSubCategoryState();
        }

        private void LoadSubCategoryState()
        {
            Name = currentCategory.Name;
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

        private ObservableCollection<Product> prod;
        public ObservableCollection<Product> Prod
        {
            get { return prod; }
            set
            {
                prod = value;
                RaisePropertyChanged(nameof(Prod));
            }
        }
        #endregion

        public ICommand ViewProductsCommand => new Command<SubCategory>(
            (SubCategory subCategory) =>
            {
                navigation.PushAsync(new StocklistProductView(subCategory));
            });

        public ICommand DeleteSubCategoryCommand => new Command<SubCategory>(
        async (SubCategory subCategory) =>
        {
            await stocklistService.DeleteSubCategory(subCategory.Id);
            var subCategories = await stocklistService.GetSubCategoryListById(currentCategory.Id);
            SubCategories = null;
            SubCategories = new ObservableCollection<SubCategory>(subCategories);
        });
    }
}
