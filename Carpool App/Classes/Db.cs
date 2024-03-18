using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Carpool_App.Classes
{
    internal class Db
    {
        private string connectionString;

        public Db()
        {
            // Connect to MySQL database
            connectionString = "server=srv943.hstgr.io;port=3306;database=u723022118_CarpoolApp;user=u723022118_dmytro;password=81346573195dD;";
        }

        public void ExecuteQuery(string query, Action<MySqlDataReader> handleData)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            handleData(reader);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        // Insert Post to database
        public void AddPost(string userType, string from, string to, string date, string time, string seats, string price)
        {
            string query = $"INSERT INTO Posts (from_location, to_location, date, time, seats, price) VALUES ('{from}', '{to}', '{date}', '{time}', '{seats}', '{price}')";
            ExecuteQuery(query, (reader) => { });
        }
    }
}
