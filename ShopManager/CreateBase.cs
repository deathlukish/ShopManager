using Microsoft.Data.SqlClient;
using Microsoft.Office.Interop.Access.Dao;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManager
{
    internal class CreateBase
    {
        public void CreateAccessBase()
        {
            string database = @".\AccessBase.accdb";
            const string dbLangGeneral = ";LANGID=0x0409;CP=1252;COUNTRY=0";
            var engine = new DBEngine();
            var dbs = engine.CreateDatabase(database, dbLangGeneral);
            dbs.Close();
            
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + database + ";Persist Security Info=True"; ;
            OleDbConnection connection = null;
            OleDbCommand command1 = null;
            OleDbCommand command2 = null;
            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();
                command1 = new OleDbCommand("CREATE TABLE Clients(Id AUTOINCREMENT, FirstName CHAR(20) NOT NULL, MidleName CHAR(20) NOT NULL," +
                    "LastName CHAR(20) NOT NULL, NumPhone CHAR(20), Email CHAR(20) NOT NULL)", connection);
                command2 = new OleDbCommand("CREATE INDEX PrimaryKey ON Clients(Id) WITH PRIMARY", connection);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка " + exc.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                if (command1 != null)
                    command1.Dispose();
                if (command2 != null)
                    command2.Dispose();
            }
        }
    }
}
