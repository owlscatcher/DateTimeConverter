using DateTimeConverter.Commands;
using DateTimeConverter.Helpers;
using DateTimeConverter.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DateTimeConverter.ViewModels
{
    class MainWindowViewModel : NotifyPropertyChanged
    {
        struct ThemesName
        {
            public static string Light => "Light";
            public static string Dark => "Dark";
        }

        struct ColorName
        {
            public static string DeepBlue => "#FF141F1F";
            public static string LightBlue => "#FFCCE5FF";
            public static string Black => "#FF303030";
            public static string White => "#FFFFFFFF";
        }

        struct MinMaxIconName
        {
            public static string Minimize => "Minimize";
            public static string Maximize => "Maximize";
        }
        struct WindowStateName
        {
            public static string Noraml => "Normal";
            public static string Maximize => "Maximized";
            public static string Minimize => "Minimized";

        }

        private string _actualTheme = ThemesName.Light;
        public string ActualTheme
        {
            get => _actualTheme;
            set
            {
                _actualTheme = value;
                OnPropertyChanged();
            }
        }

        private string _actualColor = ColorName.Black;
        public string ActualColor
        {
            get => _actualColor;
            set
            {
                _actualColor = value;
                OnPropertyChanged();
            }
        }

        private string _actualBtnColor = ColorName.White;
        public string ActualBtnColor
        {
            get => _actualBtnColor;
            set
            {
                _actualBtnColor = value;
                OnPropertyChanged();
            }
        }

        private string _actualBorderColor = ColorName.LightBlue;
        public string ActualBorderColor
        {
            get => _actualBorderColor;
            set
            {
                _actualBorderColor = value;
                OnPropertyChanged();
            }
        }

        private string _actualMinMaxIcon = MinMaxIconName.Maximize;
        public string ActualMinMaxIcon
        {
            get => _actualMinMaxIcon;
            set
            {
                _actualMinMaxIcon = value;
                OnPropertyChanged();
            }
        }

        private string _actualWindiowState = WindowStateName.Noraml;
        public string ActualWindiowState
        {
            get { return _actualWindiowState; }
            set
            {
                _actualWindiowState = value;
                OnPropertyChanged();
            }
        }

        public bool IsThemeDark { get; set; } = false;
        public bool IsMaximize { get; set; } = false;

        private Date _date = new Date();
        public Date Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private ARMSByte _ARMSByte = new();

        public ARMSByte ARMSByte
        {
            get { return _ARMSByte; }
            set { _ARMSByte = value; OnPropertyChanged(); }
        }

        public ICommand GetCRC16
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var _input = obj is not null ? obj as string : String.Empty;


                    if (_input.Length is 12)
                    {
                        _input += "0000";
                        var _db = StringToByteArray(_input);
                        var _crc16full = VerificationPackageHelper.GetCrc16(_db);
                        ARMSByte.CRC16L = _crc16full[0];
                        ARMSByte.CRC16H = _crc16full[1];
                    }

                });
            }
        }

        public ICommand ConvertToFileTime
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                    DateTime _ = new(
                        year: Date.Year,
                        month: Date.Month,
                        day: Date.Day,
                        hour: Date.Hour,
                        minute: Date.Minutes,
                        second: Date.Seconds,
                        millisecond: Date.Milliseconds);

                    Date.FileTime = Date.ToFileTime(_);
                });
            }
        }

        public ICommand ConvertFromFileTime
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var _fileTime = Convert.ToInt64(obj as string);

                    Date.ToDateTime(_fileTime);
                });
            }
        }

        public ICommand ConvertByteToFloat
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var _input = obj is not null ? obj as string : String.Empty;

                    if (_input.Length is 8)
                    {
                        var _db = StringToByteArray(_input);
                        ARMSByte.Value = BitConverter.ToSingle(_db);
                    }
                });
            }
        }

        public static byte[] StringToByteArray(string str)
        {
            return (from x in Enumerable.Range(0, str.Length)
                    where x % 2 == 0
                    select Convert.ToByte(str.Substring(x, 2), 16)).ToArray();
        }

        public ICommand CloseWindow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (obj is not null)
                    {
                        Window homeWindow = obj as Window;
                        homeWindow.Close();
                    }
                });
            }
        }

        public ICommand ChangeTheme
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    IsThemeDark = !IsThemeDark;
                    ActualTheme = IsThemeDark ? ThemesName.Dark : ThemesName.Light;
                    ActualColor = IsThemeDark ? ColorName.White : ColorName.Black;
                    ActualBtnColor = IsThemeDark ? ColorName.Black : ColorName.White;
                    ActualBorderColor = IsThemeDark ? ColorName.DeepBlue : ColorName.LightBlue;
                });
            }
        }

        public ICommand ChangeWindowSize
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    IsMaximize = !IsMaximize;
                    ActualMinMaxIcon = IsMaximize ? MinMaxIconName.Minimize : MinMaxIconName.Maximize;
                    ActualWindiowState = IsMaximize ? WindowStateName.Maximize : WindowStateName.Noraml;
                });
            }
        }
    }
}
