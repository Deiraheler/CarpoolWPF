using Carpool_App.Classes;
using Carpool_App.Events;
using Carpool_App.UserControls;
using Carpool_App.UI;
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

namespace Carpool_App.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            //SetUp event handlers
            SubscribeToUserEvents();
            UI.UI.ToggleVisibility(AddButton);
            // Set the default tab to be the "Cars" tab
            Button_Cars_Click(null, null);
        }

        private async Task InitializeAsync()
        {
            GtfsAPI gtfsAPI = new GtfsAPI();
            string updates = await gtfsAPI.GetRealTimeUpdates();
            MessageBox.Show(updates);
        }

        private void Button_LogIn(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void Button_Cars_Click(object sender, RoutedEventArgs e)
        {
            ReturnColorToButtons();
            // Clear existing content
            ContentBlock.Children.Clear();

            CarsTabBtn.Background = Brushes.LightGray;

            // Add the content for the "Cars" tab
            var carsContent = new CarsControll();
            ContentBlock.Children.Add(carsContent);
        }

        private void Button_Passengers_Click(object sender, RoutedEventArgs e)
        {
            ReturnColorToButtons();
            // Clear existing content
            ContentBlock.Children.Clear();

            PassangerTabBtn.Background = Brushes.LightGray;

            // Add the content for the "Passengers" tab
            var passengersContent = new PassangerControll();
            ContentBlock.Children.Add(passengersContent);
        }

        //Function that will return color to buttons 
        private void ReturnColorToButtons()
        {
            CarsTabBtn.Background = Brushes.CadetBlue;
            PassangerTabBtn.Background = Brushes.CadetBlue;
        }

        //Method to subscribe to UserEvents
        private void SubscribeToUserEvents()
        {
            UserEvents.UserLogIn += OnUserLogIn;
            UserEvents.UserLogOut += OnUserLogOut;
        }

        //Method to unsubscribe to UserEvents
        private void UnsubscribeToUserEvents()
        {
            UserEvents.UserLogIn -= OnUserLogIn;
            UserEvents.UserLogOut -= OnUserLogOut;
        }

        private void OnUserLogIn(object sender, UserEventArgs e)
        {
            ButtonGrid.Width = GridLength.Auto;
            LogInBtn.Margin = new Thickness(0, 0, 10, 0);
            LogInBtn.Content = $"Profile, {Store.Store.UserData.userName}";
            LogInBtn.Click -= Button_LogIn;
            LogInBtn.Click += OpenProfilePage;
            UI.UI.ToggleVisibility(AddButton);
        }

        private void OnUserLogOut(object sender, UserEventArgs e)
        {
            ButtonGrid.Width = new GridLength(80);
            LogInBtn.Content = "Log In";
            UI.UI.ToggleVisibility(AddButton);
            LogInBtn.Click -= OpenProfilePage;
            LogInBtn.Click += Button_LogIn;
            Store.Store.UserData = null;
        }

        private void OpenProfilePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Profile());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PostPage());
        }
    }
}
