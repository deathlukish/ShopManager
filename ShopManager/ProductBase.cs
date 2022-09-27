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
        private SqlConnection con;
        public ProductBase()
        {
            da = new SqlDataAdapter();
            dt = new DataTable();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectProducts"].ConnectionString);            
            da.UpdateCommand = new SqlCommand("SELECT * FROM Cart", con);
        }
        /// <summary>
        /// Получить все продукты клиента из базы
        /// </summary>
        /// <returns></returns>
        public  DataTable GetCart(string eMail)
        {
            da.DeleteCommand = new SqlCommand($"DELETE FROM Cart WHERE id = @rt", con);
            da.InsertCommand = new SqlCommand($"INSERT INTO Cart(eMail,idProd) VALUES('{eMail}', @Prod) ", con);
            da.InsertCommand.Parameters.Add("@Prod", SqlDbType.Int, 10, "ID");
            da.DeleteCommand.Parameters.Add("@rt", SqlDbType.Int, 10, "rt");
            dt.Clear();
            string SelectCommand = $"SELECT " +               
                $"Products.idProd as 'ID',"+
                $"Products.nameProd as 'Наименование'," +
                $"Products.Price as 'Цена'," +
                $"Cart.id as 'rt'" +
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
        public DataTable GetProducts()
        { 
            DataTable dt = new DataTable();
            dt.Clear();
            string SelectCommand = $"SELECT " +
                $"Products.idProd as 'ID'," +
                $"Products.nameProd as 'Наименование'," +
                $"Products.Price as 'Цена'" +
                $"FROM Products";
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
            _update?.Invoke("Данные успешно внесены");
        }
    }
}

