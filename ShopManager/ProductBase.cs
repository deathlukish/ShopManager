using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManager
{
    internal class ProductBase
    {
        private DataTable dt;
        private SqlDataAdapter da;
        private DataSet ds;
        private SqlCommandBuilder commandBuilder;
        private SqlConnection con;
        private readonly string database = @"(localdb)MSSQLLocalDB";
        private SqlConnectionStringBuilder connectionString;
        public ProductBase()
        {
            ds = new DataSet();
            da = new SqlDataAdapter();
            dt = new DataTable();
            commandBuilder = new SqlCommandBuilder(da);
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectProducts"].ConnectionString);
            

        }
        /// <summary>
        /// Получить все продукты клиента из базы
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> GetProducts()
        {
            string SelectCommand = $"SELECT * FROM Products";
            da.SelectCommand = new SqlCommand();
            await Task.Run(GetProd);
            void GetProd()
            {

                
                try
                {
                    
                    da.SelectCommand = new SqlCommand(SelectCommand, con);
                    da.Fill(ds);
                    dt = ds.Tables[0];

                }

                catch (Exception e)
                {

                    MessageBox.Show(e.Message);

                }
            }
            return dt;
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

