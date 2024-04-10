using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool_App.Interfaces
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int PostId { get; set; }
        public bool Accepted { get; set; }
    }
}
