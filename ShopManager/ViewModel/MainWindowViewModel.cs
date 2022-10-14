using Microsoft.EntityFrameworkCore;
using ShopManager.Command;
using ShopManager.EFClient;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private BindingList<Client> _dataClient;
        private Client _selectedClient;
        private BindingList<Product> _dataProd;
        private List<Cart> _dataCart;
        private string _message;
        private Product _SelectedProdInCart;
        public Product SelectedProdInCart
        {
            get => _SelectedProdInCart;
            set => Set(ref _SelectedProdInCart, value);
        }
        public BindingList<Product> DataProd
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
                    //DataCart = ProductBase.GetCart(_selectedClient?.Row?.Field<string>("eMail"));
                }
                return _selectedClient;
            }
            set
            {
                Set(ref _selectedClient, value);
                if (SelectedClient != null)
                {
                    _dbcontext.Cart.Local.Where(e => e.eMail == SelectedClient.Email);
                    DataCart = _dbcontext.Cart.ToList();
                    _dbcontext.SaveChanges();
                }
            }
        }
        public string MessageText
        {
            get => _message;
            set => Set(ref _message, value);

        }
        public BindingList<Client> DataTableClient
        {
            get => _dataClient;
            set => Set(ref _dataClient, value);
        }
        public ICommand CommandSave { get; }
        private bool CanSave(object p) => true;
        private async void OnSave(object p)
        {
            _dbcontext.SaveChanges();



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
            _dbcontext.Cart.Local.Add(new Cart()
            {
                Count = 0,
                eMail = _selectedClient.Email,
                idProd = _selectedProd.id
            }
            );
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
            
            _dbcontext = new();
            _dbcontext.Clients.Load();
            _dbcontext.Products.Load();
            //_dbcontext.Cart.Load();
            DataTableClient = _dbcontext.Clients.Local.ToBindingList();
            DataProd = _dbcontext.Products.Local.ToBindingList();
            AddToCart = new RelayCommand(OnAddToCart,CanAddToCart);
            
            //_dbcontext.Cart.Load();
            CommandSave = new RelayCommand(OnSave, CanSave);
        }
    }
}
