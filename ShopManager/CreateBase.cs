﻿using Microsoft.Data.SqlClient;
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
                    OleDbCommand comAddProds = new OleDbCommand("CREATE TABLE Prods(Id AUTOINCREMENT, Name CHAR(20) NOT NULL, Price INT)", con);
                    comAddClients.ExecuteNonQuery();
                    comAddProds.ExecuteNonQuery();
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
                            "([Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                            "[eMail] NCHAR(30) NOT NULL," +
                            "[idProd] INT NOT NULL UNIQUE," +
                            "[nameProd] NCHAR(30) NULL)";
                    
                    con.Open();
                        SqlCommand cmd = new SqlCommand(CommandAddProds, con);
                        cmd.ExecuteNonQuery();
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
        /// Заполнить базу SQL
        /// </summary>
        private void FillBaseProd()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectProducts"].ConnectionString))
                {
                    con.Open();
                    string comText = "";
                    SqlCommand command = new SqlCommand(comText, con);                   
                    command.ExecuteNonQuery();
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
                    string comAddProd1 = "INSERT INTO Prods (Name, Price) VALUES ('Телек', 300);";
                    string comAddProd2 = "INSERT INTO Prods (Name, Price) VALUES ('Утюг', 20);";
                    string comAddProd3 = "INSERT INTO Prods (Name, Price) VALUES ('Пылесос', 80);";
                    con.Open();
                    OleDbCommand com1 = new OleDbCommand(AddClient1, con);
                    OleDbCommand com2 = new OleDbCommand(AddClient2, con);
                    OleDbCommand com3 = new OleDbCommand(AddClient3, con);
                    OleDbCommand com4 = new OleDbCommand(comAddProd1, con);
                    OleDbCommand com5 = new OleDbCommand(comAddProd2, con);
                    OleDbCommand com6 = new OleDbCommand(comAddProd3, con);
                    com1.ExecuteNonQuery();
                    com2.ExecuteNonQuery();
                    com3.ExecuteNonQuery();
                    com4.ExecuteNonQuery();
                    com5.ExecuteNonQuery();
                    com6.ExecuteNonQuery();
                    con.Close();

                }


            }
            catch(Exception ex)
            {
                _update?.Invoke(ex.Message);
                return;
            }
            _update?.Invoke("База клиентов успешно создана");
        }
    }
}
