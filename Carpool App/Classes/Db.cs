using Carpool_App.Interfaces;
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
            string query = $"INSERT INTO Posts (`UserID`, `From`, `To`, `Departure`, `Time`, `Seats`, `Cost`, `Type`) VALUES ('{Store.Store.UserData.userId}', '{from}', '{to}', '{date}', '{time}', '{seats}', '{price}', '{userType}')";
            ExecuteQuery(query, (reader) => { });
        }

        public void GetAllPosts(Action<List<CarPost>> handleData)
        {
            string query = "SELECT U.name, P.id, P.UserID, P.From, P.To, P.Cost, P.Departure, P.Time, P.Seats, P.Type, AVG(R.RatingNum) AS AverageRating FROM Posts AS P JOIN Users AS U ON P.UserID = U.id LEFT JOIN Raiting AS R ON R.EndUserID = U.id GROUP BY U.id, P.id;";
            List<CarPost> posts = new List<CarPost>();

            ExecuteQuery(query, (reader) =>
            {
                while (reader.Read())
                {
                    var post = new CarPost
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        UserId = reader.GetInt32(reader.GetOrdinal("UserID")),
                        Username = reader.GetString(reader.GetOrdinal("name")),
                        From = reader.GetString(reader.GetOrdinal("From")),
                        To = reader.GetString(reader.GetOrdinal("To")),
                        Cost = reader.GetDecimal(reader.GetOrdinal("Cost")),
                        Departure = DateTime.Parse(reader.GetString(reader.GetOrdinal("Departure"))),
                        Time = reader.GetString(reader.GetOrdinal("Time")),
                        Seats = reader.GetInt32(reader.GetOrdinal("Seats")),
                        Type = reader.GetBoolean(reader.GetOrdinal("Type")),
                        Rating = reader.GetInt32(reader.GetOrdinal("AverageRating")),
                    };
                    posts.Add(post);
                }
                handleData(posts);
            });
        }

    }
}
