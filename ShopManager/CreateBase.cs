using Microsoft.Data.SqlClient;
using Microsoft.Office.Interop.Access.Dao;
using System;
using System.Configuration;
using System.Data.OleDb;


namespace ShopManager
{
    internal class CreateBase
    {
        public event Action<string>? _update;
        public void CreateBases()
        {
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
                _update?.Invoke(e.Message);
                return;
            }
            AddTableToAccess();
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
                    _update?.Invoke(ex.Message);
                    return;
                }
            }
            AddTableToSQL();
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
                    OleDbCommand comAddClients = new OleDbCommand("CREATE TABLE Clients(Id AUTOINCREMENT, FirstName CHAR(20) NOT NULL, MidleName CHAR(20) NOT NULL," +
                        "LastName CHAR(20) NOT NULL, NumPhone CHAR(20), Email CHAR(20) NOT NULL UNIQUE)", con);
                    comAddClients.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                _update?.Invoke(ex.Message);
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
                    string CommandAddProds = "CREATE TABLE[dbo].[Products]" +
                        "([idProd] INT NOT NULL PRIMARY KEY IDENTITY(1,1)," +
                        "[nameProd] NCHAR(30) NULL,"+
                        "[Price] INT);";
                    string CommandAddCart = "CREATE TABLE[dbo].[Cart]" +
                        "([id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                        "[eMail] NCHAR(30) NOT NULL," +
                        "[idProd] INT NOT NULL," +
                        "[Count] INT NOT NULL);";
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand(CommandAddProds, con);
                    SqlCommand cmd2 = new SqlCommand(CommandAddCart, con);
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _update?.Invoke(ex.Message);
                    return;
                }
            }
            FillBaseProd();
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
                    string comText1 = "SET IDENTITY_INSERT [dbo].[Products] ON "+
                                      "INSERT INTO[dbo].[Products] ([idProd],[nameProd], [Price]) VALUES(1,N'Утюг',100)" +
                                      "INSERT INTO[dbo].[Products] ([idProd],[nameProd], [Price]) VALUES(2,N'Пылесос',200)" +
                                      "INSERT INTO[dbo].[Products] ([idProd],[nameProd], [Price]) VALUES(3,N'Телек',500)" +
                                      "SET IDENTITY_INSERT[dbo].[Products] OFF";
                    string comText2 = "SET IDENTITY_INSERT [dbo].[Cart] ON " +
                                      "INSERT INTO[dbo].[Cart] ([Id], [eMail], [idProd]) VALUES(1, N'test1@ya.ru', 1)" +
                                      "INSERT INTO[dbo].[Cart] ([Id], [eMail], [idProd]) VALUES(2, N'test1@ya.ru', 2)" +
                                      "INSERT INTO[dbo].[Cart] ([Id], [eMail], [idProd]) VALUES(3, N'test1@ya.ru', 3)" +
                                      "SET IDENTITY_INSERT[dbo].[Cart] OFF";
                    SqlCommand command = new SqlCommand(comText1, con);
                    SqlCommand command2 = new SqlCommand(comText2, con);
                    command.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                _update?.Invoke(ex.Message);
            }
            _update?.Invoke("База товаров успешно создана");
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
            catch (Exception ex)
            {
                _update?.Invoke(ex.Message);
                return;
            }
            _update?.Invoke("База клиентов успешно создана");
        }
    }
}
