using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManager
{
    internal class ProductBase
    {
        private readonly string database = @"(localdb)MSSQLLocalDB";
        private SqlConnectionStringBuilder connectionString;
        public ProductBase()
        {
            connectionString = new()
            {
                DataSource =  @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "ProductBase",
                IntegratedSecurity = true,
                Pooling = true,
                
               
            };


        }
        /// <summary>
        /// Добавить клиента в базу
        /// </summary>
        /// <param name="client"></param>
        public void AddProduct(Product product)
        {
            string commandAdd = $"INSERT INTO Products(Email, IdProd, NameProd) VALUES ('{product.Email}'," +
                $"'{product.IdProd}','{product.NameProd}')";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString.ConnectionString))
                {
                    con.Open();
                    SqlCommand command = new(commandAdd, con);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }

        }
        /// <summary>
        /// Получить все продукты клиента из базы
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Product> GetProducts(Client client)
        {
            var products = new ObservableCollection<Product>();
            string commandGet = $"SELECT * FROM Products WHERE Email = '{client.Email}'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString.ConnectionString))
                {
                    con.Open();
                    SqlCommand command = new(commandGet, con);
                    var a = command.ExecuteReader();
                    while (a.Read())
                    {
                        products.Add(new Product
                        {
                            IdProd = (int)a["IdProd"],
                            NameProd = a["NameProd"].ToString(),
                            
                        });

                    }

                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);

            }
            return products;
        }
        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <param name="ClienToDel"></param>
        public void DelProduct(Product ProdToDel)
        {

            string commandDel = $"DELETE FROM Products WHERE Email = '{ProdToDel.IdProd}'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString.ConnectionString))
                {
                    con.Open();
                    SqlCommand command = new(commandDel, con);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

    }
}

