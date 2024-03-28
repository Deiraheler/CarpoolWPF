using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool_App.Interfaces
{
    public class CarPost
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Cost { get; set; }
        public DateTime Departure { get; set; }
        public string Time { get; set; }
        public int Seats { get; set; }
        public bool Type { get; set; }
        public int Rating { get; set; }
    }
}
