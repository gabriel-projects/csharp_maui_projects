using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.MindfulZen.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private DateTime _dateTime;
        private Timer _timer;

        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                if (_dateTime != value)
                {
                    _dateTime = value;
                    OnPropertyChanged(); 
                }
            }
        }

        public HomeViewModel()
        {
            this.DateTime = DateTime.Now;

            _timer = new Timer(new TimerCallback((s) => DateTime = DateTime.Now),
                               null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }
    }
}
