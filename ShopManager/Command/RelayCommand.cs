using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.Command
{
    internal class RelayCommand : BaseCommand

    {
        private readonly Action<object> _Action;
        private readonly Func<object, bool> _CanExecute;
        public RelayCommand(Action<object> Action, Func<object, bool> CanExecute)
        {
            _Action = Action ?? throw new ArgumentNullException(nameof(Action));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _Action(parameter);
    }


}

