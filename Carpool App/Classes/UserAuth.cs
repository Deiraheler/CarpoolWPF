using MySql.Data.MySqlClient;
using Carpool_App.Interfaces;
using System;
using System.Windows;

namespace Carpool_App.Classes
{
    internal class UserAuth
    {
        private string connectionString;

        public UserAuth()
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

        // Check if user exists in the database
        public bool UserExists(string email, string password)
        {
            string query = $"SELECT * FROM Users WHERE email = '{email}' AND password = '{password}'";
            bool exists = false;
            ExecuteQuery(query, (reader) =>
            {
                exists = reader.HasRows;
            });
            return exists;
        }

        // Register a new user
        public void RegisterUser(string username, string email, string password)
        {
            string query = $"INSERT INTO Users (name, email, password) VALUES ('{username}', '{email}', '{password}')";
            ExecuteQuery(query, (reader) => { });
        }

        // Get user ID by email
        public int GetUserId(string email)
        {
            string query = $"SELECT id FROM Users WHERE email = '{email}'";
            int id = -1;
            ExecuteQuery(query, (reader) =>
            {
                if (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            });
            return id;
        }

        //Get user data by email or id
        public void GetUserData(string email, int? id, Action<ConcreteUser> handleData)
        {
            string query = "";
            if (email != null)
            {
                query = $"SELECT id, name, email, password FROM Users WHERE email = '{email}'";
            }
            else if (id.HasValue)
            {
                query = $"SELECT id, name, email, password FROM Users WHERE id = {id}";
            }

            ExecuteQuery(query, (reader) =>
            {
                if (reader.Read())
                {
                    var user = new ConcreteUser
                    {
                        userId = reader.GetInt32(reader.GetOrdinal("id")),
                        userName = reader.GetString(reader.GetOrdinal("name")),
                        userEmail = reader.GetString(reader.GetOrdinal("email")),
                        password = reader.GetString(reader.GetOrdinal("password"))
                    };
                    handleData(user);
                }
            });
        }

        // Get user rating by user ID
        public int GetUserRating(int userId)
        {
            string query = $"SELECT AVG(RatingNum) as AverageRating FROM Rating WHERE EndUserID = {userId}";
            int rating = 0;
            ExecuteQuery(query, (reader) =>
            {
                if (reader.Read() && !reader.IsDBNull(reader.GetOrdinal("AverageRating")))
                {
                    double averageRating = reader.GetDouble(reader.GetOrdinal("AverageRating"));
                    rating = Convert.ToInt32(Math.Round(averageRating));
                }
            });
            return rating;
        }

        // Update user data
        public void UpdateUserData(int userId, string username, string email, string password)
        {
            string query = $"UPDATE Users SET name = '{username}', email = '{email}', password = '{password}' WHERE id = {userId}";
            ExecuteQuery(query, (reader) => { });
        }
    }
}
