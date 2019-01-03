using EE.Hillewaere.Domain.Models;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace EE.Hillewaere.ViewModels
{
    public class PreviousOrdersViewModel : INotifyPropertyChanged
    {
        private INavigation navigation;

        public PreviousOrdersViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            Initialize();
        }

        private void Initialize()
        {
            PageTitle = "Previous Orders";
            LoadFile();
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

        private async void LoadFile()
        {
            string fileName = "orderlist.xml";
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
                        var serializer = new XmlSerializer(typeof(OrderList));
                        OrderList orderList = (OrderList)serializer.Deserialize(reader);
                        this.Name = orderList.Name;
                        this.Price = orderList.Price;

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error reading location: {ex.Message}");
                }
            }
        }
    }
}
