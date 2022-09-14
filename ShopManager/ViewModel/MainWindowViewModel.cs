using Microsoft.Data.SqlClient;
using ShopManager.Command;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;
using System.Xml.Serialization;
using System;
using System.Diagnostics;

namespace ShopManager.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {
       

        private DataTable _dataTable;
        private string _statusString;
        private DataTable _dataProd;
        private ObservableCollection<Client> _clients;
        private DataRowView _selectedClient;
        private ObservableCollection<Product> _products;
        private DataSet _dataSet;
        private ClientsBase clientsBase;
        private ProductBase ProductBase;
        public string StatusString
        {

            get => _statusString;
            set => Set(ref _statusString, value);
        }
        public DataTable DataProd
        {
            get => _dataProd;
            set => Set(ref _dataProd, value);
        
        }
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set => Set(ref _clients, value);

        }
        public DataRowView SelectedClient
        {
            get
            {
                Temp();
                return _selectedClient;
            }
            set => Set(ref _selectedClient, value);
               

            
        }
        private  void Temp()
        {

            if (_selectedClient != null)
            {
                DataProd = ProductBase.GetProducts(_selectedClient?.Row?.Field<string>("eMail"));
                //LoadProd(_selectedClient?.Row?.Field<string>("eMail"));
            }
        }
        
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => Set(ref _products, value);
        }
        public DataTable DataTableClient
        {
            get => _dataTable;
            set => Set(ref _dataTable, value);
        }
        public DataSet DataSetClient
        {
            get => _dataSet;
            set => Set(ref _dataSet, value);

        }
        public ICommand CommandSave { get; }
        private bool CanSave(object p) => true;
        private async void OnSave(object p)
        {
            clientsBase.Save();
            ProductBase.Save();
        }
        public ICommand DelClient { get; }
        public ICommand LoadBase { get; }
        public ICommand AddBase { get; }
        private bool CanLoadBase(object p) => true;
        private void OnLoadBase(object p) => GetBase();
        private bool CanDelCLient(object p) => true;
        private bool CanAddBase(object p) => true;
        private void OnAddBase(object p)
        {
            CreateBase createBase = new();
            createBase.CreateAccessBase((w)=>MessageBox.Show(w));
            createBase.AddSqlBase();
        
        }
        private void OnDelClient(object p)
        {

            SelectedClient.Row.Delete();
            clientsBase.Save();

        }
        private async void GetBase()
        {

            DataTableClient =  await clientsBase.PrepeareBaseClients();
            
        }
        //private async void LoadProd(string eMail)
        //{
        //    DataProd =  await ProductBase.GetProducts(eMail);
        //}

        public MainWindowViewModel()
        {
            ProductBase = new();
            clientsBase = new();
            CommandSave = new RelayCommand(OnSave, CanSave);
            DelClient = new RelayCommand(OnDelClient, CanDelCLient);
            LoadBase = new RelayCommand(OnLoadBase, CanLoadBase);
            AddBase = new RelayCommand(OnAddBase, CanAddBase);

        }

    }
}
