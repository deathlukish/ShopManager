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
        private SqlCommandBuilder commandBuilder;
        private SqlConnection con;
        public ProductBase()
        {
           
            da = new SqlDataAdapter();
            dt = new DataTable();
            commandBuilder = new SqlCommandBuilder(da);
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectProducts"].ConnectionString);

        }


 

        /// <summary>
        /// Получить все продукты клиента из базы
        /// </summary>
        /// <returns></returns>
        public  DataTable GetProducts(string eMail)
        {

//            @"SELECT 
//Workers.id as 'ID',
//Workers.workerName as 'Имя сотрудника',
//Bosses.workerName  as 'Имя начальника',
//Bosses.departmentName  as 'Имя отдела',
//[Description].[value] as 'Замечание'
//FROM  Workers, Bosses, [Description]
//WHERE Workers.idBoss = Bosses.id and Workers.idDescription = [Description].id
//Order By Workers.Id";



            dt.Clear();
            string SelectCommand = $"SELECT " +
                $"Products.nameProd as 'Наименование'," +
                $"Products.Price as 'Цена'" +
                $"FROM Cart, Products WHERE Cart.eMail = '{eMail}' and Products.idProd = Cart.idProd ";
            da.SelectCommand = new SqlCommand();
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

