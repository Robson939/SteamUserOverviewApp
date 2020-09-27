namespace GamePlatformsClient.Model
{
    public class Statistic
    {
        public string Name { get; set; }
        public int DefaultValue { get; set; }
        public string DisplayName { get; set; } 


        public Statistic()
        {

        }
        public Statistic(Statistic statistic)
        {
            Name = statistic.Name;
            DefaultValue = statistic.DefaultValue;
            DisplayName = statistic.DisplayName;
        }
    }
}