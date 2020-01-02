using System;
using System.ComponentModel;

namespace CalendarLibrary.CalendarTab
{
    class CalendarTabViewModel : INotifyPropertyChanged
    {
        private readonly Calendar calendar;

        public string CalendarName
        {
            get
            {
                return calendar.CalendarName;
            }
            set
            {
                Console.WriteLine("Setting");
                if(!string.Equals(calendar.CalendarName, value))
                {
                    Console.WriteLine("Changed");
                    calendar.CalendarName = value;
                    OnPropertyChanged(nameof(CalendarName));
                }
            }
        }

        public CalendarTabViewModel()
        {
            calendar = new Calendar();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
