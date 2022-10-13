using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.EFClient
{
    internal class Cart
    {
        public int id { get; set; }
        public string eMail { get; set; }
        public int idProd { get; set; }
        public int Count { get; set; }


    }
}
