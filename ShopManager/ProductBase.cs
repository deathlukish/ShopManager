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
        private DataTable dt;
        private SqlDataAdapter da;
        private DataSet ds;
        private SqlCommandBuilder commandBuilder;
        private SqlConnection con;
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
        public async Task<DataTable> GetProducts(string eMail)
        {
            string SelectCommand = $"SELECT * FROM Products WHERE eMail = '{eMail}'";
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
      

        

    }
}

