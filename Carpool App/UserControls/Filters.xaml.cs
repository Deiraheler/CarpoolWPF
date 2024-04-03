using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Carpool_App.Classes;
using Carpool_App.Events;
using Syncfusion.Windows.Controls.Input;

namespace Carpool_App.UserControls
{
    /// <summary>
    /// Interaction logic for Filters.xaml
    /// </summary>
    public partial class Filters : UserControl
    {

        private Dictionary<string, List<string>> datesWithTimes;
        public Filters()
        {
            InitializeComponent();
            this.Loaded += UserControl_Loaded;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleFilterBlock.Visibility == Visibility.Visible)
            {
                ToggleFilterBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                ToggleFilterBlock.Visibility = Visibility.Visible;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Get all cities from database
            Classes.Db db = new Classes.Db();
            db.GetRoutes(routes =>
            {
                // Populate ComboBox cmbFrom
                Dispatcher.Invoke(() =>
                {
                    cmbFrom.ItemsSource = routes[0];
                });

                // Populate ComboBox cmbTo
                Dispatcher.Invoke(() =>
                {
                    cmbTo.ItemsSource = routes[1];
                });
            });
        }

        private void cmbFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox fromBox = sender as ComboBox;
            if (fromBox.SelectedIndex != -1)
            {
                string from = fromBox.SelectedItem.ToString();
                Db db = new Db();
                db.GetToCities(from, toCities =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        cmbTo.ItemsSource = toCities;
                    });
                });
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cmbFrom.SelectedIndex = -1;
            cmbTo.SelectedIndex = -1;
            cmbDate.SelectedIndex = -1;
            cmbTime.SelectedIndex = -1;
        }

        private void cmbTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox toBox = sender as ComboBox;
            if (toBox.SelectedIndex != -1 && cmbFrom.SelectedIndex != -1)
            {
                string from = cmbFrom.SelectedItem.ToString();
                string to = toBox.SelectedItem.ToString();
                Db db = new Db();
                db.GetFutureDatesWithTimes(from, to, datesWithTimes =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        this.datesWithTimes = datesWithTimes;
                        cmbDate.ItemsSource = datesWithTimes.Keys;
                    });
                });
            }
        }

        private void cmbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDate.SelectedIndex != -1)
            {
                Dispatcher.Invoke(() =>
                {
                    cmbTime.ItemsSource = datesWithTimes[cmbDate.SelectedItem.ToString()];
                });
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string fromCity = cmbFrom.SelectedValue?.ToString() ?? "";
            string toCity = cmbTo.SelectedValue?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(fromCity) || string.IsNullOrWhiteSpace(toCity))
            {
                MessageBox.Show("Important fields 'From' and 'To' are required.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string date = cmbDate.SelectedValue?.ToString() ?? "";
            string time = cmbTime.SelectedValue?.ToString() ?? "";

            // Raise the SearchEvent
            UserEvents.RaiseSearchEvent(this, new UserEventArgs(fromCity, toCity, date, time));
        }
    }
}
