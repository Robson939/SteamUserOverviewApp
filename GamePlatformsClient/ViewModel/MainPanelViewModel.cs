using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GamePlatformsClient.ViewModel
{
    public class MainPanelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyChanged)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
        }

        public MainPanelViewModel()
        {

        }











    }
}
