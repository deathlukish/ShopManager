using ShopManager.Command;
using ShopManager.EFClient;
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
            DataCart.AcceptChanges();
            int index = _dataCart
                .AsEnumerable()
                .Select(col => col.Field<string>("nameProd"))                
                .ToList()                
                .FindIndex(b => b == _selectedProd.Row.Field<string>("nameProd"));
            if (index == -1)
            {
                _dataCart.Rows.Add(_selectedProd.Row.ItemArray).SetField("Count", 1);
             
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
           
        }
        private void MessageOfEvent(string text)
        {
            MessageText = text;
        }
        private void OnDelClient(object p)
        {
            SelectedClient.Row.Delete();
     
        }
        private void LoadBases()
        {
      
           
        }
        public MainWindowViewModel()
        {
            EFClientBase dbcontext = new();
            var a = dbcontext.Products.ToList();
            var b = dbcontext.Cart.ToList();
            
            dbcontext.Clients.Add(new Client()
            {
                FirstName = "sd",
                MidleName = "sd",
                LastName = "sd",
                NumPhone = "2222",
                Email = "dsdsdsds",
              
               
            });
            dbcontext.SaveChanges();
            var c = dbcontext.Clients.ToList();
        }
    }
}
