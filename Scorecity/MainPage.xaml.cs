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
        public ScoreBoardViewModel Sbvm { get; set; }
        public BoxScoreViewModel Bsvm { get; set; }
        ThreadPoolTimer PeriodicTimer;
        private string date { get; set; }
        private bool dateChanged = true;


        public MainPage()
        {
            this.InitializeComponent();
            date = DateTime.Now.ToString("yyyyMMdd");
            Sbvm = new ScoreBoardViewModel();
            Bsvm = new BoxScoreViewModel();
            startScoreboardTimer();
        }


        private async void refreshScoreboard()
        {
            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();

            if (isInternetConnected)
            {
                await Sbvm.updateScoreBoardViewModel(date);

                if (scoreboard.Items.Count > 0 && scoreboard.SelectedIndex == -1)
                    scoreboard.SelectedIndex = 0;

                string gameId = (scoreboard.Items.Count > 0) ? Sbvm.Sb[scoreboard.SelectedIndex].GameId : null;
                await Bsvm.updateBoxScoreViewModel(date, gameId);
            }

            if (!isInternetConnected)
            {
                HideSb.Content = "\xE72A";
                scoreboard.Visibility = Visibility.Collapsed;
                nomoregames.Text = "Check your internet connection.";
                nomoregames.Visibility = Visibility.Visible;
                boxScore.Visibility = Visibility.Collapsed;
            }
            else if (homeListView.Items.Count == 0 && scoreboard.Items.Count > 0)
            {
                nomoregames.Text = "This game has not yet started.";
                nomoregames.Visibility = Visibility.Visible;
                boxScore.Visibility = Visibility.Collapsed;
            }
            else if (homeListView.Items.Count == 0 && scoreboard.Items.Count == 0)
            {
                nomoregames.Text = "There are no games for this date.";
                nomoregames.Visibility = Visibility.Visible;
                boxScore.Visibility = Visibility.Collapsed;
            }
            else
            {
                nomoregames.Visibility = Visibility.Collapsed;
                HideSb.Content = "\xE72B";
                scoreboard.Visibility = Visibility.Visible;
                boxScore.Visibility = Visibility.Visible;
            }

            ProgressRing.IsActive = false;
            dateChanged = false;
        }


        private async void startScoreboardTimer()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, refreshScoreboard);

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


        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (calendar.Date.HasValue)
            {
                dateChanged = true;
                date = calendar.Date.Value.ToString("yyyyMMdd");
                boxScore.Visibility = Visibility.Collapsed;
                nomoregames.Visibility = Visibility.Collapsed;
                ProgressRing.IsActive = true;
                PeriodicTimer.Cancel();
                startScoreboardTimer();
            }
        }

        private void scoreboard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (scoreboard.SelectedIndex != -1 && !dateChanged)
            {
                boxScore.Visibility = Visibility.Collapsed;
                nomoregames.Visibility = Visibility.Collapsed;
                ProgressRing.IsActive = true;
                PeriodicTimer.Cancel();
                startScoreboardTimer();
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
