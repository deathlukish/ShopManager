using Microsoft.Data.SqlClient;
using Microsoft.Office.Interop.Access.Dao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManager
{
    internal class CreateBase
    {
        
        public void CreateAccessBase(Action<string> action)
        {
                        
            string database = @".\AccessBase.accdb";
            const string dbLangGeneral = ";LANGID=0x0409;CP=1252;COUNTRY=0;PWD=1";
            var engine = new DBEngine();
            try
            {
                var dbs = engine.CreateDatabase(database, dbLangGeneral);
                dbs.Close();
                
            }
            catch (Exception e)
            {
               
                action.Invoke(e.Message);
                return;
            }
 
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={database};Jet OLEDB:Database Password=1";
            OleDbConnection connection = null;
            OleDbCommand command1 = null;
            OleDbCommand command2 = null;
            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();
                command1 = new OleDbCommand("CREATE TABLE Clients(Id AUTOINCREMENT, FirstName CHAR(20) NOT NULL, MidleName CHAR(20) NOT NULL," +
                    "LastName CHAR(20) NOT NULL, NumPhone CHAR(20), Email CHAR(20) NOT NULL UNIQUE)", connection);
                command2 = new OleDbCommand("CREATE INDEX PrimaryKey ON Clients(Id) WITH PRIMARY", connection);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                action.Invoke(e.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                if (command1 != null)
                    command1.Dispose();
                if (command2 != null)
                    command2.Dispose();
                action.Invoke("База успешно создана");
            }
            
        }
        public void CreateSqlBase()
        {
            Creaate();
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectProducts"].ConnectionString))
            {
                string Command = "CREATE TABLE[dbo].[Products]" +
                    "([Id] INT NOT NULL PRIMARY KEY, " +
                    "[email] NCHAR(30) NOT NULL," +
                    "[IdProd] INT NOT NULL UNIQUE," +
                    "[nameProd] NCHAR(30) NULL)";
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(Command);
                cmd.Connection = sqlConnection;
                cmd.ExecuteNonQuery();
               
            }
        }
        public void Creaate()
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                 connection.Open();   // открываем подключение

                SqlCommand command = new SqlCommand();
                // определяем выполняемую команду
                command.CommandText = "CREATE DATABASE Products";
                // определяем используемое подключение
                command.Connection = connection;
                // выполняем команду
                command.ExecuteNonQuery();
                
            }


        }


    }
}
