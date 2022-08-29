using ShopManager.Command;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {

        public ICommand command { get; }
        private bool CanClose(object p) => true;
        private void OnClose(object p)
        {
            CreateBase createBase = new();
            createBase.CreateAccessBase();
               
        }


        public MainWindowViewModel()
        {

            command = new RelayCommand(OnClose, CanClose);
        
        }

    }
}
