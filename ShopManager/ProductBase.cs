using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManager
{
    internal class ProductBase
    {
        public event Action<string>? _update;
        private DataTable dt;
        private SqlDataAdapter da;
       // private SqlCommandBuilder commandBuilder;
        private SqlConnection con;
        public ProductBase()
        {
            //INSERT INTO[dbo].[Products] ([idProd],[nameProd], [Price]) VALUES(1,N'Утюг',100)
            da = new SqlDataAdapter();
            dt = new DataTable();
          //  commandBuilder = new SqlCommandBuilder(da);
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectProducts"].ConnectionString);
            da.InsertCommand = new SqlCommand("INSERT INTO Cart(eMail,idProd) VALUES(@id, 100) ",con);
            da.UpdateCommand = new SqlCommand("SELECT * FROM Cart", con);
            da.InsertCommand.Parameters.Add("@id",SqlDbType.Char,20, "Наименование");
        }
        /// <summary>
        /// Получить все продукты клиента из базы
        /// </summary>
        /// <returns></returns>
        public  DataTable GetProducts(string eMail)
        {
            dt.Clear();
            string SelectCommand = $"SELECT " +
                $"Products.nameProd as 'Наименование'," +
                $"Products.Price as 'Цена'" +
                $"FROM Cart, Products WHERE Cart.eMail = '{eMail}' and Products.idProd = Cart.idProd ";
            GetProd();
            void GetProd()
            {                
                try
                {                    
                    da.SelectCommand = new SqlCommand(SelectCommand, con);
                    da.Fill(dt);
                }

                catch (Exception e)   
                {
                    _update?.Invoke(e.Message);
                }
            }
            return dt;
        }

        public void Save()
        {
            try
            {
                da.Update(dt);
            }
            catch (Exception e)
            {
                _update?.Invoke(e.Message);
            }
        }


    }
}

