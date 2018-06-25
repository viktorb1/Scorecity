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
            await Sbvm.updateScoreBoardViewModel(date);

            scoreboard.SelectionChanged -= scoreboard_SelectionChanged;

            if (scoreboard.Items.Count > 0 && scoreboard.SelectedIndex == -1)
                scoreboard.SelectedIndex = 0;

            scoreboard.SelectionChanged -= scoreboard_SelectionChanged;

            if (scoreboard.SelectedIndex >= 0)
            {
                await Bsvm.updateBoxScoreViewModel(date, Sbvm.Sb[scoreboard.SelectedIndex].GameId);
            }
        }


        private void startScoreboardTimer()
        {
            PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer(
            async (source) =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.High, refreshScoreboard);
            }, TimeSpan.FromSeconds(5));
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
                PeriodicTimer.Cancel();
                date = calendar.Date.Value.ToString("yyyyMMdd");
                refreshScoreboard();
                startScoreboardTimer();
            }
        }

        private void scoreboard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                PeriodicTimer.Cancel();
                refreshScoreboard();
                startScoreboardTimer();
        }
	}
}
