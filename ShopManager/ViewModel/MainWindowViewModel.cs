using ShopManager.Command;
using System.Windows;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _statusString;
        public string StatusString
        {

            get => _statusString;
            set => Set(ref _statusString, value);
        }
        public ICommand command { get; }
        private bool CanClose(object p) => true;
        private void OnClose(object p)
        {
            CreateBase createBase = new();
            createBase.CreateAccessBase(Message);
               
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
