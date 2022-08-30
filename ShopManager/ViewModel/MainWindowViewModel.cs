using ShopManager.Command;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _statusString;
        private ObservableCollection<Client> _clients;
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
        public ICommand command { get; }
        private bool CanClose(object p) => true;
        private void OnClose(object p)
        {
            //CreateBase createBase = new();
            //createBase.CreateAccessBase(Message);
            ClientsBase clientsBase = new();
            //clientsBase.AddClient(new Client()
            //    {
            //    FirstName = "fds",
            //    MiddleName = "sfsa",
            //    LastName = "sadsdas",
            //    NumPhone = "+3432423",
            //    Email = "sdaa@aaa.ru"
            //    });
            Clients = clientsBase.GetClients();
               
        }
        private void Message(string a)
        {
            StatusString = a;
        
        }

        public MainWindowViewModel()
        {

            command = new RelayCommand(OnClose, CanClose);
        
        }

    }
}
