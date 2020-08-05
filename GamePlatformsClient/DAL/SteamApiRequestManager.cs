using System;
using System.Collections.Generic;
using System.Text;

namespace GamePlatformsClient
{
    public static class SteamApiRequestManager
    {
        public static string key = "39303736A8DB0BC62D1142B202DEA41B";

        public static string GetResolveVanityURL(string nickname) =>
            SteamApiRequestURL.resolveVanityURL
            .Replace("{0}", key)
            .Replace("{1}", nickname);
        public static string GetAppList() =>
            SteamApiRequestURL.getAppList;
        public static string GetOwnedGames(string userSteamId, bool includeAppinfo = true) =>
            SteamApiRequestURL.getOwnedGames
            .Replace("{0}", key)
            .Replace("{1}", userSteamId)
            .Replace("{2}", includeAppinfo.ToString());
        public static string GetPlayerAchievements(string userSteamID, string appid, string language = "english") =>
            SteamApiRequestURL.getPlayerAchievements
            .Replace("{0}", key)
            .Replace("{1}", userSteamID)
            .Replace("{2}", appid)
            .Replace("{3}", language);
        public static string GetGlobalAchievementPercentagesForApp(string gameid) =>
            SteamApiRequestURL.getGlobalAchievementPercentagesForApp
            .Replace("{0}", gameid);
        public static string GetSchemaForGame(string appid, string language = "english") =>
            SteamApiRequestURL.getSchemaForGame
            .Replace("{0}", key)
            .Replace("{1}", appid)
            .Replace("{2}", language);
        public static string GetUserStatsForGame(string userSteamID, string appid) =>
            SteamApiRequestURL.getUserStatsForGame
            .Replace("{0}", key)
            .Replace("{1}", userSteamID)
            .Replace("{2}", appid);

        private class SteamApiRequestURL
        {
            public const string getAppList =                            "https://api.steampowered.com/ISteamApps/GetAppList/v2/";
            public const string resolveVanityURL =                      "https://api.steampowered.com/ISteamUser/ResolveVanityURL/v1/?key={0}&vanityurl={1}";
            public const string getOwnedGames =                         "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={0}&steamid={1}&include_appinfo={2}&format=json";
            public const string getPlayerAchievements =                 "https://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v1/?key={0}&steamID={1}&appid={2}&l={3}";
            public const string getGlobalAchievementPercentagesForApp = "https://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v2/?gameid={0}";
            public const string getSchemaForGame =                      "https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2/?key={0}&appid={1}&l={2}";
            public const string getUserStatsForGame =                   "https://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v2/?key={0}&steamid={1}&appid={2}";
        }
    }
}






//{"response":{"game_count":211,"games":[{"appid":220,"name":"Half-Life 2","playtime_forever":0,"img_icon_url":"fcfb366051782b8ebf2aa297f3b746395858cb62","img_logo_url":"e4ad9cf1b7dc8475c1118625daf9abd4bdcbcad0","has_community_visible_stats":true,"playtime_windows_forever":0,"playtime_mac_forever":0,"playtime_linux_forever":0}