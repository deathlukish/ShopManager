using ShopManager.Command;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {


        private DataTable _dataClient;
        private DataTable _dataProd;
        private DataTable _dataCart;
        private DataRowView _selectedClient;
        private DataSet _dataSet;
        private ClientsBase clientsBase;
        private ProductBase ProductBase;
        private string _message;
        public DataTable DataProd
        {
            get => _dataProd;
            set => Set(ref _dataProd, value);

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
        public ICommand LoadBase { get; }
        public ICommand AddBase { get; }
        private bool CanLoadBase(object p) => true;
        private void OnLoadBase(object p) => GetBase();
        private bool CanDelCLient(object p) => true;
        private bool CanAddBase(object p) => true;
        private void OnAddBase(object p)
        {
            CreateBase createBase = new();
            createBase._update += MessageOfEvent;
            createBase.CreateBases();


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
        private void GetBase()
        {



        }


        public MainWindowViewModel()
        {
            
            ProductBase = new();
            clientsBase = new();
            ProductBase._update += MessageOfEvent;
            clientsBase._update += MessageOfEvent;
            CommandSave = new RelayCommand(OnSave, CanSave);
            DelClient = new RelayCommand(OnDelClient, CanDelCLient);
            LoadBase = new RelayCommand(OnLoadBase, CanLoadBase);
            AddBase = new RelayCommand(OnAddBase, CanAddBase);
            DataTableClient = clientsBase.PrepeareBaseClients();
            DataProd = ProductBase.GetProducts();
        }


    }
}
