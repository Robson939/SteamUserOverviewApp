using System;
using System.Collections.Generic;
using System.Text;

namespace GamePlatformsClient.Model
{
    public class GlobalAchievement : Achievement
    {
        public float Percent { get; set; }

        public GlobalAchievement(Achievement achievement) : base(achievement)
        {

        }
    }
}