using System;
using System.Collections.Generic;
using System.Text;
using GamePlatformsClient.DAL;
using GamePlatformsClient.ViewModel;

namespace GamePlatformsClient
{
    public class ViewModelLocator
    {
        private static ISteamService steamService = new SteamService();
        private static MainPanelViewModel mainPanelViewModel = new MainPanelViewModel(steamService);
    
        public static MainPanelViewModel MainPanelViewModel
        {
            get
            {
                return mainPanelViewModel;
            }
        }
    
    }
}
