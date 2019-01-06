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

        private ObservableCollection<OrderList> orderLists;
        public ObservableCollection<OrderList> OrderLists
        {
            get { return orderLists; }
            set
            {
                orderLists = value;
                RaisePropertyChanged(nameof(OrderLists));
            }
        }
        #endregion

        private async void LoadFile()
        {
            string folderName = "orders";
            IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
            ExistenceCheckResult result = await folder.CheckExistsAsync(folderName);
            if (result == ExistenceCheckResult.FolderExists)
            {
                try
                {
                    IFolder orderFolder = await folder.GetFolderAsync(folderName);
                    IList<IFile> files = await orderFolder.GetFilesAsync();
                    OrderLists = new ObservableCollection<OrderList>();
                    foreach (var item in files)
                    {
                        string text = await item.ReadAllTextAsync();
                        using (var reader = new StringReader(text))
                        {
                            var serializer = new XmlSerializer(typeof(OrderList));
                            OrderList orderList = (OrderList)serializer.Deserialize(reader);
                            this.Name = orderList.Name;
                            this.Price = orderList.Price;
                            OrderLists.Add(orderList);
                        }
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
