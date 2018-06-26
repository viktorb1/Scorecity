using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public bool loading = true;
        static SemaphoreSlim sem = new SemaphoreSlim(1, 1);

        public MainPage()
        {
            this.InitializeComponent();
            date = DateTime.Now.ToString("yyyyMMdd");
            date = "20180307";
            Sbvm = new ScoreBoardViewModel();
            Bsvm = new BoxScoreViewModel();
            refreshScoreboard();
            startScoreboardTimer();
        }


        private async void refreshScoreboard()
        {
            await sem.WaitAsync();
            
            try
            {
                await Sbvm.updateScoreBoardViewModel(date);

                if (scoreboard.Items.Count > 0 && scoreboard.SelectedIndex == -1)
                    scoreboard.SelectedIndex = 0;

                if (scoreboard.Items.Count > 0)
                {
                    Debug.WriteLine("THIS SHOULDNT RUNNL OLOLOLOL");
                    await Bsvm.updateBoxScoreViewModel(date, Sbvm.Sb[scoreboard.SelectedIndex].GameId);
                }
                else
                {
                    Bsvm.HomeBoxscore.Clear();
                    Bsvm.AwayBoxscore.Clear();
                }

            } finally
            {
                if (homeListView.Items.Count == 0)
                {
                    Debug.WriteLine("THSI SHOULD RUN");
                    boxScore.Visibility = Visibility.Collapsed;

                    if (scoreboard.Items.Count > 0)
                        nomoregames.Text = "The game has not yet started.";
                    else
                        nomoregames.Text = "There are no games for this date.";

                    nomoregames.Visibility = Visibility.Visible;
                } else {
                    nomoregames.Visibility = Visibility.Collapsed;
                    boxScore.Visibility = Visibility.Visible;
                }

                sem.Release();
                ProgressRing.IsActive = false;
                loading = false;
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
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, refreshScoreboard);
            }
        }

        private async void scoreboard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, refreshScoreboard);
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            homeButton.Background = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
            awayButton.Background = new SolidColorBrush(Color.FromArgb(255, 34, 34, 34));
            bothButton.Background = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
            homeListView.SelectedIndex = -1;
            awayListView.SelectedIndex = -1;
            homeListView.Visibility = Visibility.Collapsed;
            awayListView.Visibility = Visibility.Visible;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
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
