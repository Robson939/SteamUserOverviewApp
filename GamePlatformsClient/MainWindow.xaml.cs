using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GamePlatformsClient.SteamResponseData;
using System.Threading;
using System.Reflection;
using System.Net;

namespace GamePlatformsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string playerId;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this; 
        }

        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await GetInfo();
            }
            catch (Exception ex)
            {
                DataLoadStatus.Text = ex.ToString();
            }
        }

        CancellationTokenSource cancellationTokenSource = null;

        public async Task GetInfo()
        {
            using (var httpClient = new HttpClient())
            {
                ProgressBar.Visibility = Visibility.Visible;
                ProgressBar.IsIndeterminate = true;
                send.Content = "Cancel";

                #region Cancellation
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                    return;
                }

                cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.Token.Register(() =>
                {
                    DataLoadStatus.Text += "Cancellation requested" + Environment.NewLine;
                    return;
                });
                #endregion

                try
                {
                    #region Get user info

                    DataLoadStatus.Text = "Get user info";

                    var response = await httpClient.GetAsync(SteamApiRequestManager.GetResolveVanityURL(nickname.Text), cancellationTokenSource.Token);
                    response.EnsureSuccessStatusCode();

                    #endregion

                    #region Get user id

                    DataLoadStatus.Text = "Get user id";

                    string content = await response.Content.ReadAsStringAsync();
                    //string userId = ""; 
                    await Task.Run(() =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            ResolveVanityURLData.Rootobject data = JsonSerializer.Deserialize<ResolveVanityURLData.Rootobject>(content);
                            playerId = data.Response.Steamid;
                        });
                    }, cancellationTokenSource.Token);

                    cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    #endregion

                    #region Get user games

                    DataLoadStatus.Text = "Get user games";

                    response = await httpClient.GetAsync(SteamApiRequestManager.GetOwnedGames(playerId /*userId*/), cancellationTokenSource.Token);
                    response.EnsureSuccessStatusCode();

                    string contentt = await response.Content.ReadAsStringAsync();

                    #endregion

                    #region Set user games list

                    DataLoadStatus.Text = "Set user games list";


                    GetOwnedGamesData.Rootobject data = JsonSerializer.Deserialize<GetOwnedGamesData.Rootobject>(contentt);
                    listView.ClearValue(ItemsControl.ItemsSourceProperty);

                    List<GameListViewItem> playerGames = new List<GameListViewItem>();

                    await Task.Run(() =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            foreach (var element in data.Response.Games)
                            {
                                playerGames.Add(new GameListViewItem()
                                {
                                    Id = element.Appid,
                                    GameTitle = element.Name,
                                    GameIcon = $"https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/{element.Appid}/{element.Img_icon_url}.jpg"
                                });
                            }

                            listView.ItemsSource = playerGames.OrderBy(x => x.GameTitle).ToList();
                            listView.SelectedIndex = 0;

                        });
                    }, cancellationTokenSource.Token);

                    cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    #endregion



                    DataLoadStatus.Text = $"Data loaded for user: {nickname.Text}";
                }
                catch (OperationCanceledException)
                {
                    DataLoadStatus.Text = "The operation was cancelled";
                }
                catch (Exception ex)
                {
                    DataLoadStatus.Text = ex.ToString();
                }
                finally
                {
                    cancellationTokenSource = null;
                    ProgressBar.Visibility = Visibility.Hidden;
                    send.Content = "Send";
                }
            }
        }






        //public async Task GetInfo()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        ProgressBar.Visibility = Visibility.Visible;
        //        ProgressBar.IsIndeterminate = true;
        //        send.Content = "Cancel";

        //        #region Cancellation
        //        if (cancellationTokenSource != null)
        //        {
        //            cancellationTokenSource.Cancel();
        //            cancellationTokenSource = null;
        //            return;
        //        }

        //        cancellationTokenSource = new CancellationTokenSource();
        //        cancellationTokenSource.Token.Register(() =>
        //        {
        //            DataLoadStatus.Text += "Cancellation requested" + Environment.NewLine;
        //            ProgressBar.Visibility = Visibility.Hidden;
        //            return;
        //        });
        //        #endregion

        //        try
        //        {

        //            DataLoadStatus.Text = "Get user info";

        //            var response = await httpClient.GetAsync(SteamApiRequestManager.GetResolveVanityURL(nickname.Text), cancellationTokenSource.Token);
        //            response.EnsureSuccessStatusCode();




        //            DataLoadStatus.Text = "Get user id";

        //            string content = await response.Content.ReadAsStringAsync();
        //            string userId = ""; 
        //            await Task.Run(() =>
        //            {
        //                Dispatcher.Invoke(() =>
        //                {
        //                    ResolveVanityURLResult data = JsonSerializer.Deserialize<ResolveVanityURLResult>(content);
        //                    userId = data.Response.SteamId;
        //                });
        //            } , cancellationTokenSource.Token);




        //            DataLoadStatus.Text = "Get user games";

        //            response = await httpClient.GetAsync(SteamApiRequestManager.GetOwnedGames(userId), cancellationTokenSource.Token);
        //            response.EnsureSuccessStatusCode();

        //            string contentt = await response.Content.ReadAsStringAsync();




        //            DataLoadStatus.Text = "Set user games list";

        //            await Task.Run(() => 
        //            {
        //                Dispatcher.Invoke(() =>
        //                {
        //                    GetOwnedGamesResult data = JsonSerializer.Deserialize<GetOwnedGamesResult>(contentt);
        //                    //result.ItemsSource = data.Response.Games;

        //                    listView.Items.Clear();

        //                    listView.ItemsSource = data.Response.Games.Select(x => new GameListViewItem()
        //                    {
        //                        id = x.Appid,
        //                        gameTitle = x.Name,
        //                        gameIcon = new Image
        //                        {
        //                            Source = new BitmapImage(new Uri($"https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/{x.Appid}/{x.IconUrl}.jpg"))
        //                        }
        //                    }).ToList();

        //                    listView.SelectedIndex = 0; 
        //                });
        //            }, cancellationTokenSource.Token);

        //            DataLoadStatus.Text = $"Data loaded for user: {nickname.Text}";
        //            ProgressBar.Visibility = Visibility.Hidden;
        //            send.Content = "Send";
        //        }
        //        catch (Exception ex)
        //        {
        //            DataLoadStatus.Text = ex.ToString();
        //        }
        //        finally
        //        {
        //            cancellationTokenSource = null;
        //        }
        //    }
        //}

        string gameHeaderURI = "https://steamcdn-a.akamaihd.net/steam/apps/9/header.jpg";
        CancellationTokenSource cancellationTokenSourceSelectionChanged = null;


        private async void GamesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cancellationTokenSourceSelectionChanged != null)
            {
                cancellationTokenSourceSelectionChanged.Cancel();
            }

            cancellationTokenSourceSelectionChanged = new CancellationTokenSource();


            try
            {
                if (((ListView)sender).Items == null || ((ListView)sender).Items.Count == 0)
                {
                    return;
                }

                #region game miniature

                List<UserAchievement> userAchievementList = new List<UserAchievement>();
                List<UserStatistic> userStatisticList = new List<UserStatistic>();
                GameListViewItem selectedItem = (GameListViewItem)((ListView)sender).SelectedItem;




                AccessText accessText = gameTitleLabel.Content as AccessText;
                accessText.Text = selectedItem.GameTitle;
                gameScreen.Source = new BitmapImage(new Uri(gameHeaderURI.Replace("9", selectedItem.Id.ToString())));

                #endregion

                #region achievements and stats

                using (HttpClient httpClientt = new HttpClient())
                {
                    HttpResponseMessage response = 
                        await httpClientt.GetAsync(SteamApiRequestManager.GetSchemaForGame(selectedItem.Id.ToString()), cancellationTokenSourceSelectionChanged.Token);
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    GetSchemaForGameData.Rootobject data = JsonSerializer.Deserialize<GetSchemaForGameData.Rootobject>(content);

                    achievementsList.ClearValue(ItemsControl.ItemsSourceProperty);

                    //to do: working on single task inside this method (getting achievements data task -> continue with getting achievements of player)

                    if (data.Game.AvailableGameStats != null)
                    {
                        if (data.Game.AvailableGameStats.Achievements != null)
                        {
                            await Task.Run(() => 
                            {
                                Dispatcher.Invoke(() => 
                                {
                                    foreach (var x in data.Game.AvailableGameStats.Achievements)
                                    {
                                        userAchievementList.Add(new UserAchievement()
                                        {
                                            ApiName = x.Name,
                                            DisplayName = x.DisplayName,
                                            Description = x.Description,
                                            Icongray = x.Icongray,
                                            Icon = x.Icon
                                        });
                                    }
                                });
                            }, cancellationTokenSourceSelectionChanged.Token);
                        }

                        if (data.Game.AvailableGameStats.Stats != null)
                        {
                            await Task.Run(() =>
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    foreach (var x in data.Game.AvailableGameStats.Stats)
                                    {
                                        userStatisticList.Add(new UserStatistic()
                                        {
                                            DisplayName = x.DisplayName,
                                            DefaultValue = x.Defaultvalue,
                                            Name = x.Name
                                        });
                                    }
                                });
                            }, cancellationTokenSourceSelectionChanged.Token);
                        }
                    }
                }

                #endregion

                #region user achievements

                cancellationTokenSourceSelectionChanged.Token.ThrowIfCancellationRequested();

                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(SteamApiRequestManager.GetPlayerAchievements(playerId, selectedItem.Id.ToString()), cancellationTokenSourceSelectionChanged.Token);
                    response.EnsureSuccessStatusCode();

                    string content = await response.Content.ReadAsStringAsync();
                    GetPlayerAchievementsData.Rootobject data = JsonSerializer.Deserialize<GetPlayerAchievementsData.Rootobject>(content);

                    await Task.Run(() => 
                    {
                        Dispatcher.Invoke(() => 
                        { 
                            foreach (var element in data.Playerstats.Achievements)
                            {
                                UserAchievement ach = userAchievementList.FirstOrDefault(x => x.ApiName == element.Apiname);
                                if (ach != null)
                                {
                                    ach.Achieved = element.Achieved == 0;
                                    ach.CurrentIcon = element.Achieved == 0 ? ach.Icongray : ach.Icon;
                                }
                            }
                        });
                    }, cancellationTokenSourceSelectionChanged.Token);

                    achievementsList.ItemsSource = userAchievementList.OrderBy(x => x.Achieved).ToList();
                }

                cancellationTokenSourceSelectionChanged.Token.ThrowIfCancellationRequested();

                #endregion

                #region user stats

                //using (HttpClient httpClient = new HttpClient())
                //{
                //    HttpResponseMessage response = await httpClient.GetAsync(SteamApiRequestManager.GetUserStatsForGame(playerId, selectedItem.id.ToString()), cancellationTokenSourceSelectionChanged.Token);
                //    response.EnsureSuccessStatusCode();

                //    string content = await response.Content.ReadAsStringAsync();
                //    GetUserStatsForGameData.Rootobject data = JsonSerializer.Deserialize<GetUserStatsForGameData.Rootobject>(content);

                //    await Task.Run(() =>
                //    {
                //        Dispatcher.Invoke(() =>
                //        {
                //            foreach (var element in data.Playerstats.Stats)
                //            {
                //                UserStatistic stat = userStatisticList.FirstOrDefault(x => x.Name == element.Name);
                //                if (stat != null)
                //                {
                //                    stat.UserValue = element.Value;
                //                }
                //            }
                //        });
                //    }, cancellationTokenSourceSelectionChanged.Token);

                //    //achievementsList.ItemsSource = userAchievementList.OrderBy(x => x.Achieved).ToList();
                //}

                //cancellationTokenSourceSelectionChanged.Token.ThrowIfCancellationRequested();

                #endregion

            }
            catch (Exception ex)
            {
                DataLoadStatus.Text = ex.ToString();

            }
            finally
            {
                cancellationTokenSourceSelectionChanged = null;
            }
        }




        //private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var selectedItem = (SteamResponseData.App)result.SelectedItem;


        //    Uri resourceUri = new Uri(gameHeaderURI.Replace("9", selectedItem.Appid.ToString()));
        //    gameMiniature.Source = new BitmapImage(resourceUri);

        //}

    }

    public class GameListViewItem
    {
        public uint Id { get; set; }
        public string GameTitle { get; set; }
        public string GameIcon { get; set; }
    }
}