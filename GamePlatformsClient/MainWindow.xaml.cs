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
using System.Threading;
using System.Reflection;
using System.Net;
using GamePlatformsClient.DAL;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Configuration;
using GamePlatformsClient.Model.SteamResponseData;
using GamePlatformsClient.Model;

namespace GamePlatformsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static string playerId;

        ISteamService steamService;
        private string _loadStatusInfo = "";
        public string LoadStatusInfo 
        {
            get
            {
                return _loadStatusInfo;
            }
            set
            {
                _loadStatusInfo = value;
                OnPropertyChanged();
            }
        }

        //private ObservableCollection<GameListViewItem> _gameListViewItems = new ObservableCollection<GameListViewItem>();
        public ObservableCollection<GameListViewItem> GameListViewItems { get; set; } = new ObservableCollection<GameListViewItem>();
        public IEnumerable<Achievement> Achievements { get; set; }// = new IEnumerable<Achievement>();
        public IEnumerable<Statistic> Statistics { get; set; }// = new List<Statistic>();
        public ObservableCollection<UserAchievement> UserAchievementList { get; set; } = new ObservableCollection<UserAchievement>();
        public ObservableCollection<UserStatistic> UserStatisticList { get; set; } = new ObservableCollection<UserStatistic>();
        public ObservableCollection<GlobalAchievement> GlobalAchievements { get; set; } = new ObservableCollection<GlobalAchievement>();



        public GameListViewItem SelectedGame { get; set; }

        //public string GetLoadStatusInfo()
        //{
        //    return loadStatusInfo;
        //}

        //public void SetLoadStatusInfo(string value)
        //{
        //    loadStatusInfo = value;
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public MainWindow()
        {
            steamService = new SteamService();
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
                LoadStatusInfo = ex.ToString();
            }
        }

        CancellationTokenSource cancellationTokenSource = null;

        public async Task GetInfo()
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
                LoadStatusInfo += "Cancellation requested" + Environment.NewLine;
                return;
            });

            #endregion

            try
            {
                #region Get user info

                LoadStatusInfo = "Get user info";

                ResolveVanityURLData.Rootobject resolveVanityURLData = await steamService.ResolveVanityURLData(nickname.Text, cancellationTokenSource.Token);
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
                
                playerId = resolveVanityURLData.Response.Steamid;

                #endregion

                #region Get user games

                LoadStatusInfo = "Get user games";

                GetOwnedGamesData.Rootobject getOwnedGamesData = await steamService.GetOwnedGamesData(playerId, cancellationTokenSource.Token);

                //listView.ClearValue(ItemsControl.ItemsSourceProperty);

                List<GameListViewItem> playerGames = new List<GameListViewItem>();

                await Task.Run(() =>
                {
                    //Dispatcher.Invoke(() =>
                    //{
                    //    foreach (var element in getOwnedGamesData.Response.Games)
                    //    {
                    //        playerGames.Add(new GameListViewItem()
                    //        {
                    //            Id = element.Appid,
                    //            GameTitle = element.Name,
                    //            GameIcon = $"https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/{element.Appid}/{element.Img_icon_url}.jpg"
                    //        });
                    //    }

                    //    listView.ItemsSource = playerGames.OrderBy(x => x.GameTitle).ToList();
                    //    listView.SelectedIndex = 0;

                    //});

                    //GameListViewItems = new ObservableCollection<GameListViewItem>


                    Dispatcher.Invoke(() =>
                    {
                        GameListViewItems.Clear();
                        
                        GameListViewItems = new ObservableCollection<GameListViewItem>((getOwnedGamesData.Response.Games.Select(element => new GameListViewItem()
                        {
                            Id = element.Appid,
                            GameTitle = element.Name,
                            GameIcon = $"https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/{element.Appid}/{element.Img_icon_url}.jpg"
                        }).OrderBy(x => x.GameTitle)));

                        SelectedGame = GameListViewItems.First();
                    });
                }, cancellationTokenSource.Token);

                cancellationTokenSource.Token.ThrowIfCancellationRequested();

                #endregion

                LoadStatusInfo = $"Data loaded for user: {nickname.Text}";
            }
            catch (OperationCanceledException)
            {
                LoadStatusInfo = "The operation was cancelled";
            }
            catch (Exception ex)
            {
                LoadStatusInfo = ex.ToString();
            }
            finally
            {
                cancellationTokenSource = null;
                ProgressBar.Visibility = Visibility.Hidden;
                send.Content = "Send";
            }
        }


        string gameHeaderURI = "https://steamcdn-a.akamaihd.net/steam/apps/9/header.jpg";
        CancellationTokenSource cancellationTokenSourceSelectionChanged = null;




        private async void GamesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cancellationTokenSourceSelectionChanged != null)
            {
                cancellationTokenSourceSelectionChanged.Cancel();
            }

            cancellationTokenSourceSelectionChanged = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSourceSelectionChanged.Token;


            try
            {
                if (((ListView)sender).Items == null || ((ListView)sender).Items.Count == 0)
                {
                    return;
                }

                #region game miniature

                GameListViewItem selectedItem = (GameListViewItem)((ListView)sender).SelectedItem;


                AccessText accessText = gameTitleLabel.Content as AccessText;
                accessText.Text = selectedItem.GameTitle;
                gameScreen.Source = new BitmapImage(new Uri(gameHeaderURI.Replace("9", selectedItem.Id.ToString())));

                #endregion

                #region achievements and stats

                {
                    GetSchemaForGameData.Rootobject gameData = await steamService.GetSchemaForGameData(selectedItem.Id.ToString(), cancellationToken);
                    UserAchievementList.Clear();
                    UserStatisticList.Clear();

                    if (gameData.Game.AvailableGameStats != null)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        if (gameData.Game.AvailableGameStats.Achievements != null)
                        {
                            await Task.Run(() => 
                            {
                                Dispatcher.Invoke(() => 
                                {
                                    Achievements = gameData.Game.AvailableGameStats.Achievements.Select(x =>
                                        new Achievement()
                                        {
                                            ApiName = x.Name,
                                            DisplayName = x.DisplayName,
                                            Description = x.Description,
                                            Icongray = x.Icongray,
                                            Icon = x.Icon
                                        }).OrderBy(x => x.DisplayName);
                                });
                            }, cancellationToken);
                        }
                        cancellationToken.ThrowIfCancellationRequested();

                        if (gameData.Game.AvailableGameStats.Stats != null)
                        {
                            await Task.Run(() =>
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    Statistics = gameData.Game.AvailableGameStats.Stats.Select(x =>
                                        new Statistic()
                                        {
                                            Name = x.Name,
                                            DisplayName = x.DisplayName,
                                            DefaultValue = x.Defaultvalue
                                        }).OrderBy(x => x.DisplayName);
                                });
                            }, cancellationToken);
                        }
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }

                #endregion

                #region user achievements

                cancellationToken.ThrowIfCancellationRequested();  
                GetPlayerAchievementsData.Rootobject achievementsData = await steamService.GetPlayerAchievementsData(playerId, selectedItem.Id.ToString(), cancellationToken);

                await Task.Run(() => 
                {
                    Dispatcher.Invoke(() => 
                    {
                        UserAchievementList = new ObservableCollection<UserAchievement>(Achievements.Select(x => new UserAchievement(x)).ToList());

                        foreach (var element in achievementsData.Playerstats.Achievements)
                        {
                            UserAchievement ach = UserAchievementList.FirstOrDefault(x => x.ApiName == element.Apiname);
                            if (ach != null)
                            {
                                ach.Achieved = element.Achieved == 0;
                                ach.CurrentIcon = element.Achieved == 0 ? ach.Icongray : ach.Icon;
                            }
                        }
                    });
                }, cancellationToken);

                UserAchievementList = new ObservableCollection<UserAchievement>(UserAchievementList.OrderBy(x => x.Achieved));
                cancellationToken.ThrowIfCancellationRequested();

                #endregion

                #region user stats

                cancellationToken.ThrowIfCancellationRequested();
                GetUserStatsForGameData.Rootobject statsData = await steamService.GetUserStatsForGame(playerId, selectedItem.Id.ToString(), cancellationToken);

                if (statsData.Playerstats.Stats != null)
                    await Task.Run(() =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            UserStatisticList = new ObservableCollection<UserStatistic>(Statistics.Select(x => new UserStatistic(x)).ToList());

                            foreach (var element in statsData.Playerstats.Stats)
                            {
                                UserStatistic stat = UserStatisticList.FirstOrDefault(x => x.Name == element.Name);
                                if (stat != null)
                                {
                                    stat.UserValue = element.Value;
                                }
                            }
                        });
                    }, cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();

                #endregion

                #region global stats

                cancellationToken.ThrowIfCancellationRequested();




                #endregion

                #region global achievements

                cancellationToken.ThrowIfCancellationRequested();
                GetGlobalAchievementPercentagesForAppData.Rootobject globalAchievementsData = await steamService.GetGlobalAchievementPercentagesForAppData(selectedItem.Id.ToString(), cancellationToken);

                await Task.Run(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        GlobalAchievements = new ObservableCollection<GlobalAchievement>(Achievements.Select(x => new GlobalAchievement(x)).ToList());

                        foreach (var element in globalAchievementsData.Achievementpercentages.Achievements)
                        {
                            GlobalAchievement ach = GlobalAchievements.FirstOrDefault(x => x.ApiName == element.Name);
                            if (ach != null)
                            {
                                ach.Percent = element.Percent;
                            }
                        }
                    });
                }, cancellationToken);

                GlobalAchievements = new ObservableCollection<GlobalAchievement>(GlobalAchievements.OrderBy(x => x.Percent).Reverse());

                #endregion

                LoadStatusInfo = "Done";

            }
            catch (OperationCanceledException)
            {
                LoadStatusInfo = "The operation was cancelled";
            }
            catch (HttpRequestException)
            {
                LoadStatusInfo = "Bad Request";
            }
            catch (Exception ex)
            {
                LoadStatusInfo = ex.ToString();
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
        private uint _id;
        public uint Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private string _gameTitle;
        public string GameTitle
        {
            get
            {
                return _gameTitle;
            }
            set
            {
                _gameTitle = value;
            }
        }
        private string _gameIcon;
        public string GameIcon
        {
            get
            {
                return _gameIcon;
            }
            set
            {
                _gameIcon = value;
            }
        }
    }



    //public class GameListViewItem: INotifyPropertyChanged
    //{
    //    private uint _id;
    //    public uint Id 
    //    {
    //        get
    //        {
    //            return _id;
    //        }
    //        set
    //        {
    //            _id = value;
    //            OnPropertyChanged("Id");
    //        }
    //    }

    //    private string _gameTitle;
    //    public string GameTitle 
    //    {
    //        get
    //        {
    //            return _gameTitle;
    //        }
    //        set
    //        {
    //            _gameTitle = value;
    //            OnPropertyChanged("GameTitle");
    //        }
    //    }
    //    private string _gameIcon;
    //    public string GameIcon 
    //    {
    //        get
    //        {
    //            return _gameIcon;
    //        }
    //        set
    //        {
    //            _gameIcon = value;
    //            OnPropertyChanged("GameIcon");
    //        }
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected void OnPropertyChanged(string name = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    //    }
    //}
}