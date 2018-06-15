using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public Teams teams { get; set; }
        public ScoreBoardGetter sb { get; set; }
        public GameDetails gd { get; set; }
        public string date { get; set; }
        ThreadPoolTimer PeriodicTimer;
        int userSelectedGame = 0;

        public MainPage()
        {
            this.InitializeComponent();
            teams = new Teams();
            sb = new ScoreBoardGetter();
            gd = new GameDetails();
            date = DateTime.Now.ToString("yyyyMMdd");
            startScoreboardTimer();
            refreshScoreboard();
        }

        private async void refreshScoreboard()
        {
            await sb.updateScoreBoard(date);

            if (scoresbox.Items.Count > 0)
            {
                if (scoresbox.SelectedIndex == -1)
                    scoresbox.SelectedIndex = 0;

                userSelectedGame = scoresbox.SelectedIndex;
                await gd.loadGameDetailsAsync(date, sb.data.games[userSelectedGame].gameId);
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

            if (scoresbox.Visibility == Visibility.Visible)
            {
                scoresbox.Visibility = Visibility.Collapsed;
                button.Content = "\xE72A";
            }
            else
            {
                scoresbox.Visibility = Visibility.Visible;
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

        private void scoresbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PeriodicTimer.Cancel();
            refreshScoreboard();
            startScoreboardTimer();
        }
	}
}
