using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Scorecity
{
    public class ScoreBoardGetter : ViewModelBase
    {
        private ScoreBoard _data { get; set; }

        public ScoreBoard data
        {
            get { return _data; }
            set { _data = value; this.OnPropertyChanged(); }
        }

        public string result { get; set; }

        public async Task<ScoreBoard> updateScoreBoard(string day)
        {
            var http = new HttpClient();
            var url = "http://data.nba.net/data/10s/prod/v1/" + day + "/scoreboard.json";
            var response = await http.GetAsync(url);
            var resulttemp = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(result) || resulttemp != result)
            {
                result = resulttemp;
                var serializer = new DataContractJsonSerializer(typeof(ScoreBoard));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(resulttemp));
                data = (ScoreBoard)serializer.ReadObject(ms);

                for (int i = 0; i < data.numGames; i++)
                {
                    if (data.games[i].statusNum == 1)
                    {
                        data.games[i].hTeam.score = "0";
                        data.games[i].vTeam.score = "0";
                    }

                    if (string.IsNullOrEmpty(data.games[i].clock) || data.games[i].clock == "0.0")
                    {
                        data.games[i].clock = "END";
                    }
                }
            }

            return data;
        }
    }

    [DataContract]
    public class ScoreBoard
    {
        [DataMember]
        public int numGames { get; set; }
        [DataMember]
        public ObservableCollection<Game> games { get; set; }
    }

    [DataContract]
    public class Game
    {
        [DataMember]
        public int seasonStageId { get; set; }
        [DataMember]
        public string seasonYear { get; set; }
        [DataMember]
        public string gameId { get; set; }
        [DataMember]
        public bool isGameActivated { get; set; }
        [DataMember]
        public int statusNum { get; set; }
        [DataMember]
        public int extendedStatusNum { get; set; }
        [DataMember]
        public string StartTimeUTC { get; set; }
        [DataMember]
        public string startDateEastern { get; set; }
        [DataMember]
        public string clock { get; set; }
        [DataMember]
        public bool isBuzzerBeater { get; set; }
        [DataMember]
        public bool isPreviewArticleAvail { get; set; }
        [DataMember]
        public bool isRecapArticleAvail { get; set; }
        [DataMember]
        public bool hasGameBookPdf { get; set; }
        [DataMember]
        public bool isStartTimeTBD { get; set; }
        [DataMember]
        public Nugget nugget { get; set; }
        [DataMember]
        public string attendance { get; set; }
        [DataMember]
        public Gameduration gameDuration { get; set; }
        [DataMember]
        public string[] tags { get; set; }
        [DataMember]
        public Period period { get; set; }
        [DataMember]
        public Vteam vTeam { get; set; }
        [DataMember]
        public Hteam hTeam { get; set; }
    }

    [DataContract]
    public class Nugget
    {
        [DataMember]
        public string text { get; set; }
    }

    [DataContract]
    public class Gameduration
    {
        [DataMember]
        public string hours { get; set; }
        [DataMember]
        public string minutes { get; set; }
    }

    [DataContract]
    public class Period
    {
        [DataMember]
        public int current { get; set; }
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public int maxRegular { get; set; }
        [DataMember]
        public bool isHalftime { get; set; }
        [DataMember]
        public bool isEndOfPeriod { get; set; }
    }

    [DataContract]
    public class Vteam
    {
        [DataMember]
        public string teamId { get; set; }
        [DataMember]
        public string triCode { get; set; }
        [DataMember]
        public string win { get; set; }
        [DataMember]
        public string loss { get; set; }
        [DataMember]
        public string seriesWin { get; set; }
        [DataMember]
        public string seriesLoss { get; set; }
        [DataMember]
        public string score { get; set; }
        [DataMember]
        public Linescore[] linescore { get; set; }

        public string color
        {
            get { return Teams.teams[triCode]; }
        }
    }

    [DataContract]
    public class Linescore
    {
        [DataMember]
        public string score { get; set; }
    }

    [DataContract]
    public class Hteam
    {
        [DataMember]
        public string teamId { get; set; }
        [DataMember]
        public string triCode { get; set; }
        [DataMember]
        public string win { get; set; }
        [DataMember]
        public string loss { get; set; }
        [DataMember]
        public string seriesWin { get; set; }
        [DataMember]
        public string seriesLoss { get; set; }
        [DataMember]
        public string score { get; set; }
        [DataMember]
        public Linescore[] linescore { get; set; }

        public string color
        {
            get { return Teams.teams[triCode]; }
        }
    }
}
