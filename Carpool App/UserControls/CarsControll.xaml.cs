using Carpool_App.Classes;
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


        public CarsControll()
        {
            InitializeComponent();

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

        private void UserControllList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected item
            var selectedPost = UserControllList.SelectedItem as CarPost;
            if (selectedPost != null)
            {
                UsernameOut.Text = selectedPost.Username;
                FromOut.Text = selectedPost.From;
                ToOut.Text = selectedPost.To;
                DepartureOut.Text = selectedPost.Departure.ToShortDateString();
                TimeOut.Text = selectedPost.Time;
                SeatsOut.Text = selectedPost.Seats.ToString();
                CostOut.Text = selectedPost.Cost.ToString();
                RatingStars.RatingValue = selectedPost.Rating;
            }
        }
    }
}
