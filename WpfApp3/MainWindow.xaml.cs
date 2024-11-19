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
        string shapeType = "line";

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
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point origin;
                origin.X = Math.Min(start.X, dest.X);
                origin.Y = Math.Min(start.Y, dest.Y);
                double width = Math.Abs(start.X - dest.X);
                double height = Math.Abs(start.Y - dest.Y);

                switch (shapeType)
                {
                    case "line":                     /*OfType->在myCanvas的子元素中找出類型是Line的值*/
                        var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                        line.X2 = dest.X;
                        line.Y2 = dest.Y;
                        break;
                    case "rectangle":
                        var rectangle = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                        rectangle.Width = width;
                        rectangle.Height = height;
                        rectangle.SetValue(Canvas.LeftProperty, origin.X);
                        rectangle.SetValue(Canvas.TopProperty, origin.Y);
                        break;
                    case "ellipse":
                        var ellipse = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                        ellipse.Width = width;
                        ellipse.Height = height;
                        ellipse.SetValue(Canvas.LeftProperty, origin.X);
                        ellipse.SetValue(Canvas.TopProperty, origin.Y);
                        break;
                    case "polyline":
                        break;
                }
            }
            statusPoint.Content = $"({Convert.ToInt32(start.X)}, {Convert.ToInt32(start.Y)}) - ({Convert.ToInt32(dest.X)}, {Convert.ToInt32(dest.Y)})";
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
            var targetRadioButton = sender as RadioButton;
            shapeType = targetRadioButton.Tag.ToString();
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

        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Brush strokeBrush = new SolidColorBrush(strokeColor);
            Brush fillBrush = new SolidColorBrush(fillColor);

            switch (shapeType)
            {
                case "line":
                    var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                    line.Stroke = strokeBrush;
                    line.StrokeThickness = strokeThickness;
                    break;
                case "rectangle":
                    var rectangle = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                    rectangle.Stroke = strokeBrush;
                    rectangle.Fill = fillBrush;
                    rectangle.StrokeThickness = strokeThickness;
                    break;
                case "ellipse":
                    var ellipse = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                    ellipse.Stroke = strokeBrush;
                    ellipse.Fill = fillBrush;
                    ellipse.StrokeThickness = strokeThickness;
                    break;
                case "polyline":
                    break;
            }
        }

        // 當滑鼠在畫布上按下左鍵時記錄起點座標並改變游標
        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(myCanvas);
            myCanvas.Cursor = Cursors.Cross;

            switch (shapeType)
            {
                case "line":
                    Line line = new Line
                    {
                        X1 = start.X,
                        Y1 = start.Y,
                        X2 = dest.X,
                        Y2 = dest.Y,
                        Stroke = Brushes.Gray,
                        StrokeThickness = 1
                    };
                    myCanvas.Children.Add(line);
                    break;
                case "rectangle":
                    Rectangle rectangle = new Rectangle
                    {
                        Stroke = Brushes.Gray,
                        Fill = Brushes.LightGray
                    };
                    myCanvas.Children.Add(rectangle);
                    rectangle.SetValue(Canvas.LeftProperty, start.X);
                    rectangle.SetValue(Canvas.TopProperty, start.Y);
                    break;
                case "ellipse":
                    Ellipse ellipse = new Ellipse
                    {
                        Stroke = Brushes.Gray,
                        Fill = Brushes.LightGray
                    };
                    myCanvas.Children.Add(ellipse);
                    ellipse.SetValue(Canvas.LeftProperty, start.X);
                    ellipse.SetValue(Canvas.TopProperty, start.Y);
                    break;
                case "polyline":
                    break;
            }
        }
    }
}