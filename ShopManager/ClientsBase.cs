using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace ShopManager
{
    internal class ClientsBase
    {
        public event Action<string>? _update;
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
        public DataTable PrepeareBaseClients()
        {
            dt.Clear();
            try
            {
                string commandGet = $"SELECT * FROM Clients";
                da.SelectCommand = new OleDbCommand(commandGet, con);
                da.Fill(dt);
            }
            catch (Exception e)
            {
                _update?.Invoke(e.Message);
            }
            return dt;
        }
    }
}