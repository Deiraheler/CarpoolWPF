using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Carpool_App.UI
{
    internal class UI
    {
        //Method to toggle visibility of a UI element
        public static void ToggleVisibility(UIElement element)
        {
            if (element != null)
            {
                // Check the current visibility state and toggle it
                element.Visibility = element.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}
