using Azure.Messaging;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManager
{
    
    internal class ClientsBase
    {
        private readonly string database = @".\AccessBase.accdb";
        private readonly string dbLangGeneral = ";LANGID=0x0409;CP=1252;COUNTRY=0";
        private string connectionString;
        private OleDbConnection connection;
        public ClientsBase()
        {
            connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={database};Persist Security Info=True";
            connection = new OleDbConnection(connectionString);
            connection.Open();
        }
        public void AddClient(Client client)
        {

            string commandAdd = $"INSERT INTO Clients(FirstName, MidleName, LastName, NumPhone, Email) VALUES ('{client.FirstName}'," +
                $"'{client.MiddleName}','{client.LastName}','{client.NumPhone}','{client.Email}')";
            OleDbCommand command1 = null;
            try
            {

                command1 = new OleDbCommand(commandAdd, connection);
                command1.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                if (command1 != null)
                    command1.Dispose();

            }

        }

    }

    
    
    
}
