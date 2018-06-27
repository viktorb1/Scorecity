using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecity
{
    public class BoxScoreViewModel
    {
        public ObservableCollection<PlayerStats> HomeBoxscore { get; set; }
        public ObservableCollection<PlayerStats> AwayBoxscore { get; set; }
        private string oldGameId;
        private string oldDate;


        public BoxScoreViewModel()
        {
            HomeBoxscore = new ObservableCollection<PlayerStats>();
            AwayBoxscore = new ObservableCollection<PlayerStats>();
        }

        public async Task<List<ActivePlayer>> updateBoxScoreViewModel(string date, string gameId)
        {
            var rawbs = (await BoxScore.loadGameDetailsAsync(date, gameId));
            List<ActivePlayer> raw;

            if (rawbs != null && rawbs.stats != null && rawbs.stats.activePlayers != null && rawbs.stats.activePlayers.Count > 0)
                raw = rawbs.stats.activePlayers;
            else
                raw = null;


            if (raw == null)
            { // no boxscore for that dateoldDate = date;
                oldDate = date;
                oldGameId = gameId;
                HomeBoxscore.Clear();
                AwayBoxscore.Clear();
            }
            else if (oldGameId != gameId ||  oldDate != date)
            { // date changed
                oldDate = date;
                oldGameId = gameId;
                await LoadView(raw);
            }
            else
            { // same date
                UpdateView(raw);
            }

            return raw;
        }

        public async Task<ObservableCollection<PlayerStats>> LoadView(List<ActivePlayer> raw_players)
        {
            HomeBoxscore.Clear();
            AwayBoxscore.Clear();

            for (int i = 0; i < raw_players.Count; i++)
            {
                var player = new PlayerStats();

                player.Playername = await BoxScore.GetPlayerName(raw_players[i].personId);

                UpdateVM(player, raw_players[i]);

                if (raw_players[i].teamId == raw_players[0].teamId)
                    AwayBoxscore.Add(player);
                else
                    HomeBoxscore.Add(player);
            }

            return HomeBoxscore;
        }

        public void UpdateView(List<ActivePlayer> raw_players)
        {
            int j = 0, k = 0;

            for (int i = 0; i < raw_players.Count; i++)
            {
                if (raw_players[i].teamId == raw_players[0].teamId)
                    UpdateVM(AwayBoxscore[j++], raw_players[i]);
                else
                    UpdateVM(HomeBoxscore[k++], raw_players[i]);
            }
        }

        private void UpdateVM(PlayerStats player, ActivePlayer raw_player)
        {
            player.Min = raw_player.min;
            player.Pts = raw_player.points;
            player.Ast = raw_player.assists;
            player.Reb = raw_player.totReb;
            player.Fg = raw_player.fgm + "/" + raw_player.fga;
            player.Threept = raw_player.tpm + "/" + raw_player.tpa;
            player.Ft = raw_player.ftm + "/" + raw_player.fta;
            player.Stl = raw_player.steals;
            player.Blk = raw_player.blocks;
            player.Tov = raw_player.turnovers;
            player.Pf = raw_player.pFouls;
        }
    }

    public class PlayerStats : ViewModelBase
    {
        private string playername;
        private string min;
        private string pts;
        private string ast;
        private string reb;
        private string fg;
        private string threept;
        private string ft;
        private string stl;
        private string blk;
        private string tov;
        private string pf;

        public string Playername
        {
            get { return playername; }
            set { if (value != playername) { playername = value; NotifyPropertyChanged(); } }
        }

        public string Min
        {
            get { return min; }
            set { if (value != min) { min = value; NotifyPropertyChanged(); } }
        }

        public string Pts
        {
            get { return pts; }
            set { if (value != pts) { pts = value; NotifyPropertyChanged(); } }
        }

        public string Ast
        {
            get { return ast; }
            set { if (value != ast) { ast = value; NotifyPropertyChanged(); } }
        }

        public string Reb
        {
            get { return reb; }
            set { if (value != reb) { reb = value; NotifyPropertyChanged(); } }
        }

        public string Fg
        {
            get { return fg; }
            set { if (value != fg) { fg = value; NotifyPropertyChanged(); } }
        }

        public string Threept
        {
            get { return threept; }
            set { if (value != threept) { threept = value; NotifyPropertyChanged(); } }
        }

        public string Ft
        {
            get { return ft; }
            set { if (value != ft) { ft = value; NotifyPropertyChanged(); } }
        }

        public string Stl
        {
            get { return stl; }
            set { if (value != stl) { stl = value; NotifyPropertyChanged(); } }
        }

        public string Blk
        {
            get { return blk; }
            set { if (value != blk) { blk = value; NotifyPropertyChanged(); } }
        }

        public string Tov
        {
            get { return tov; }
            set { if (value != tov) { tov = value; NotifyPropertyChanged(); } }
        }

        public string Pf
        {
            get { return pf; }
            set { if (value != pf) { pf = value; NotifyPropertyChanged(); } }
        }
    }
}