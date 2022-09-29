﻿using Microsoft.Data.SqlClient;
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
        }
        /// <summary>
        /// Получить все продукты клиента из базы
        /// </summary>
        /// <returns>
        /// </returns>
        public  DataTable GetCart(string eMail)
        {
            da.InsertCommand = new SqlCommand($"INSERT INTO Cart(eMail,idProd,Count) VALUES('{eMail}',@Prod,1)", con);
            da.InsertCommand.Parameters.Add("@Prod", SqlDbType.Int, 10, "ProdID");
            da.UpdateCommand = new SqlCommand($"UPDATE Cart SET Count = @count WHERE id = @id AND eMail = '{eMail}'", con);
            da.UpdateCommand.Parameters.Add("@count", SqlDbType.Int, 10, "Count");
            da.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 10, "id");
            da.DeleteCommand = new SqlCommand($"DELETE FROM Cart WHERE id = @id AND eMail = '{eMail}'" , con);
            da.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 10, "id");
            dt.Clear();
            string SelectCommand = $"SELECT " +               
                $"Products.idProd as 'ProdID',"+
                $"Products.nameProd," +
                $"Products.Price," +
                $"Cart.id," +
                $"Cart.Count "+
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
                $"Products.idProd," +
                $"Products.nameProd," +
                $"Products.Price " +
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
                return;
            }
            _update?.Invoke("Данные успешно внесены");
        }
    }
}