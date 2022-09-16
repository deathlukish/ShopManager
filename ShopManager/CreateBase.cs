using Microsoft.Data.SqlClient;
using Microsoft.Office.Interop.Access.Dao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopManager
{
    internal class CreateBase
    {
        private Action<string>? _action;
        public void CreateBases(Action<string> action)
        {
            _action = action;
            CreateSqlBase();
            CreateAccessBase();

        }

        /// <summary>
        /// Создать базу Access
        /// </summary>
        private void CreateAccessBase()
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
               
                _action?.Invoke(e.Message);
                return;
            }
 
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={database};Jet OLEDB:Database Password=1";
            OleDbConnection? connection = null;
            OleDbCommand? command1 = null;
            OleDbCommand? command2 = null;
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
                _action?.Invoke(e.Message);
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
                if (command1 != null)
                    command1.Dispose();
                if (command2 != null)
                    command2.Dispose();
                _action?.Invoke("База клиентов успешно создана");
            }
            
        }
        /// <summary>
        /// Создать таблицу SQL
        /// </summary>
        private void AddTableToSQL()
        {

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectProducts"].ConnectionString))
                {
                    try
                    {
                        string Command = "CREATE TABLE[dbo].[Products]" +
                            "([Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                            "[eMail] NCHAR(30) NOT NULL," +
                            "[idProd] INT NOT NULL UNIQUE," +
                            "[nameProd] NCHAR(30) NULL)";
                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand(Command);
                        cmd.Connection = sqlConnection;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        _action?.Invoke(ex.Message);
                        return;
                    }
                }
                FillBaseProd();
                
            

        }
        /// <summary>
        /// Добавить базу SQL
        /// </summary>
        /// <returns></returns>
        private void CreateSqlBase()
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();                    
                    command.CommandText = "CREATE DATABASE ProductBase";
                    command.Connection = connection;                    
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    _action?.Invoke(ex.Message);
                    return;
                }
            }
            AddTableToSQL();

        }
        /// <summary>
        /// Заполнить базу SQL
        /// </summary>
        private void FillBaseProd()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectProducts"].ConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    con.Open();
                    command.CommandText = "SET IDENTITY_INSERT [dbo].[Products] ON INSERT INTO [dbo].[Products] " +
                            "([Id], [eMail], [idProd], [nameProd]) VALUES (1, N'', 34, " +
                            "N'Комп')" +
                            "INSERT INTO [dbo].[Products] ([Id], [eMail], [idProd], [nameProd]) " +
                            "VALUES (3, N'', 25, N'Телефон')" +
                            "INSERT INTO [dbo].[Products] ([Id], [eMail], [idProd], [nameProd]) " +
                            "VALUES (6, N'', 89, N'Телек')" +
                            "SET IDENTITY_INSERT [dbo].[Products] OFF";
                    command.Connection = con;
                    command.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                _action?.Invoke(ex.Message);

            }
            _action?.Invoke("База товаров успешно создана");
        }

    }
}
