using ShopManager.Command;
using ShopManager.EFClient;
using ShopManager.EFobject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {
        private DataProvader _dataProvader;
        private Product _selectedProd;
        private ObservableCollection<Client> _dataClient;
        private Client _selectedClient;
        private List<Product> _dataProd;
        private ObservableCollection<Cart> _dataCart;
        private string _message;
        private Product _SelectedProdInCart;
        public Product SelectedProdInCart
        {
            get => _SelectedProdInCart;
            set => Set(ref _SelectedProdInCart, value);
        }
        public List<Product> DataProd
        {
            get => _dataProd;
            set => Set(ref _dataProd, value);

        }
        public Product SelectedProd
        {
            get => _selectedProd;
            set => Set(ref _selectedProd, value);

        }
        public ObservableCollection<Cart> DataCart
        {
            get => _dataCart;
            set => Set(ref _dataCart, value);

        }
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                Set(ref _selectedClient, value);

                if (SelectedClient != null)
                {
                    DataCart = new ObservableCollection<Cart>();
                    foreach (var item in _dataProvader.GetGart(SelectedClient.Email))
                    {
                        DataCart.Add(item);
                    }
                }
            }
        }
        public string MessageText
        {
            get => _message;
            set => Set(ref _message, value);

        }
        public ObservableCollection<Client> DataClient
        {
            get => _dataClient;
            set => Set(ref _dataClient, value);
        }
        public ICommand CommandSave { get; }
        private bool CanSave(object p) => true;
        private async void OnSave(object p)
        {
            //_dataProvader.SaveOBS(DataClient);
            _dataProvader.SaveBase();
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
            LoadBases();
        }
        private void OnAddToCart(object p)
        {
            DataCart.Add(new Cart()
            {
                Count = 0,
                eMail = _selectedClient.Email,
                idProd = _selectedProd.id
            }
            );
            _dataProvader.SaveOBS(DataCart);
            _dataProvader.SaveBase();
            //DataCart.Add(new Cart()
            //{
            //    Count = 0,
            //    eMail = _selectedClient.Email,
            //    idProd = _selectedProd.id
            //}
            //); 
            //_dbcontext.SaveChanges();
            //_dbcontext.Cart.
            //DataCart.AcceptChanges();
            //int index = _dataCart
            //    .AsEnumerable()
            //    .Select(col => col.Field<string>("nameProd"))
            //    .ToList()
            //    .FindIndex(b => b == _selectedProd.Row.Field<string>("nameProd"));
            //if (index == -1)
            //
            //    _dataCart.Rows.Add(_selectedProd.Row.ItemArray).SetField("Count", 1);

            //}
            //else
            //{
            //    var a = _dataCart.Rows[index].Field<int>("Count");
            //    _dataCart.Rows[index].SetField("Count", ++a);
            //}
        }
        private bool CanDelFromCart(object p) => true;
        private void OnDelFromCart(object p)
        {
            // SelectedProdInCart.Row.Delete();

        }
        private void MessageOfEvent(string text)
        {
            MessageText = text;
        }
        private void OnDelClient(object p)
        {
            // SelectedClient.Row.Delete();

        }
        private void LoadBases()
        {


        }
        public MainWindowViewModel()
        {
            _dataProvader = new();
            DataClient = new();
            foreach (var item in _dataProvader.GetClient())
            {
                DataClient.Add(item);
            }
            CommandSave = new RelayCommand(OnSave, CanSave);
            AddToCart = new RelayCommand(OnAddToCart, CanAddToCart);
            DataProd = new();
            foreach (var item in _dataProvader.GetProdt())
            {
                DataProd.Add(item);
            }
        }
    }
}
