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
using GamePlatformsClient.DAL;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Configuration;

namespace GamePlatformsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static string playerId;

        ISteamService steamRepository;
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
        public ObservableCollection<UserAchievement> UserAchievementList { get; set; } = new ObservableCollection<UserAchievement>();
        public ObservableCollection<UserStatistic> UserStatisticList { get; set; } = new ObservableCollection<UserStatistic>();


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
            steamRepository = new SteamService();
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

                ResolveVanityURLData.Rootobject resolveVanityURLData = await steamRepository.ResolveVanityURLData(nickname.Text, cancellationTokenSource.Token);
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
                
                playerId = resolveVanityURLData.Response.Steamid;

                #endregion

                #region Get user games

                LoadStatusInfo = "Get user games";

                GetOwnedGamesData.Rootobject getOwnedGamesData = await steamRepository.GetOwnedGamesData(playerId, cancellationTokenSource.Token);

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
        //            loadStatusInfo += "Cancellation requested" + Environment.NewLine;
        //            ProgressBar.Visibility = Visibility.Hidden;
        //            return;
        //        });
        //        #endregion

        //        try
        //        {

        //            loadStatusInfo = "Get user info";

        //            var response = await httpClient.GetAsync(SteamApiRequestManager.GetResolveVanityURL(nickname.Text), cancellationTokenSource.Token);
        //            response.EnsureSuccessStatusCode();




        //            loadStatusInfo = "Get user id";

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




        //            loadStatusInfo = "Get user games";

        //            response = await httpClient.GetAsync(SteamApiRequestManager.GetOwnedGames(userId), cancellationTokenSource.Token);
        //            response.EnsureSuccessStatusCode();

        //            string contentt = await response.Content.ReadAsStringAsync();




        //            loadStatusInfo = "Set user games list";

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

        //            loadStatusInfo = $"Data loaded for user: {nickname.Text}";
        //            ProgressBar.Visibility = Visibility.Hidden;
        //            send.Content = "Send";
        //        }
        //        catch (Exception ex)
        //        {
        //            loadStatusInfo = ex.ToString();
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
                    GetSchemaForGameData.Rootobject data = await steamRepository.GetSchemaForGameData(selectedItem.Id.ToString(), cancellationToken);


                    //to do: working on single task inside this method (getting achievements data task -> continue with getting achievements of player)

                    //achievementsList.ClearValue(ItemsControl.ItemsSourceProperty);
                    UserAchievementList.Clear();

                    if (data.Game.AvailableGameStats != null)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        if (data.Game.AvailableGameStats.Achievements != null)
                        {
                            await Task.Run(() => 
                            {
                                Dispatcher.Invoke(() => 
                                {
                                    foreach (var x in data.Game.AvailableGameStats.Achievements)
                                    {
                                        UserAchievementList.Add(new UserAchievement()
                                        {
                                            ApiName = x.Name,
                                            DisplayName = x.DisplayName,
                                            Description = x.Description,
                                            Icongray = x.Icongray,
                                            Icon = x.Icon
                                        });
                                    }
                                });
                            }, cancellationToken);
                        }

                        cancellationToken.ThrowIfCancellationRequested();

                        if (data.Game.AvailableGameStats.Stats != null)
                        {
                            await Task.Run(() =>
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    foreach (var x in data.Game.AvailableGameStats.Stats)
                                    {
                                        UserStatisticList.Add(new UserStatistic()
                                        {
                                            DisplayName = x.DisplayName,
                                            DefaultValue = x.Defaultvalue,
                                            Name = x.Name
                                        });
                                    }
                                });
                            }, cancellationToken);
                        }
                    }
                }

                #endregion

                #region user achievements

                cancellationToken.ThrowIfCancellationRequested();

                {
                    GetPlayerAchievementsData.Rootobject data = await steamRepository.GetPlayerAchievementsData(playerId, selectedItem.Id.ToString(), cancellationToken);

                    await Task.Run(() => 
                    {
                        Dispatcher.Invoke(() => 
                        { 
                            foreach (var element in data.Playerstats.Achievements)
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

                    //achievementsList.ItemsSource = UserAchievementList.OrderBy(x => x.Achieved).ToList();
                    UserAchievementList = new ObservableCollection<UserAchievement>(UserAchievementList.OrderBy(x => x.Achieved));
                }

                cancellationToken.ThrowIfCancellationRequested();

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