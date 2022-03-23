using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace temanpd
{
    public class TabVM : INotifyPropertyChanged
    {
        string _Header;
        public string Header
        {
            get => _Header;
            set
            {
                _Header = value;
                OnPropertyChanged();
            }
        }
        string _Text;
        public string Text
        {
            get => _Text;
            set
            {
                _Text = value;
                OnPropertyChanged();
            }
        }
        string _Path;
        public string Path
        {
            get => _Path;
            set
            {
                _Path = value;
                OnPropertyChanged();
            }
        }
        bool _IsPlaceholder = false;
        public bool IsPlaceholder
        {
            get => _IsPlaceholder;
            set
            {
                _IsPlaceholder = value;
                OnPropertyChanged();
            }
        }


        public object CurrentTab { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
