using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Forms
{
    public class Customer : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(FirstName)));
            }
        }

        private string _surname;

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Surname)));
            }
        }

        public int Age { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
