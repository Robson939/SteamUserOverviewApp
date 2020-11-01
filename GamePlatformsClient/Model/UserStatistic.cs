namespace GamePlatformsClient.Model
{
    public class UserStatistic : Statistic
    {
        public float UserValue { get; set; }

        public UserStatistic(Statistic statistic) : base(statistic)
        {

        }
    }
}