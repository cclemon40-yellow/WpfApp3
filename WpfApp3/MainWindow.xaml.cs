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
        // 畫筆顏色初始為黑
        Color strokeColor = Colors.Black;
        Color fillColor = Colors.Aqua;
        // 筆刷顏色初始為黑
        Brush strokeBrush = Brushes.Black;
        Brush fillBrush = Brushes.Aqua;
        int strokeThickness = 1;
        // 起點和終點
        Point start, dest;
        string shape;

        public MainWindow()
        {
            InitializeComponent();
            // 初始化顏色選擇器的顏色
            strokeColorPicker.SelectedColor = strokeColor;
            fillColorPicker.SelectedColor = fillColor;
        }

        // 當滑鼠進入畫布時改變游標
        private void myCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            myCanvas.Cursor = Cursors.Pen; //Cursor 屬性用來指定當滑鼠指標位於 Canvas 上方時顯示的游標樣式。
        }

        // 當滑鼠在畫布上移動時更新終點座標並顯示在狀態欄
        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            dest = e.GetPosition(myCanvas);
            statusPoint.Content = $"({Convert.ToInt32(start.X)}, {Convert.ToInt32(start.Y)}) - ({Convert.ToInt32(dest.X)}, {Convert.ToInt32(dest.Y)})";
        }

        // 當滑鼠在畫布上釋放按鍵時繪製一條線
        private void myCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var brush = new SolidColorBrush(strokeColor); // 改變筆刷顏色
            Line line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = dest.X,
                Y2 = dest.Y,
                Stroke = brush,
                StrokeThickness = 2 // 線條寬度
            };
            myCanvas.Children.Add(line);
        }

        // 當顏色選擇器的顏色改變時更新畫筆顏色
        private void strokeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            strokeColor = strokeColorPicker.SelectedColor.Value;
        }

        private void fillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fillColor = fillColorPicker.SelectedColor.Value;
        }

        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            shape = button.Tag.ToString();
            MessageBox.Show(shape);
        }

        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void strokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            strokeThickness = (int) strokeThicknessSlider.Value;
        }

        // 當滑鼠在畫布上按下左鍵時記錄起點座標並改變游標
        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(myCanvas);
            myCanvas.Cursor = Cursors.Cross;
        }
    }
}