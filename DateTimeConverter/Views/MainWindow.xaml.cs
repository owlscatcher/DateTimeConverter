using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;


namespace DateTimeConverter.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            windowTitle.PreviewMouseLeftButtonDown += _moveBorder_PreviewMouseLeftButtonDown;
        }

        Point _startPoint;
        bool _isDragging = false;

        private void _moveBorder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(this))
            {
                _isDragging = true;
                _startPoint = PointToScreen(Mouse.GetPosition(this));
            }
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point newPoint = PointToScreen(Mouse.GetPosition(this));
                int diffX = (int)(newPoint.X - _startPoint.X);
                int diffY = (int)(newPoint.Y - _startPoint.Y);
                if (Math.Abs(diffX) > 1 || Math.Abs(diffY) > 1)
                {
                    Left += diffX;
                    Top += diffY;
                    InvalidateVisual();
                    _startPoint = newPoint;
                }
            }
        }
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
                Mouse.Capture(null);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void HexPackageValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
