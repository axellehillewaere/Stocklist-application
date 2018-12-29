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
    public class OrderSubCategoryViewModel
    {
        private IStocklistService stocklistService;
        private Category currentCategory;
        private INavigation navigation;

        public OrderSubCategoryViewModel(Category category, INavigation navigation, IStocklistService slService)
        {
            this.navigation = navigation;
            this.currentCategory = category;
            stocklistService = slService;
            Initialize();
        }

        private async Task Initialize()
        {
            PageTitle = "New Order - Subcategories";
            var subCategories = await stocklistService.GetSubCategoryListById(currentCategory.Id);
            SubCategories = null;
            SubCategories = new ObservableCollection<SubCategory>(subCategories);
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

        public ICommand ViewProductsCommand => new Command<SubCategory>(
           (SubCategory subCategory) =>
           {
               Debug.WriteLine(subCategory.Name);
               navigation.PushAsync(new OrderProductView(subCategory));
           });

    }
}
