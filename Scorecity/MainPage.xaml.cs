using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Scorecity
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
	public sealed partial class MainPage : Page
    {

        public string date { get; set; }
        public ScoreBoardViewModel Sbvm { get; set; }
        public BoxScoreViewModel Bsvm { get; set; }
        ThreadPoolTimer PeriodicTimer;
        public bool dateChanged = true;
        static SemaphoreSlim sem = new SemaphoreSlim(1, 1);

        public MainPage()
        {
            this.InitializeComponent();
            date = DateTime.Now.ToString("yyyyMMdd");
            Sbvm = new ScoreBoardViewModel();
            Bsvm = new BoxScoreViewModel();
            refreshScoreboard();
            startScoreboardTimer();
        }


        private async void refreshScoreboard()
        {
            await sem.WaitAsync();
            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
            nomoregames.Visibility = Visibility.Collapsed;

            try
            {
                if (isInternetConnected)
                {
                    await Sbvm.updateScoreBoardViewModel(date);

                    if (scoreboard.Items.Count > 0 && scoreboard.SelectedIndex == -1)
                        scoreboard.SelectedIndex = 0;

                    string gameId;

                    if (scoreboard.Items.Count > 0)
                        gameId = Sbvm.Sb[scoreboard.SelectedIndex].GameId;
                    else
                        gameId = null;

                    await Bsvm.updateBoxScoreViewModel(date, gameId);
                }
            } finally
            {
                if (homeListView.Items.Count == 0 || !isInternetConnected)
                {
                    boxScore.Visibility = Visibility.Collapsed;

                    if (!isInternetConnected)
                    {
                        HideSb.Content = "\xE72A";
                        scoreboard.Visibility = Visibility.Collapsed;
                        nomoregames.Text = "Check your internet connection.";
                    }
                    else if (scoreboard.Items.Count > 0)
                        nomoregames.Text = "The game has not yet started.";
                    else
                        nomoregames.Text = "There are no games for this date.";

                    nomoregames.Visibility = Visibility.Visible;
                } else {
                    nomoregames.Visibility = Visibility.Collapsed;
                    HideSb.Content = "\xE72B";
                    scoreboard.Visibility = Visibility.Visible;
                    boxScore.Visibility = Visibility.Visible;
                }

                sem.Release();
                ProgressRing.IsActive = false;
                dateChanged = false;
            }
        }


        private void startScoreboardTimer()
        {
            if (date == DateTime.Now.ToString("yyyyMMdd")) {
                PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer(
                async (source) =>
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, refreshScoreboard);
                }, TimeSpan.FromSeconds(7));
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (scoreboard.Visibility == Visibility.Visible)
            {
                scoreboard.Visibility = Visibility.Collapsed;
                button.Content = "\xE72A";
            }
            else
            {
                scoreboard.Visibility = Visibility.Visible;
                button.Content = "\xE72B";
            }
        }


        private async void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (calendar.Date.HasValue)
            {
                date = calendar.Date.Value.ToString("yyyyMMdd");

                if (date != DateTime.Now.ToString("yyyyMMdd"))
                {
                    PeriodicTimer.Cancel();
                } else
                {
                    if (PeriodicTimer == null)
                        startScoreboardTimer();
                }

                    dateChanged = true;
                ProgressRing.IsActive = true;
                boxScore.Visibility = Visibility.Collapsed;
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, refreshScoreboard);
            }
        }

        private async void scoreboard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (scoreboard.SelectedIndex != -1 && !dateChanged)
            {
                ProgressRing.IsActive = true;
                boxScore.Visibility = Visibility.Collapsed;
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, refreshScoreboard);
            }
        }

        private void Show_Home_Only(object sender, RoutedEventArgs e)
        {
            homeButton.Background = new SolidColorBrush(Color.FromArgb(255, 34, 34, 34));
            awayButton.Background = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
            bothButton.Background = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
            homeListView.SelectedIndex = -1;
            awayListView.SelectedIndex = -1;
            homeListView.Visibility = Visibility.Visible;
            awayListView.Visibility = Visibility.Collapsed;
        }

        private void Show_Away_Only(object sender, RoutedEventArgs e)
        {
            homeButton.Background = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
            awayButton.Background = new SolidColorBrush(Color.FromArgb(255, 34, 34, 34));
            bothButton.Background = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
            homeListView.SelectedIndex = -1;
            awayListView.SelectedIndex = -1;
            homeListView.Visibility = Visibility.Collapsed;
            awayListView.Visibility = Visibility.Visible;
        }

        private void Show_Both_Home_Away(object sender, RoutedEventArgs e)
        {
            homeButton.Background = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
            awayButton.Background = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
            bothButton.Background = new SolidColorBrush(Color.FromArgb(255, 34, 34, 34));
            homeListView.SelectedIndex = -1;
            awayListView.SelectedIndex = -1;
            homeListView.Visibility = Visibility.Visible;
            awayListView.Visibility = Visibility.Visible;
        }
    }
}
