using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
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

        private void HexValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9|^A-F]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        struct RegexPatterns
        {
            public static string DayValidatePattern => "^(0?[1-9]|[12][0-9]|3[01])$";
            public static string MounthValidatePattern => "^(0?[1-9]|1[0-2])$";
            public static string YearValidatePattern => @"^[1-3][0-9]{3}$";
            public static string HourValidatePattern => "^(0?\\d|1\\d|2[0-3])$";
            public static string MinuteValidatePattern => "^(0?\\d|[1-5]\\d)$";
            public static string SecondValidatePattern => "^(0?\\d|[1-5]\\d)$";
            public static string MillisecondValidatePattern => "^\\d{1,3}$";

            public static string BytePackageValidatePattern => "[^0-9|^A-F]";

        }

        struct InputsName
        {
            public const string DayInput = "dayInput";
            public const string MounthInput = "mounthInput";
            public const string YearInput = "yearInput";
            public const string HourInput = "hourInput";
            public const string MinutesInput = "minutesInput";
            public const string SecondsInput = "secondsInput";
            public const string MillisecondsInput = "millisecondsInput";

            public const string BytePackageInput = "bytePackageInput";
            public const string DataByteInput = "dataByteInput";
        }

        private void ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            var _matchPattern = string.Empty;

            switch (textBox.Name)
            {
                case InputsName.DayInput:
                    _matchPattern = RegexPatterns.DayValidatePattern;
                    break;
                case InputsName.MounthInput:
                    _matchPattern = RegexPatterns.MounthValidatePattern;
                    break;
                case InputsName.HourInput:
                    _matchPattern = RegexPatterns.HourValidatePattern;
                    break;
                case InputsName.MinutesInput:
                    _matchPattern = RegexPatterns.MinuteValidatePattern;
                    break;
                case InputsName.SecondsInput:
                        _matchPattern = RegexPatterns.SecondValidatePattern;
                    break;
                case InputsName.MillisecondsInput:
                    _matchPattern = RegexPatterns.MillisecondValidatePattern;
                    break;
                case InputsName.BytePackageInput:
                    _matchPattern = RegexPatterns.BytePackageValidatePattern;
                    break;
                case InputsName.DataByteInput:
                    _matchPattern = RegexPatterns.BytePackageValidatePattern;
                    break;
                default:
                    break;
            }

            e.Handled = !Regex.IsMatch(textBox.Text + e.Text, _matchPattern);
        }
    }
}
