using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;

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
                PersistSecurityInfo = true
            };
            con = new OleDbConnection(connectionString.ConnectionString);
            con.StateChange += Con_StateChange;
            con.Open();
            
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
                //con.Close();
                return dt;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new DataTable();
            
        }

        private void Con_StateChange(object sender, StateChangeEventArgs e)
        {
            MessageBox.Show(e.CurrentState.ToString());
        }

        private void Con_InfoMessage(object sender, OleDbInfoMessageEventArgs e)
        {
            
        }
    }




}
