using Microsoft.Office.Interop.Access.Dao;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShopManager
{

    internal class ClientsBase
    {
        private readonly string database = @".\AccessBase.accdb";
        private OleDbConnectionStringBuilder connectionString;
        private DataTable dt;
        private OleDbDataAdapter da;
        private DataSet ds;
        private OleDbCommandBuilder commandBuilder;
        OleDbConnection con;
        public ClientsBase()
        {
            ds = new DataSet();
            da = new OleDbDataAdapter();
            dt = new DataTable();
            commandBuilder = new OleDbCommandBuilder(da);
            connectionString = new()
            {
                DataSource = database,
                Provider = "Microsoft.ACE.OLEDB.12.0",
                PersistSecurityInfo = true,
                ["Jet OLEDB:Database Password"] = "1"

        };
            con = new OleDbConnection(connectionString.ConnectionString);
            
        }
        public void Save()
        {

            da.Update(dt);

        }

        public async Task<DataTable> PrepeareBaseClients()
        {
            try
            {
                
                
                string commandGet = $"SELECT * FROM Clients";
                da.SelectCommand = new OleDbCommand(commandGet, con);
                da.Fill(ds);
                dt = ds.Tables[0];
                return dt;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new DataTable();
            
        }


    }




}
