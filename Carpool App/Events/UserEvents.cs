using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool_App.Events
{
    internal class UserEvents
    {
        // Define the delegate for the events
        public delegate void UserEventHandler(object sender, UserEventArgs e);

        // Define the events
        public static event UserEventHandler UserLogIn;
        public static event UserEventHandler UserLogOut;
        public static event UserEventHandler SearchEvent;

        // Methods to raise the events
        public static void RaiseUserLogIn(object sender, UserEventArgs e)
        {
            UserLogIn?.Invoke(sender, e);
        }

        public static void RaiseUserLogOut(object sender, UserEventArgs e)
        {
            UserLogOut?.Invoke(sender, e);
        }

        public static void RaiseSearchEvent(object sender, UserEventArgs e)
        {
            SearchEvent?.Invoke(sender, e);
        }
    }

    public class UserEventArgs : EventArgs
    {
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public UserEventArgs(string fromCity, string toCity, string date, string time)
        {
            FromCity = fromCity;
            ToCity = toCity;
            Date = date;
            Time = time;
        }

        public UserEventArgs()
        {
        }
    }

}
