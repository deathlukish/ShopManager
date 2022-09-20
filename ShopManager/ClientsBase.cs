using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace ShopManager
{

    internal class ClientsBase
    {


        private DataTable dt;
        private OleDbDataAdapter da;
        private OleDbCommandBuilder commandBuilder;
        private OleDbConnection con;
        public ClientsBase()
        {

            da = new OleDbDataAdapter();
            dt = new DataTable();
            commandBuilder = new OleDbCommandBuilder(da);
            con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectClients"].ConnectionString);

        }
        public void Save()
        {

            da.Update(dt);

        }

        public DataTable PrepeareBaseClients()
        {
            dt.Clear();
            try
            {

                string commandGet = $"SELECT * FROM Clients";
                da.SelectCommand = new OleDbCommand(commandGet, con);
                da.Fill(dt);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dt;
        }
    }
}






