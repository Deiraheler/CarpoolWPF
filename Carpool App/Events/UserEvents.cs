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

        // Methods to raise the events
        public static void RaiseUserLogIn(object sender, UserEventArgs e)
        {
            UserLogIn?.Invoke(sender, e);
        }

        public static void RaiseUserLogOut(object sender, UserEventArgs e)
        {
            UserLogOut?.Invoke(sender, e);
        }
    }

    public class UserEventArgs : EventArgs
    {
    }
}
