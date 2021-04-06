using GamePlatformsClient.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GamePlatformsClient.ViewModel
{
    public class MainPanelViewModel : INotifyPropertyChanged
    {
        private ISteamService steamService;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyChanged)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
        }

        public MainPanelViewModel(ISteamService steamService)
        {
            this.steamService = steamService;
        }











    }
}
