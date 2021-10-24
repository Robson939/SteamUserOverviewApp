namespace GamePlatformsClient.Model
{
    //todo: ZAMIENIĆ NA INTERFEJS 
    public class Achievement 
    {
        public string ApiName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int Defaultvalue { get; set; }
        public bool Hidden { get; set; }
        public string Icon { get; set; }
        public string Icongray { get; set; }
        public string CurrentIcon { get; set; }

        public Achievement()
        { 
        }
        public Achievement(Achievement achievement)
        {
            ApiName = achievement.ApiName;
            DisplayName = achievement.DisplayName;
            Description = achievement.Description;
            Defaultvalue = achievement.Defaultvalue;
            Hidden = achievement.Hidden;
            Icon = achievement.Icon;
            Icongray = achievement.Icongray;
            CurrentIcon = achievement.CurrentIcon;
        }
    }
}