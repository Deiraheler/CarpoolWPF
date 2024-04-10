using Carpool_App.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            connectionString =
                "server=srv943.hstgr.io;port=3306;database=u723022118_CarpoolApp;user=u723022118_dmytro;password=81346573195dD;";
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
        public void AddPost(string userType, string from, string to, string date, string time, string seats,
            string price)
        {
            string query =
                $"INSERT INTO Posts (`UserID`, `From`, `To`, `Departure`, `Time`, `Seats`, `Cost`, `Type`) VALUES ('{Store.Store.UserData.userId}', '{from}', '{to}', '{date}', '{time}', '{seats}', '{price}', '{userType}')";
            ExecuteQuery(query, (reader) => { });
        }

        public void GetAllPosts(Action<List<CarPost>> handleData)
        {
            string query =
                "SELECT U.name, P.id, P.UserID, P.`From`, P.`To`, P.Cost, P.Departure, P.Time, P.Seats, P.Type, AVG(R.RatingNum) AS AverageRating FROM Posts AS P JOIN Users AS U ON P.UserID = U.id LEFT JOIN Rating AS R ON R.EndUserID = U.id WHERE STR_TO_DATE(P.Departure, '%d/%m/%Y %H:%i:%s') >= CURDATE() GROUP BY U.id, P.id;";
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

        public void GetPostsByUserID(int id, Action<List<CarPost>> handleData)
        {
            string query =
                $"SELECT U.name, P.id, P.UserID, P.`From`, P.`To`, P.Cost, P.Departure, P.Time, P.Seats, P.Type FROM Posts AS P JOIN Users AS U ON P.UserID = U.id WHERE STR_TO_DATE(P.Departure, '%d/%m/%Y %H:%i:%s') >= CURDATE() AND P.UserID = {id} GROUP BY U.id, P.id;";
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
                    };
                    posts.Add(post);
                }

                handleData(posts);
            });
        }

        //Get passangers from table Requests by PostID
        public void GetPassangersByPostID(int id, Action<List<Request>> handleData)
        {
            string query = $"SELECT U.name, R.id, R.UserID, R.PostID, R.Accepted FROM Requests AS R JOIN Users AS U ON R.UserID = U.id WHERE R.PostID = {id};";
            List<Request> requests = new List<Request>();

            ExecuteQuery(query, (reader) =>
            {
                while (reader.Read())
                {
                    var request = new Request
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        UserId = reader.GetInt32(reader.GetOrdinal("UserID")),
                        Username = reader.GetString(reader.GetOrdinal("name")),
                        PostId = reader.GetInt32(reader.GetOrdinal("PostID")),
                        Accepted = reader.GetBoolean(reader.GetOrdinal("Accepted")),
                    };
                    requests.Add(request);
                }

                handleData(requests);
            });
        }

        //Get all routes from and to
        public void GetRoutes(Action<List<string>[]> handleData)
        {
            string query = "SELECT DISTINCT `From`, `To` FROM Posts;";
            List<string>[] routes = { new List<string>(), new List<string>() };

            ExecuteQuery(query, (reader) =>
            {
                while (reader.Read())
                {
                    // Add 'From' city to index 0 and 'To' city to index 1
                    routes[0].Add(reader.GetString(reader.GetOrdinal("From")));
                    routes[1].Add(reader.GetString(reader.GetOrdinal("To")));
                }

                // Remove duplicates for both 'From' and 'To' cities
                routes[0] = routes[0].Distinct().ToList();
                routes[1] = routes[1].Distinct().ToList();

                // Pass the array of lists to the callback function
                handleData(routes);
            });
        }

        //Get all To citys by From city
        public void GetToCities(string from, Action<List<string>> handleData)
        {
            string query = $"SELECT DISTINCT `To` FROM Posts WHERE `From` = '{from}';";
            List<string> toCitys = new List<string>();

            ExecuteQuery(query, (reader) =>
            {
                while (reader.Read())
                {
                    toCitys.Add(reader.GetString(reader.GetOrdinal("To")));
                }

                handleData(toCitys);
            });
        }

        // Method to get future dates with times
        public void GetFutureDatesWithTimes(string fromCity, string toCity,
            Action<Dictionary<string, List<string>>> handleData)
        {
            // Adjust the query to include the Time column
            string query =
                $"SELECT `Departure`, `Time` FROM Posts WHERE `From` = '{fromCity}' AND `To` = '{toCity}' ORDER BY `Departure` ASC;";

            ExecuteQuery(query, (reader) =>
            {
                var datesWithTimes = new Dictionary<string, List<string>>();
                var today = DateTime.Now.Date; // Use only the date part for today's comparison

                while (reader.Read())
                {
                    var dateStr = reader.GetString(reader.GetOrdinal("Departure"));
                    var timeStr = reader.GetString(reader.GetOrdinal("Time")); // Assuming time is stored as HH:mm

                    // Attempt to parse the date part of Departure
                    if (DateTime.TryParseExact(dateStr, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out DateTime date))
                    {
                        if (date >= today) // Ensure the date is in the future
                        {
                            if (!datesWithTimes.ContainsKey(date.ToShortDateString()))
                            {
                                datesWithTimes[date.ToShortDateString()] = new List<string>();
                            }

                            // Assuming timeStr is the time in HH:mm format or similar
                            datesWithTimes[date.ToShortDateString()].Add(timeStr);
                        }
                    }
                }

                handleData(datesWithTimes);
            });
        }

        // Method to get all posts with filters
        public void GetFilteredPosts(string fromCity, string toCity, string date, string time, Action<List<CarPost>> handleData)
        {
            // Initialize WHERE conditions with mandatory filters and check for future dates
            var whereConditions = $"WHERE P.`From` = '{fromCity}' AND P.`To` = '{toCity}' AND STR_TO_DATE(P.Departure, '%d/%m/%Y') >= CURDATE()";

            // Add optional date and time filters
            if (!string.IsNullOrWhiteSpace(date))
            {
                whereConditions += $" AND P.Departure = '{date} 00:00:00'";
            }
            if (!string.IsNullOrWhiteSpace(time))
            {
                if (!string.IsNullOrWhiteSpace(time))
                {
                    whereConditions += $" AND STR_TO_DATE(P.Time, '%H:%i') >= STR_TO_DATE('{time}', '%H:%i')";
                }
            }

            string query = $"SELECT U.name, P.id, P.UserID, P.`From`, P.`To`, P.Cost, P.Departure, P.Time, P.Seats, P.Type, AVG(R.RatingNum) AS AverageRating FROM Posts AS P JOIN Users AS U ON P.UserID = U.id LEFT JOIN Rating AS R ON R.EndUserID = U.id {whereConditions} GROUP BY U.id, P.id;";

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

        // Method to approve a user
        public void ApproveUser(int requestId)
        {
            string query = $"UPDATE Requests SET Accepted = 1 WHERE id = {requestId};";
            ExecuteQuery(query, (reader) => { });
        }

        // Method to reject a user
        public void RejectUser(int requestId)
        {
            string query = $"DELETE FROM Requests WHERE id = {requestId};";
            ExecuteQuery(query, (reader) => { });
        }
    }
}
