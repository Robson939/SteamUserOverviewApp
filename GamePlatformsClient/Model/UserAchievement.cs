namespace GamePlatformsClient.Model
{
    public class UserAchievement : Achievement
    {
        public bool Achieved { get; set; }
        public long UnlockTime { get; set; }

        public UserAchievement(Achievement achievement) : base(achievement)
        {
            
        }
    }
}