using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    public class ScoreBoardGetter
    {
        static public async Task<RawData> updateScoreBoard(string day)
        {
            var http = new HttpClient();
            var url = "http://data.nba.net/data/10s/prod/v1/" + day + "/scoreboard.json";
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(RawData));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                return (RawData)serializer.ReadObject(ms);
            } catch (SerializationException e)
            {
                return null;
            }

        
        }
    }

    [DataContract]
    public class RawData
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

        //public string color
        //{
        //    get { return Teams.teams[triCode]; }
        //}
    }
}
