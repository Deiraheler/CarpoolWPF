using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool_App.Interfaces
{
    public interface User
    {
        int userId { get; set; }
        string userName { get; set; }
        string userEmail { get; set; }
        string password { get; set; }
    }
}
