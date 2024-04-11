using Carpool_App.Classes;
using Carpool_App.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Carpool_App.Events;
using MySqlX.XDevAPI.Common;

namespace Carpool_App.Pages
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public ObservableCollection<CarPost> CarPosts { get; set; } = new ObservableCollection<CarPost>();
        public ObservableCollection<Request> RequestsPosts { get; set; } = new ObservableCollection<Request>();
        public ObservableCollection<CarPost> SuggestedPosts { get; set; } = new ObservableCollection<CarPost>();

        public Profile()
        {
            InitializeComponent();
            SetUp();
        }

        private void SetUp()
        {
            SetDataListbox();

            UserControllList.ItemsSource = CarPosts;
            PassengerControllList.ItemsSource = RequestsPosts;
            UserEvents.UserApprove += UserUpproved;

            SetUserDataFields();
        }

        //Setting data to listboxes
        private void SetDataListbox()
        {
            // Get all posts from the database
            Db db = new Db();
            db.GetPostsByUserID(Store.Store.UserData.userId, (posts) =>
            {
                foreach (var post in posts)
                {
                    CarPosts.Add(post);
                }
            });

            // Get suggested trips from the database
            db.GetSuggestedTrips(Store.Store.UserData.userId, (posts) =>
            {
                foreach (var post in posts)
                {
                    SuggestedPosts.Add(post);
                }

                SuggestedlList.ItemsSource = SuggestedPosts;
            });
        }

        //Setting user data fields
        private void SetUserDataFields()
        {
            Username.Text = Store.Store.UserData.userName;
            Email.Text = Store.Store.UserData.userEmail;
            Password.Text = Store.Store.UserData.password;
        }

        //Go back button logic
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void UserUpproved(object sender, UserEventArgs e)
        {
            RequestsPosts.Clear();
            UserControllList.SelectedIndex = -1;
        }

        private void UserControllList_OnSelected(object sender, RoutedEventArgs e)
        {
            if (UserControllList.SelectedIndex == -1) return;
            var post = (CarPost)UserControllList.SelectedItem;
            RequestsPosts.Clear();
            Db db = new Db();
            db.GetPassangersByPostID(post.Id, (results) =>
            {
                foreach (var result in results)
                {
                    RequestsPosts.Add(result);
                }
            });
        }

        private void PassengerControllList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PassengerControllList.SelectedItem == null) return;

            Request request = (Request)PassengerControllList.SelectedItem;
            if (request == null) return;

            UserApprove userApprove = new UserApprove(request);
            userApprove.Show();
        }

        //updating user data
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserAuth userAuth = new UserAuth();
            userAuth.UpdateUserData(Store.Store.UserData.userId, Username.Text, Email.Text, Password.Text);
            MessageBox.Show("Data updated successfully");
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            UserEvents.RaiseUserLogOut(sender, new UserEventArgs());
            NavigationService.Navigate(new MainPage());
        }
    }
}
