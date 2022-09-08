using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManager
{

    internal class ClientsBase
    {
        private readonly string database = @".\AccessBase.accdb";
        //private readonly string dbLangGeneral = ";LANGID=0x0409;CP=1252;COUNTRY=0";
        private OleDbConnectionStringBuilder connectionString;
        private DataTable dt;
        private OleDbDataAdapter da;
        private DataSet ds;
        private OleDbCommandBuilder commandBuilder;
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


        }
        public void Save()
        {

            da.Update(dt);

        }

        public DataTable PrepeareBaseForUi()
        {
            OleDbConnection con = new OleDbConnection(connectionString.ConnectionString);            
            string commandGet = $"SELECT * FROM Clients";
            da.SelectCommand = new OleDbCommand(commandGet, con);
            da.Fill(ds);
            dt = ds.Tables[0];
            return dt;

        }


    }




}
