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
using System.IO; // For working with file paths
using System.Reflection; // For accessing assembly information

namespace Carpool_App.UserControls
{
    /// <summary>
    /// Interaction logic for MapsControl.xaml
    /// </summary>
    public partial class MapsControl : UserControl
    {
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(string), typeof(MapsControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(string), typeof(MapsControl),
                new PropertyMetadata(string.Empty));

        public string From
        {
            get { return (string)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public string To
        {
            get { return (string)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }


        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateMapsDisplay();
        }

        private void UpdateMapsDisplay()
        {
            
        }

        public MapsControl()
        {
            InitializeComponent();
            string appDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = System.IO.Path.Combine(appDir, "HTMLLayouts\\map.html"); // Assuming "map.html" is the HTML file
            webBrowser.Navigate(new Uri(filePath));
        }
    }
}
