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
    /// Interaction logic for PostPage.xaml
    /// </summary>
    public partial class PostPage : Page
    {
        public PostPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Check if form is valid with form controll
            if (Classes.FormControll.CheckAddPostForm(FromField.Text, ToField.Text, DateField.Text, TimeField.Text, SeatsField.Text, PriceField.Text))
            {
                //Add post to database
                Classes.DatabaseControll.AddPost(FromTextBox.Text, ToTextBox.Text, DateTextBox.Text, TimeTextBox.Text, SeatsTextBox.Text, PriceTextBox.Text);
                MessageBox.Show("Post added successfully!");
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Please fill in all fields correctly!");
            }
        }
    }
}
