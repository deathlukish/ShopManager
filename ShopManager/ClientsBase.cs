using System;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManager
{

    internal class ClientsBase
    {
        private readonly string database = @".\AccessBase.accdb";
        private readonly string dbLangGeneral = ";LANGID=0x0409;CP=1252;COUNTRY=0";
        private OleDbConnectionStringBuilder connectionString;
        public ClientsBase()
        {
            connectionString = new()
            {
                DataSource = database,
                Provider = "Microsoft.ACE.OLEDB.12.0",
                PersistSecurityInfo = true

            };


        }
        /// <summary>
        /// Добавить клиента в базу
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            string commandAdd = $"INSERT INTO Clients(FirstName, MidleName, LastName, NumPhone, Email) VALUES ('{client.FirstName}'," +
                $"'{client.MiddleName}','{client.LastName}','{client.NumPhone}','{client.Email}')";
            try
            {
                using (OleDbConnection con = new OleDbConnection(connectionString.ConnectionString))
                {
                    con.Open();
                    OleDbCommand command = new(commandAdd, con);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }

        }
        /// <summary>
        /// Получить всех клиентов из базы
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<Client>> GetClients()
        {
            var clients = new ObservableCollection<Client>();
            string commandGet = $"SELECT * FROM Clients";
            try
            {
                using (OleDbConnection con = new OleDbConnection(connectionString.ConnectionString))
                {
                    con.Open();
                    OleDbCommand command = new(commandGet, con);
                    var a = command.ExecuteReader();
                    while (a.Read())
                    {
                        clients.Add(new Client

                        {
                            FirstName = a["Firstname"].ToString(),
                            MiddleName = a["MidleName"].ToString(),
                            LastName = a["LastName"].ToString(),
                            NumPhone = a["NumPhone"].ToString(),
                            Email = a["Email"].ToString(),
                        });

                    }

                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);

            }
            return clients;
        }
        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="ClienToDel"></param>
        public void DelClient(Client ClienToDel)
        {

            string commandDel = $"DELETE FROM Clients WHERE Email = '{ClienToDel.Email}'";
            try
            {
                using (OleDbConnection con = new OleDbConnection(connectionString.ConnectionString))
                {
                    con.Open();
                    OleDbCommand command = new(commandDel, con);
                    command.ExecuteNonQuery();
                    
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

    }




}
