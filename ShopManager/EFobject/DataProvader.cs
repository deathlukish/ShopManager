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
        public IEnumerable<ProdInCart> GetGart(string email)
        {
            return _dbContext.Cart.Where(e => e.eMail == email).Join(_dbContext.Products,
                p => p.idProd,
                c => c.id,
                (p, c) => new ProdInCart
                {
                    Name = c.nameProd,
                    Count = p.Count
                });

               
        }      
    }
}
