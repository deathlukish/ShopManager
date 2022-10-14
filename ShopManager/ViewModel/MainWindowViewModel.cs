using Microsoft.EntityFrameworkCore;
using ShopManager.Command;
using ShopManager.EFClient;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {
        private EFClientBase _dbcontext;
        private Product _selectedProd;
        private ObservableCollection<Client> _dataClient;
        private Client _selectedClient;
        private List<Product> _dataProd;
        private List<Cart> _dataCart;
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
        public List<Cart> DataCart
        {
            get => _dataCart;
            set => Set(ref _dataCart, value);

        }
        public Client SelectedClient
        {
            get
            {
                if (_selectedClient != null)
                {

                    DataCart = _dbcontext.Cart.Local.Where(e=>e.eMail==_selectedClient.Email).ToList();
                    //DataCart = ProductBase.GetCart(_selectedClient?.Row?.Field<string>("eMail"));
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
        public ObservableCollection<Client> DataTableClient
        {
            get => _dataClient;
            set => Set(ref _dataClient, value);
        }
        public ICommand CommandSave { get; }
        private bool CanSave(object p) => true;
        private async void OnSave(object p)
        {
       
         
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
            _dbcontext.SaveChanges();
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
            _dbcontext = new();
            var a =_dbcontext.Cart;
            _dbcontext.Clients.Load();
            _dbcontext.Cart.Load();
            //var a = dbcontext.Products.ToList();
            //var b = dbcontext.Cart.ToList();
            DataTableClient = _dbcontext.Clients.Local.ToObservableCollection();
            DataProd = _dbcontext.Products.ToList();
            //dbcontext.Clients.Add(new Client()
            //{
            //    FirstName = "sd",
            //    MidleName = "sd",
            //    LastName = "sd",
            //    NumPhone = "2222",
            //    Email = "dsdsdsds",


            //});
            //_dbcontext.SaveChanges();
            //var c = dbcontext.Clients.ToList();
            AddToCart = new RelayCommand(OnAddToCart,CanAddToCart);
        }
    }
}
