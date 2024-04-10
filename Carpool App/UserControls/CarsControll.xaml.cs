using Carpool_App.Classes;
using Carpool_App.Events;
using Carpool_App.Interfaces;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Carpool_App.UserControls
{
    /// <summary>
    /// Interaction logic for CarsControll.xaml
    /// </summary>
    public partial class CarsControll : UserControl
    {
        public ObservableCollection<CarPost> CarPosts { get; set; } = new ObservableCollection<CarPost>();
        private CarPost _selectedCarPost;
        public CarPost SelectedCarPost
        {
            get => _selectedCarPost;
            set
            {
                _selectedCarPost = value;
                // Assuming MapsControlUserControl is accessible
                MapsControlUserControl.From = _selectedCarPost?.From ?? string.Empty;
                MapsControlUserControl.To = _selectedCarPost?.To ?? string.Empty;
            }
        }


        public CarsControll()
        {
            InitializeComponent();
            UserEvents.SearchEvent += UserEvents_SearchEvent;

            // Get all posts from the database
            Db db = new Db();
            db.GetAllPosts((posts) =>
            {
                foreach (var post in posts)
                {
                    CarPosts.Add(post);
                }
            });

            UserControllList.ItemsSource = CarPosts;
        }

        private void UserEvents_SearchEvent(object sender, UserEventArgs e)
        {
            // Assuming you have a method in Db class to perform search with filters
            Db db = new Db();
            db.GetFilteredPosts(e.FromCity, e.ToCity, e.Date, e.Time, (posts) =>
            {
                Dispatcher.Invoke(() =>
                {
                    CarPosts.Clear();
                    foreach (var post in posts)
                    {
                        CarPosts.Add(post);
                    }

                    UserControllList.ItemsSource = CarPosts;
                });
            });
        }

        private async void UserControllList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SelectedCarPost = UserControllList.SelectedItem as CarPost;
            // Get the selected item
            if (SelectedCarPost != null)
            {
                UsernameOut.Text = SelectedCarPost.Username;
                FromOut.Text = SelectedCarPost.From;
                ToOut.Text = SelectedCarPost.To;
                DepartureOut.Text = SelectedCarPost.Departure.ToShortDateString();
                TimeOut.Text = SelectedCarPost.Time;
                SeatsOut.Text = SelectedCarPost.Seats.ToString();
                CostOut.Text = SelectedCarPost.Cost.ToString();
                RatingStars.RatingValue = SelectedCarPost.Rating;
            }

            await MapsControlUserControl.UpdateMapsDisplay();
        }

        private void Button_LogIn(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
