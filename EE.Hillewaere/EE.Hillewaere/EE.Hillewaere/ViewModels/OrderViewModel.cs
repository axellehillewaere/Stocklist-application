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
    public class OrderViewModel
    {
        private IStocklistService stocklistService;
        private INavigation navigation;

        public OrderViewModel(INavigation navigation, IStocklistService slService)
        {
            this.navigation = navigation;
            stocklistService = slService;
            Initialize();
        }

        private async Task Initialize()
        {
            PageTitle = "New order - Categories";
            Categories = null;
            Categories = new ObservableCollection<Category>(stocklistService.GetCategoryList().Result);
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

        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                RaisePropertyChanged(nameof(Categories));
            }
        }
        #endregion

        public ICommand ViewSubCategoriesCommand => new Command<Category>(
            (Category category) =>
            {
                navigation.PushAsync(new OrderSubCategoryView(category));
                Debug.WriteLine(category.Name);
            });

        public ICommand ViewOrder => new Command(
            async () =>
            {
                await navigation.PushAsync(new OrderListView());
            });
            }
}
