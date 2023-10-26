using System;

namespace DateTimeConverter.Models
{
    class Date : NotifyPropertyChanged
    {
        private long fileTime;
        private int day;
        private int month;
        private int year;
        private int minutes;
        private int seconds;
        private int hour;
        private int milliseconds;

        public int Day { get => day; set { day = value; OnPropertyChanged(); } }
        public int Month { get => month; set { month = value; OnPropertyChanged(); } }
        public int Year { get => year; set { year = value; OnPropertyChanged(); } }
        public int Hour { get => hour; set { hour = value; OnPropertyChanged(); } }
        public int Minutes { get => minutes; set { minutes = value; OnPropertyChanged(); } }
        public int Seconds { get => seconds; set { seconds = value; OnPropertyChanged(); } }
        public int Milliseconds { get => milliseconds; set { milliseconds = value; OnPropertyChanged(); } }
        public long TotalTicks { get; set; }
        public long FileTime
        {
            get => fileTime; set
            {
                fileTime = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateTimeUTC { get; set; }
        public DateTime LocalDateTime { get; set; }
        public DateTimeOffset Offsets { get; set; }

        public Date()
        {
            LocalDateTime = DateTime.Now;
            DateTimeUTC = DateTime.UtcNow;

            Day = LocalDateTime.Day;
            Month = LocalDateTime.Month;
            Year = LocalDateTime.Year;

            Hour = LocalDateTime.Hour;
            Minutes = LocalDateTime.Minute;
            Seconds = LocalDateTime.Second;
            Milliseconds = LocalDateTime.Millisecond;

            Offsets = new DateTimeOffset(LocalDateTime);

            FileTime = ToFileTime(dateTime: LocalDateTime);
        }

        public void ToDateTime(long fileTime)
        {
            var _dt = DateTime.FromFileTime(fileTime);

            Day = _dt.Day; Month = _dt.Month; Year = _dt.Year;
            Hour = _dt.Hour; Minutes = _dt.Minute;
            Seconds = _dt.Second; Milliseconds = _dt.Millisecond;
        }

        public static long ToFileTime(DateTime dateTime)
            => dateTime.ToFileTime();
    }
}
