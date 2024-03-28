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

namespace Carpool_App
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            Username.Text = "Username";
            Username.Foreground = Brushes.Gray;
            Email.Text = "Email";
            Email.Foreground = Brushes.Gray;
            Password.Text = "Password";
            Password.Foreground = Brushes.Gray;

            EmailReg.Text = "Email";
            EmailReg.Foreground = Brushes.Gray;
            PasswordReg.Text = "Password";
            PasswordReg.Foreground = Brushes.Gray;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Username" || textBox.Text == "Email" || textBox.Text == "Password")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }else if(textBox != null && textBox.Text == "EmailReg" || textBox.Text == "PasswordReg")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.Gray;
                if (textBox.Name == "Username")
                {
                    textBox.Text = "Username";
                }
                else if (textBox.Name == "Email")
                {
                    textBox.Text = "Email";
                }
                else if (textBox.Name == "Password")
                {
                    textBox.Text = "Password";
                }
                else if (textBox.Name == "EmailReg")
                {
                    textBox.Text = "Email";
                }
                else if (textBox.Name == "PasswordReg")
                {
                    textBox.Text = "Password";
                }
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = Email.Text;
            string password = Password.Text;

            if (Classes.FormControll.CheckLoginForm(email, password))
            {
                Classes.UserAuth userAuth = new Classes.UserAuth();
                if (userAuth.UserExists(email, password))
                {
                    userAuth.GetUserData(email, null, (user) =>
                    {
                        Store.Store.UserData = user;
                    });
                    var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    if (mainWindow != null)
                    {
                        UserEvents.RaiseUserLogIn(this, new UserEventArgs());

                        // Close the LoginWindow
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("User does not exist");
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string email = EmailReg.Text;
            string password = PasswordReg.Text;

            if (Classes.FormControll.CheckRegisterForm(username, email, password))
            {
                Classes.UserAuth userAuth = new Classes.UserAuth();
                userAuth.RegisterUser(username, email, password);
                MessageBox.Show("User registered successfully");
            }
            else
            {
                MessageBox.Show("Please fill in all fields");
            }
        }
    }
}
