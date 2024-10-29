using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        Color strokeColor = Colors.Black;
        Brush strokeBrush = Brushes.Black;
        Point start, dest;
        public MainWindow()
        {
            InitializeComponent();
            strokeColorPicker.SelectedColor = strokeColor;
        }

        private void myCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            myCanvas.Cursor = Cursors.Pen;
        }

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            dest = e.GetPosition(myCanvas);
            statusPoint.Content = $"({Convert.ToInt32(start.X)}, {Convert.ToInt32(start.Y)}) - ({Convert.ToInt32(dest.X)}, {Convert.ToInt32(dest.Y)})";
        }

        private void myCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var brush = new SolidColorBrush(strokeColor);
            Line line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = dest.X,
                Y2 = dest.Y,
                Stroke = brush,
                StrokeThickness = 2
            };
            myCanvas.Children.Add(line);
        }

        private void strokeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            strokeColor = strokeColorPicker.SelectedColor.Value;
        }

        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(myCanvas);
            myCanvas.Cursor = Cursors.Cross;
        }
    }
}