using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Carpool_App.UserControls
{
    public partial class StarRatingControl : UserControl
    {
        public static readonly DependencyProperty RatingValueProperty =
            DependencyProperty.Register("RatingValue", typeof(int), typeof(StarRatingControl),
                new PropertyMetadata(0, OnRatingValueChanged));

        public StarRatingControl()
        {
            InitializeComponent();
        }

        public int RatingValue
        {
            get { return (int)GetValue(RatingValueProperty); }
            set { SetValue(RatingValueProperty, value); }
        }

        private static void OnRatingValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as StarRatingControl)?.UpdateRatingDisplay();
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateRatingDisplay();
        }

        private void UpdateRatingDisplay()
        {
            starsPanel.Children.Clear(); // Use the directly named StackPanel

            for (int i = 0; i < 5; i++)
            {
                starsPanel.Children.Add(CreateStar(i < RatingValue));
            }
        }


        private Path CreateStar(bool isFilled)
        {
            Path star = new Path
            {
                Data = Geometry.Parse("M12,17.27L18.18,21l-1.64-7.03L22,9.24l-7.19-.61L12,2l-2.81,6.63L2,9.24l5.46,4.73L5.82,21z"),
                Fill = isFilled ? Brushes.Yellow : Brushes.Gray,
                Width = 20,
                Height = 20,
                Stretch = Stretch.Fill
            };
            return star;
        }
    }
}