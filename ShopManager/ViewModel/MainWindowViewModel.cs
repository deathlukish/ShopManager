using ShopManager.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {

        public ICommand command { get; }
        private bool CanClose(object p) => true;
        private void OnClose(object p) => MessageBox.Show("re");


        public MainWindowViewModel()
        {

            command = new RelayCommand(OnClose, CanClose);
        
        }

    }
}
