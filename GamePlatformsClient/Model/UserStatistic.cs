namespace GamePlatformsClient.Model
{
    public class UserStatistic : Statistic
    {
        public int UserValue { get; set; }

        public UserStatistic(Statistic statistic) : base(statistic)
        {

        }
    }
}