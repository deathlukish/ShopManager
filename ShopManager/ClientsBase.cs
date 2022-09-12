using Microsoft.Office.Interop.Access.Dao;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShopManager
{

    internal class ClientsBase
    {
        private DataTable dt;
        private OleDbDataAdapter da;
        private DataSet ds;
        private OleDbCommandBuilder commandBuilder;
        private OleDbConnection con;
        public ClientsBase()
        {
            ds = new DataSet();
            da = new OleDbDataAdapter();
            dt = new DataTable();
            commandBuilder = new OleDbCommandBuilder(da);
            con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectClients"].ConnectionString);

        }
        public void Save()
        {

            da.Update(dt);

        }

        public async Task<DataTable> PrepeareBaseClients()
        {

            await Task.Run(GetClients);
            void GetClients()
            {
                try
                {

                    string commandGet = $"SELECT * FROM Clients";
                    da.SelectCommand = new OleDbCommand(commandGet, con);
                    da.Fill(ds);
                    dt = ds.Tables[0];
                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return dt;
        }
    }
}






