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
    public class ScoreBoardViewModel : ViewModelBase
    {
        public ObservableCollection<ScoreBoard> Sb { get; set; }
        private string oldDate;

        public ScoreBoardViewModel()
        {
            Sb = new ObservableCollection<ScoreBoard>();
        }

        public async Task<ObservableCollection<ScoreBoard>> updateScoreBoardViewModel(string date)
        {
            var raw = await ScoreBoardGetter.updateScoreBoard(date);

            if (raw == null) { // no games for that dateoldDate = date;
                oldDate = date;
                Sb.Clear();
            } else if (oldDate == null || oldDate != date) { // date changed
                oldDate = date;
                LoadView(raw);
            } else { // same date
                UpdateView(raw);
            }


            return Sb;
        }

        public void LoadView(RawData raw)
        {
            var teamInfo = new TeamInfo();
            var colors = teamInfo.colors;
            var names = teamInfo.names;

            Sb.Clear();

            for (int i = 0; i < raw.numGames; i++)
            {
                var hTriCode = raw.games[i].hTeam.triCode;
                var hScore = raw.games[i].hTeam.score;

                var aTriCode = raw.games[i].vTeam.triCode;
                var aScore = raw.games[i].vTeam.score;

                var Home = new Team(hTriCode, generateScore(hScore), colors[hTriCode]);
                var Away = new Team(aTriCode, generateScore(aScore), colors[aTriCode]);
                string status = generateStatus(raw.games[i]);
                var NewSb = new ScoreBoard(Home, Away, status, raw.games[i].gameId);
                Sb.Add(NewSb);
            }
        }

        public void UpdateView(RawData raw)
        {
            for (int i = 0; i < raw.numGames; i++)
            {
                Sb[i].Home.Score = generateScore(raw.games[i].hTeam.score);
                Sb[i].Away.Score = generateScore(raw.games[i].vTeam.score);
                Sb[i].BottomStatus = generateStatus(raw.games[i]);
            }
        }

        public string generateScore(string score) {
            if (score == "")
                return "0";
            
            return score;
        }

        public string generateStatus(Game raw)
        {
            string status;

            if (raw.statusNum == 1)
                status = "GAME HAS NOT STARTED";
            else if (raw.statusNum == 2) { // GAME IS IN PROGRESS
                if (raw.period.isHalftime)
                    status = "HALFTIME";
                if (raw.period.isEndOfPeriod)
                    status = "END OF QUARTER " + raw.period.current;
                else
                    status = "QUARTER: " + raw.period.current + "TIME LEFT: " + raw.clock;
            }
            else // GAME ENDED
                status = "THIS GAME HAS ENDED";

            return status;
        }
    }


    public class ScoreBoard : ViewModelBase
    {
        private Team home;
        private Team away;
        private string bottomstatus;
        private string gameid;

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

        public ScoreBoard(Team Home, Team Away, string BottomStatus, string GameId) 
        {
            this.Home = Home;
            this.Away = Away;
            this.BottomStatus = BottomStatus;
            this.GameId = GameId;
        }
    }


    public class Team : ViewModelBase
    {
        private string triCode;
        private string score;
        private string teamcolor;

        public Team(string TriCode, string Score, string TeamColor)
        {
            this.TriCode = TriCode;
            this.Score = Score;
            this.TeamColor = TeamColor;
        }

        public string TriCode
        {
            get { return triCode; }
            set { if (value != triCode) { triCode = value; NotifyPropertyChanged(); } }
        }

        public string Score
        {
            get { return score; }
            set { if (value != score) { score = value; NotifyPropertyChanged(); } }
        }

        public string TeamColor
        {
            get { return teamcolor; }
            set { if (value != teamcolor) { teamcolor = value; NotifyPropertyChanged(); } }
        }

    }
}