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

        /*private static void OnFromOrToPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MapsControl;
            if (control != null)
            {
                control.UpdateMapsDisplay().ConfigureAwait(false);
            }
        }*/

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

        public async Task UpdateMapsDisplay()
        {
            if (webView.CoreWebView2 != null)
            {
                string script = $@"
            new Promise((resolve, reject) => {{
                setFromToPoints('{From}', '{To}')
                    .then(resolve)
                    .catch(reject);
            }})
        ";

                await webView.CoreWebView2.ExecuteScriptAsync(script);
            }
        }


        public MapsControl()
        {
            InitializeComponent();
            this.Loaded += MapsControl_Loaded;
        }

        private async void MapsControl_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                await webView.EnsureCoreWebView2Async(null);

                if (webView == null || webView.CoreWebView2 == null)
                    throw new InvalidOperationException("WebView2 has not been initialized.");

                // Hook into the NavigationCompleted event to know when the page is fully loaded
                webView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;

                string appDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string filePath = System.IO.Path.Combine(appDir, "HTMLLayouts\\map.html");
                webView.CoreWebView2.Navigate(new Uri(filePath).AbsoluteUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing WebView2: {ex.Message}, Inner exception: {ex.InnerException?.Message}");
            }
        }

        private async void CoreWebView2_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                // Make sure From and To are set to valid locations before updating the map.
                if (!string.IsNullOrEmpty(From) && !string.IsNullOrEmpty(To))
                {
                    await UpdateMapsDisplay();
                }
            }
            else
            {
                MessageBox.Show($"Navigation to the map page failed: {e.WebErrorStatus}");
            }
        }


    }
}
