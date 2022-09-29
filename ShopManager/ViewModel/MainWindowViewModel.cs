using ShopManager.Command;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {

        private DataRowView _selectedProd;
        private DataTable _dataClient;
        private DataTable _dataProd;
        private DataTable _dataCart;
        private DataRowView _selectedClient;
        private DataSet _dataSet;
        private ClientsBase clientsBase;
        private ProductBase ProductBase;
        private string _message;
        private DataRowView _SelectedProdInCart;
        public DataRowView SelectedProdInCart
        {
            get => _SelectedProdInCart;
            set => Set(ref _SelectedProdInCart, value);
        }
        public DataTable DataProd
        {
            get => _dataProd;
            set => Set(ref _dataProd, value);

        }
        public DataRowView SelectedProd
        {
            get => _selectedProd;
            set => Set(ref _selectedProd, value);

        }
        public DataTable DataCart
        {
            get => _dataCart;
            set => Set(ref _dataCart, value);

        }
        public DataRowView SelectedClient
        {
            get
            {
                if (_selectedClient != null)
                {
                    DataCart = ProductBase.GetCart(_selectedClient?.Row?.Field<string>("eMail"));
                }
                return _selectedClient;
            }
            set => Set(ref _selectedClient, value);
        }
        public string MessageText
        {
            get => _message;
            set => Set(ref _message, value);

        }
        public DataTable DataTableClient
        {
            get => _dataClient;
            set => Set(ref _dataClient, value);
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
        public ICommand AddBase { get; }
        public ICommand AddToCart { get; }
        public ICommand DelFromCart { get; }
        private bool CanAddToCart(object p) => true;
        private bool CanDelCLient(object p) => true;
        private bool CanAddBase(object p) => true;
        private void OnAddBase(object p)
        {
            CreateBase createBase = new();
            createBase._update += MessageOfEvent;
            createBase.CreateBases();
            LoadBases();
        }
        private void OnAddToCart(object p)
        {          
            int index = _dataCart
                .AsEnumerable()
                .Select(col => col.Field<string>("nameProd"))                
                .ToList()                
                .FindIndex(b => b == _selectedProd.Row.Field<string>("nameProd"));
            if (index == -1)
            {
                _dataCart.Rows.Add(_selectedProd.Row.ItemArray).SetField("Count", 1);
                ProductBase.Save();
            }
            else
            {
                var a = _dataCart.Rows[index].Field<int>("Count");
                _dataCart.Rows[index].SetField("Count", ++a);
            }
        }
        private bool CanDelFromCart(object p) => true;
        private void OnDelFromCart(object p)
        {
            SelectedProdInCart.Row.Delete();
            ProductBase.Save();
        }
        private void MessageOfEvent(string text)
        {
            MessageText = text;
        }
        private void OnDelClient(object p)
        {
            SelectedClient.Row.Delete();
            clientsBase.Save();
        }
        private void LoadBases()
        {
            DataTableClient = clientsBase.PrepeareBaseClients();
            DataProd = ProductBase.GetProducts();
        }
        public MainWindowViewModel()
        {            
            ProductBase = new();
            clientsBase = new();
            ProductBase._update += MessageOfEvent;
            clientsBase._update += MessageOfEvent;
            CommandSave = new RelayCommand(OnSave, CanSave);
            DelClient = new RelayCommand(OnDelClient, CanDelCLient);
            AddBase = new RelayCommand(OnAddBase, CanAddBase);
            AddToCart = new RelayCommand(OnAddToCart, CanAddToCart);
            DelFromCart = new RelayCommand(OnDelFromCart, CanDelFromCart);
            LoadBases();
        }
    }
}
