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
        private Client _selectedClient;
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
                MessageBox.Show(SelectedClient.Email);
            }
        }
        public ICommand command { get; }
        private bool CanClose(object p) => true;
        private async void OnClose(object p)
        {

            ClientsBase clientsBase = new();
            Clients = await clientsBase.GetClients();
            //CreateBase createBase = new();
            //createBase.CreateAccessBase(Message);
            Client client = new Client()
            {
                FirstName = "fds",
                MiddleName = "sfsa",
                LastName = "sadsdas",
                NumPhone = "+3432423",
                Email = "sdaa@ccc.ru"
            };
            //clientsBase.AddClient(client);
            clientsBase.DelClient(client);

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

            command = new RelayCommand(OnClose, CanClose);
        
        }

    }
}
