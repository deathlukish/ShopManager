using Microsoft.EntityFrameworkCore;
using ShopManager.EFClient;
using System.Collections.Generic;

namespace ShopManager.EFobject
{
    internal class DataProvader
    {
        private readonly EFClientBase _dbContext;
        public DataProvader()
        {
            _dbContext = new();
            _dbContext.Clients.Load();
            _dbContext.Products.Load();
            _dbContext.Cart.Load();

        }
        public IEnumerable<Client> GetClient()
        {
            return _dbContext.Clients;
        }
        public void SaveBase()
        {
            _dbContext.SaveChanges();
        }
    }
}
