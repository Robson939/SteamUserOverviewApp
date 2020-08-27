using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GamePlatformsClient.ViewModel
{
    class MainPanelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyChanged)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
        }













    }
}
