using Microsoft.EntityFrameworkCore;
using ShopManager.EFClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShopManager.EFobject
{
    internal class WorkBase
    {
        private EFClientBase _dbcontext;
        public WorkBase()
        {
            _dbcontext = new();
            _dbcontext.Clients.Load();
            _dbcontext.Cart.Load();
            _dbcontext.Products.Load();
        }
        public BindingList<Client> GetClient()
        {
            var a = _dbcontext.Clients.Local.ToBindingList();
          return _dbcontext.Clients.Local.ToBindingList();
        }
    }
}
