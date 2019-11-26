using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dania_Kalender_Datamodel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public (DateTime, string) AllEvents { get; set; }
        public (DateTime, string) FilteredEvents { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
