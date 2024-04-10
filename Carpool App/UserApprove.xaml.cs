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
using System.Windows.Shapes;
using Carpool_App.Classes;
using Carpool_App.Events;
using Carpool_App.Interfaces;

namespace Carpool_App
{
    /// <summary>
    /// Interaction logic for UserApprove.xaml
    /// </summary>
    public partial class UserApprove : Window
    {
        Request UserRequest;

        public UserApprove(Request request)
        {
            InitializeComponent();
            UserRequest = request;
        }

        //Set data to fields
        public void SetData()
        {
            if (UserRequest == null) return;

            //Set the username
            UserNameField.Text = UserRequest.Username;

            //Set rating level
            UserAuth userAuth = new UserAuth();
            RatingControl.RatingValue = userAuth.GetUserRating(UserRequest.UserId);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Db db = new Db();
            db.ApproveUser(UserRequest.Id);
            UserEvents.RaiseUserApprove(this, new UserEventArgs());
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Db db = new Db();
            db.RejectUser(UserRequest.Id);
            UserEvents.RaiseUserApprove(this, new UserEventArgs());
            this.Close();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            SetData();
        }
    }
}
