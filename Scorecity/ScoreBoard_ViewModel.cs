using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Scorecity
{
    public class ScoreBoardViewModel
    {
        public ObservableCollection<ScoreBoard> Sb { get; set; }

        public ScoreBoardViewModel()
        {
            Sb = new ObservableCollection<ScoreBoard>();
        }

        public async Task<ObservableCollection<ScoreBoard>> updateScoreBoardViewModel(string date)
        {
            var raw = await ScoreBoardGetter.updateScoreBoard(date);

            if (raw == null) // no response object
                Sb.Clear();
            else if (raw.numGames != Sb.Count) // different set of games
            {
                Sb.Clear();

                for (int i = 0; i < raw.numGames; i++)
                {
                    Sb.Add(new ScoreBoard());
                    Sb[i].Home.Name = raw.games[i].hTeam.triCode;
                    Sb[i].Home.Score = raw.games[i].hTeam.score;
                    Sb[i].Away.Name = raw.games[i].vTeam.triCode;
                    Sb[i].Away.Score = raw.games[i].vTeam.score;

                    if (raw.games[i].statusNum == 1)
                        Sb[i].Home.Status = "GAME HAS NOT STARTED";
                    else if (raw.games[i].statusNum == 2)
                    { // GAME IS IN PROGRESS
                        if (raw.games[i].period.isHalftime)
                            Sb[i].Home.Status = "HALFTIME";
                        if (raw.games[i].period.isEndOfPeriod)
                            Sb[i].Home.Status = "END OF QUARTER " + raw.games[i].period.current;
                        else
                            Sb[i].Home.Status = "QUARTER: " + raw.games[i].period.current + "TIME LEFT: " + raw.games[i].clock;
                    }
                    else // GAME ENDED
                        Sb[i].Home.Status = "GAME HAS ENDED";
                }
            } else { // update same set of games
                for (int i = 0; i < raw.numGames; i++)
                {
                    Sb[i].Home.Name = raw.games[i].hTeam.triCode;
                    Sb[i].Home.Score = raw.games[i].hTeam.score;
                    Sb[i].Away.Name = raw.games[i].vTeam.triCode;
                    Sb[i].Away.Score = raw.games[i].vTeam.score;
                }
            }

            return Sb;
        }
    }


    public class ScoreBoard : ViewModelBase
    {
        private Team home { get; set; }
        private Team away { get; set; }
        private string bottomstatus { get; set; }
        private string gameid { get; set; }

        public Team Home {
            get { return home;  }
            set { if (value != home) { home = value; NotifyPropertyChanged(); } }
        }

        public Team Away
        {
            get { return away; }
            set { if (value != away) { away = value; NotifyPropertyChanged(); } }
        }

        public string BottomStatus {
            get { return bottomstatus; }
            set { if (value != bottomstatus) { bottomstatus = value; NotifyPropertyChanged(); } }
        }

        public string GameId
        {
            get { return gameid; }
            set { if (value != gameid) { gameid = value; NotifyPropertyChanged(); } }
        }

        public ScoreBoard()
        {
            Home = new Team();
            Away = new Team();
        }
    }


    public class Team : ViewModelBase
    {
        private string name;
        private string score { get; set; }
        public string status { get; set; }
        public string teamcolor { get; set; }

        public string Name
        {
            get { return name; }
            set { if (value != name) { name = value; NotifyPropertyChanged(); } }
        }

        public string Score
        {
            get { return score; }
            set { if (value != score) { score = value; NotifyPropertyChanged(); } }
        }

        public string Status
        {
            get { return status; }
            set { if (value != status) { status = value; NotifyPropertyChanged(); } }
        }

        public string TeamColor
        {
            get { return teamcolor; }
            set { if (value != teamcolor) { teamcolor = value; NotifyPropertyChanged(); } }
        }
    }
}