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
            AddTableToAccess();
            
        }
        /// <summary>
        /// Добавить таблицу в Access
        /// </summary>
        private void AddTableToAccess()
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectClients"].ConnectionString))
                {
                    con.Open();
                    OleDbCommand com = new OleDbCommand("CREATE TABLE Clients(Id AUTOINCREMENT, FirstName CHAR(20) NOT NULL, MidleName CHAR(20) NOT NULL," +
                        "LastName CHAR(20) NOT NULL, NumPhone CHAR(20), Email CHAR(20) NOT NULL UNIQUE)", con);
                    com.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                _action?.Invoke(ex.Message);
                return;
            }
            FillBaseClient();
        
        }


        /// <summary>
        /// Создать таблицу SQL
        /// </summary>
        private void AddTableToSQL()
        {

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectProducts"].ConnectionString))
                {
                    try
                    {
                        string Command = "CREATE TABLE[dbo].[Products]" +
                            "([Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                            "[eMail] NCHAR(30) NOT NULL," +
                            "[idProd] INT NOT NULL UNIQUE," +
                            "[nameProd] NCHAR(30) NULL)";
                        con.Open();
                        SqlCommand cmd = new SqlCommand(Command, con);
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
                    SqlCommand command = new SqlCommand("CREATE DATABASE ProductBase", connection);                                       
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
                    con.Open();
                    string comText = "SET IDENTITY_INSERT [dbo].[Products] ON INSERT INTO [dbo].[Products] " +
                            "([Id], [eMail], [idProd], [nameProd]) VALUES (1, N'', 34, " +
                            "N'Комп')" +
                            "INSERT INTO [dbo].[Products] ([Id], [eMail], [idProd], [nameProd]) " +
                            "VALUES (3, N'', 25, N'Телефон')" +
                            "INSERT INTO [dbo].[Products] ([Id], [eMail], [idProd], [nameProd]) " +
                            "VALUES (6, N'', 89, N'Телек')" +
                            "SET IDENTITY_INSERT [dbo].[Products] OFF";
                    SqlCommand command = new SqlCommand(comText, con);                   
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
        /// <summary>
        /// Заполнить базу Access
        /// </summary>
        private void FillBaseClient()
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectClients"].ConnectionString))
                {
                    string AddClient1 = "INSERT INTO Clients(FirstName,MidleName,LastName,NumPhone,Email) VALUES ('Иван','Иванович','Иванов','+79266666666','test1@ya.ru')";
                    string AddClient2 = "INSERT INTO Clients(FirstName,MidleName,LastName,NumPhone,Email) VALUES ('Петр','Петрович','Петров','+79277777777','test2@ya.ru')";
                    string AddClient3 = "INSERT INTO Clients(FirstName,MidleName,LastName,NumPhone,Email) VALUES ('Сидр','Сидорович','Сидоров','+7928888888','test3@ya.ru')";
                    con.Open();
                    OleDbCommand com1 = new OleDbCommand(AddClient1, con);
                    OleDbCommand com2 = new OleDbCommand(AddClient2, con);
                    OleDbCommand com3 = new OleDbCommand(AddClient3, con);
                    com1.ExecuteNonQuery();
                    com2.ExecuteNonQuery();
                    com3.ExecuteNonQuery();
                    con.Close();

                }


            }
            catch(Exception ex)
            {
                _action?.Invoke(ex.Message);
                return;
            }
            _action?.Invoke("База клиентов успешно создана");
        }
    }
}
