﻿using EE.Hillewaere.Domain.Models;
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
        private CategoriesInMemoryService categoryService;
        private Category currentCategory;
        private INavigation navigation;

        public StocklistSubCategoryViewModel(Category category, INavigation navigation)
        {
            this.navigation = navigation;
            this.currentCategory = category;
            categoryService = new CategoriesInMemoryService();
            RefreshCategories();
        }

        private async Task RefreshCategories()
        {
            if (currentCategory != null)
            {
                PageTitle = currentCategory.Name;
                currentCategory = await categoryService.GetById(currentCategory.Id);
            }
            else
            {
                PageTitle = "New Category List";
                currentCategory = new Category();
                currentCategory.Id = Guid.NewGuid();
                currentCategory.SubCategories = new List<SubCategory>();
            }
            LoadCategoryState();
        }

        private void LoadCategoryState()
        {
            Name = currentCategory.Name;
            SubCategories = new ObservableCollection<SubCategory>(currentCategory.SubCategories);
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
                navigation.PushAsync(new StocklistProductView(subCategory));
                Debug.WriteLine(subCategory.Name);
            });
    }
}
