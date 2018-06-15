using System;
using System.Collections.Generic;
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
    public class GameDetails : ViewModelBase
    {
        private BoxScore _gameDetails;
        public BoxScore gameDetails
        {
            get { return _gameDetails; }
            set { _gameDetails = value; this.OnPropertyChanged(); }
        }

        public async Task<BoxScore> loadGameDetailsAsync(string day, string gameID)
        {
            var http = new HttpClient();
            //http://data.nba.net/data/10s/prod/v1/20170707/1521700001_boxscore.json
            var url = "http://data.nba.net/data/10s/prod/v1/" + day + "/" + gameID + "_boxscore.json";
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(BoxScore));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            gameDetails = (BoxScore)serializer.ReadObject(ms);
            return gameDetails;
        }
    }

    [DataContract]
    public class Totals
    {
        [DataMember]
        public string points { get; set; }
        [DataMember]
        public string fgm { get; set; }
        [DataMember]
        public string fga { get; set; }
        [DataMember]
        public string fgp { get; set; }
        [DataMember]
        public string ftm { get; set; }
        [DataMember]
        public string fta { get; set; }
        [DataMember]
        public string ftp { get; set; }
        [DataMember]
        public string tpm { get; set; }
        [DataMember]
        public string tpa { get; set; }
        [DataMember]
        public string tpp { get; set; }
        [DataMember]
        public string offReb { get; set; }
        [DataMember]
        public string defReb { get; set; }
        [DataMember]
        public string totReb { get; set; }
        [DataMember]
        public string assists { get; set; }
        [DataMember]
        public string pFouls { get; set; }
        [DataMember]
        public string steals { get; set; }
        [DataMember]
        public string turnovers { get; set; }
        [DataMember]
        public string blocks { get; set; }
        [DataMember]
        public string plusMinus { get; set; }
        [DataMember]
        public string min { get; set; }
    }

    [DataContract]
    public class Player
    {
        [DataMember]
        public string personId { get; set; }
    }

    [DataContract]
    public class Points
    {
        [DataMember]
        public string value { get; set; }
        [DataMember]
        public List<Player> players { get; set; }
    }

    [DataContract]
    public class Player2
    {
        [DataMember]
        public string personId { get; set; }
    }

    [DataContract]
    public class Rebounds
    {
        [DataMember]
        public string value { get; set; }
        [DataMember]
        public List<Player2> players { get; set; }
    }

    [DataContract]
    public class Player3
    {
        [DataMember]
        public string personId { get; set; }
    }

    [DataContract]
    public class Assists
    {
        [DataMember]
        public string value { get; set; }
        [DataMember]
        public List<Player3> players { get; set; }
    }

    [DataContract]
    public class Leaders
    {
        [DataMember]
        public Points points { get; set; }
        [DataMember]
        public Rebounds rebounds { get; set; }
        [DataMember]
        public Assists assists { get; set; }
    }

    [DataContract]
    public class VTeam3
    {
        [DataMember]
        public string fastBreakPoints { get; set; }
        [DataMember]
        public string pointsInPaint { get; set; }
        [DataMember]
        public string biggestLead { get; set; }
        [DataMember]
        public string secondChancePoints { get; set; }
        [DataMember]
        public string pointsOffTurnovers { get; set; }
        [DataMember]
        public string longestRun { get; set; }
        [DataMember]
        public Totals totals { get; set; }
        [DataMember]
        public Leaders leaders { get; set; }
    }

    [DataContract]
    public class Totals2
    {
        [DataMember]
        public string points { get; set; }
        [DataMember]
        public string fgm { get; set; }
        [DataMember]
        public string fga { get; set; }
        [DataMember]
        public string fgp { get; set; }
        [DataMember]
        public string ftm { get; set; }
        [DataMember]
        public string fta { get; set; }
        [DataMember]
        public string ftp { get; set; }
        [DataMember]
        public string tpm { get; set; }
        [DataMember]
        public string tpa { get; set; }
        [DataMember]
        public string tpp { get; set; }
        [DataMember]
        public string offReb { get; set; }
        [DataMember]
        public string defReb { get; set; }
        [DataMember]
        public string totReb { get; set; }
        [DataMember]
        public string assists { get; set; }
        [DataMember]
        public string pFouls { get; set; }
        [DataMember]
        public string steals { get; set; }
        [DataMember]
        public string turnovers { get; set; }
        [DataMember]
        public string blocks { get; set; }
        [DataMember]
        public string plusMinus { get; set; }
        [DataMember]
        public string min { get; set; }
    }

    [DataContract]
    public class Player4
    {
        [DataMember]
        public string personId { get; set; }
    }

    [DataContract]
    public class Points2
    {
        [DataMember]
        public string value { get; set; }
        [DataMember]
        public List<Player4> players { get; set; }
    }

    [DataContract]
    public class Player5
    {
        [DataMember]
        public string personId { get; set; }
    }

    [DataContract]
    public class Rebounds2
    {
        [DataMember]
        public string value { get; set; }
        [DataMember]
        public List<Player5> players { get; set; }
    }

    [DataContract]
    public class Player6
    {
        [DataMember]
        public string personId { get; set; }
    }

    [DataContract]
    public class Assists2
    {
        [DataMember]
        public string value { get; set; }
        [DataMember]
        public List<Player6> players { get; set; }
    }

    [DataContract]
    public class Leaders2
    {
        [DataMember]
        public Points2 points { get; set; }
        [DataMember]
        public Rebounds2 rebounds { get; set; }
        [DataMember]
        public Assists2 assists { get; set; }
    }

    [DataContract]
    public class HTeam3
    {
        [DataMember]
        public string fastBreakPoints { get; set; }
        [DataMember]
        public string pointsInPaint { get; set; }
        [DataMember]
        public string biggestLead { get; set; }
        [DataMember]
        public string secondChancePoints { get; set; }
        [DataMember]
        public string pointsOffTurnovers { get; set; }
        [DataMember]
        public string longestRun { get; set; }
        [DataMember]
        public Totals2 totals { get; set; }
        [DataMember]
        public Leaders2 leaders { get; set; }
    }

    [DataContract]
    public class SortKey
    {
        [DataMember]
        public int name { get; set; }
        [DataMember]
        public int pos { get; set; }
        [DataMember]
        public int points { get; set; }
        [DataMember]
        public int min { get; set; }
        [DataMember]
        public int fgm { get; set; }
        [DataMember]
        public int fga { get; set; }
        [DataMember]
        public int fgp { get; set; }
        [DataMember]
        public int ftm { get; set; }
        [DataMember]
        public int fta { get; set; }
        [DataMember]
        public int ftp { get; set; }
        [DataMember]
        public int tpm { get; set; }
        [DataMember]
        public int tpa { get; set; }
        [DataMember]
        public int tpp { get; set; }
        [DataMember]
        public int offReb { get; set; }
        [DataMember]
        public int defReb { get; set; }
        [DataMember]
        public int totReb { get; set; }
        [DataMember]
        public int assists { get; set; }
        [DataMember]
        public int pFouls { get; set; }
        [DataMember]
        public int steals { get; set; }
        [DataMember]
        public int turnovers { get; set; }
        [DataMember]
        public int blocks { get; set; }
        [DataMember]
        public int plusMinus { get; set; }
    }

    [DataContract]
    public class ActivePlayer
    {
        [DataMember]
        public string personId { get; set; }
        [DataMember]
        public string teamId { get; set; }
        [DataMember]
        public bool isOnCourt { get; set; }
        [DataMember]
        public string points { get; set; }
        [DataMember]
        public string pos { get; set; }
        [DataMember]
        public string min { get; set; }
        [DataMember]
        public string fgm { get; set; }
        [DataMember]
        public string fga { get; set; }
        [DataMember]
        public string fgp { get; set; }
        [DataMember]
        public string ftm { get; set; }
        [DataMember]
        public string fta { get; set; }
        [DataMember]
        public string ftp { get; set; }
        [DataMember]
        public string tpm { get; set; }
        [DataMember]
        public string tpa { get; set; }
        [DataMember]
        public string tpp { get; set; }
        [DataMember]
        public string offReb { get; set; }
        [DataMember]
        public string defReb { get; set; }
        [DataMember]
        public string totReb { get; set; }
        [DataMember]
        public string assists { get; set; }
        [DataMember]
        public string pFouls { get; set; }
        [DataMember]
        public string steals { get; set; }
        [DataMember]
        public string turnovers { get; set; }
        [DataMember]
        public string blocks { get; set; }
        [DataMember]
        public string plusMinus { get; set; }
        [DataMember]
        public string dnp { get; set; }
        [DataMember]
        public SortKey sortKey { get; set; }
    }

    [DataContract]
    public class Stats
    {
        [DataMember]
        public string timesTied { get; set; }
        [DataMember]
        public string leadChanges { get; set; }
        [DataMember]
        public VTeam3 vTeam { get; set; }
        [DataMember]
        public HTeam3 hTeam { get; set; }
        [DataMember]
        public List<ActivePlayer> activePlayers { get; set; }
    }

    [DataContract]
    public class BoxScore
    {
        [DataMember]
        public Stats stats { get; set; }
    }
}
