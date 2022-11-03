﻿using ShopManager.Command;
using ShopManager.EFClient;
using ShopManager.EFobject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {
        private EFClientBase _datacontext;
        private Product _selectedProd;
        private ObservableCollection<Client> _dataClient;
        private Client _selectedClient;
        private List<Product> _dataProd;
        private ObservableCollection<ProdInCart> _dataCart;
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
        public ObservableCollection<ProdInCart> DataCart
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
                    DataCart = new ObservableCollection<ProdInCart>();
                    foreach (var item in _datacontext.Cart.Where(e => e.eMail == SelectedClient.Email).Join(_datacontext.Products,
                    p => p.idProd,
                    c => c.id,
                    (p, c) => new ProdInCart
                    {
                        Name = c.nameProd,
                        Count = p.Count
                    }))
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
            _datacontext.Clients.AddRange(DataClient);
            _datacontext.SaveChanges();
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
            _datacontext.Cart.Add(new Cart()
            {
                eMail = SelectedClient.Email,
                idProd = SelectedProd.id,
                Count = 1

            });
            _datacontext.SaveChanges();
        }
        private bool CanDelFromCart(object p) => true;
        private void OnDelFromCart(object p)
        {


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
            _datacontext = new();
            DataClient = new();
            foreach (var item in _datacontext.Clients)
            {
                DataClient.Add(item);
            }
            CommandSave = new RelayCommand(OnSave, CanSave);
            AddToCart = new RelayCommand(OnAddToCart, CanAddToCart);
            DataProd = new();
            foreach (var item in _datacontext.Products)
            {
                DataProd.Add(item);
            }
        }
    }
}
