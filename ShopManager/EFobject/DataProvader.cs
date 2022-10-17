using Microsoft.EntityFrameworkCore;
using ShopManager.EFClient;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<Product> GetProdt()
        {
            return _dbContext.Products;
        }
        public void SaveBase()
        {
            _dbContext.SaveChanges();
            
        }
        public IEnumerable<Cart> GetGart(string email)
        {         
          return _dbContext.Cart.Where(e=>e.eMail==email);
        }
        public void SaveOBS (IEnumerable<Cart> values)
        {
            _dbContext.UpdateRange(values);
        }
    }
}
