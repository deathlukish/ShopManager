using Microsoft.Data.SqlClient;
using ShopManager.Command;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Windows;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {

        private DataTable _dataTable;
        private string _statusString;
        private ObservableCollection<Client> _clients;
        private Client _selectedClient;
        private ObservableCollection<Product> _products;
        public string StatusString
        {

            get => _statusString;
            set => Set(ref _statusString, value);
        }
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set=>Set(ref _clients, value);
        
        }
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                Set(ref _selectedClient, value);
                ProductBase productBase = new();
                Products = productBase.GetProducts(SelectedClient);
            }
        }
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => Set(ref _products, value);
        }
        public DataTable DataTable
        {
            get => _dataTable;
            set => Set(ref _dataTable, value);
        }
        public ICommand command { get; }
        private bool CanClose(object p) => true;
        private async void OnClose(object p)
        {

            ClientsBase clientsBase = new();
            Clients = await clientsBase.GetClients();
            CreateBase createBase = new();
            createBase.CreateAccessBase(Message);
            Client client = new Client()
            {
                FirstName = "fds",
                MiddleName = "sfsa",
                LastName = "sadsdas",
                NumPhone = "+3432423",
                Email = "sdaa@ccc.ru"
            };
            clientsBase.AddClient(client);
            //clientsBase.DelClient(client);
            ProductBase product = new();
            product.AddProduct(new Product
            {
                Email = "sdaa@ccc.ru",
                IdProd = 24,
                NameProd = "dfsdfsdfs"
            });
        }
        public void AddClient()
        {
        
        
        }


        private void Message(string a)
        {
            StatusString = a;
        
        }

        public MainWindowViewModel()
        {
            ClientsBase clientsBase = new();
            DataTable = clientsBase.Prepear();
            command = new RelayCommand(OnClose, CanClose);
        
        }

    }
}
