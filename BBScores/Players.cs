using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace BBScores
{
    public class Players //: INotifyPropertyChanged
    {
        public LeaguePlayers lp { get; set; }

        public async Task<LeaguePlayers> loadPlayers()
        {
            var http = new HttpClient();
            var url = "http://data.nba.net/data/10s/prod/v1/" + DateTime.Now.ToString("yyyy") + "/players.json";
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(LeaguePlayers));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            lp = (LeaguePlayers)serializer.ReadObject(ms);
            return lp;
        }

        public class Team
        {
            public string teamId { get; set; }
            public string seasonStart { get; set; }
            public string seasonEnd { get; set; }
        }

        public class Draft
        {
            public string teamId { get; set; }
            public string pickNum { get; set; }
            public string roundNum { get; set; }
            public string seasonYear { get; set; }
        }

        public class Standard
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string personId { get; set; }
            public string teamId { get; set; }
            public string jersey { get; set; }
            public string pos { get; set; }
            public string heightFeet { get; set; }
            public string heightInches { get; set; }
            public string heightMeters { get; set; }
            public string weightPounds { get; set; }
            public string weightKilograms { get; set; }
            public string dateOfBirthUTC { get; set; }
            public List<Team> teams { get; set; }
            public Draft draft { get; set; }
            public string nbaDebutYear { get; set; }
            public string yearsPro { get; set; }
            public string collegeName { get; set; }
            public string lastAffiliation { get; set; }
            public string country { get; set; }
        }

        public class Team2
        {
            public string teamId { get; set; }
            public string seasonStart { get; set; }
            public string seasonEnd { get; set; }
        }

        public class Draft2
        {
            public string teamId { get; set; }
            public string pickNum { get; set; }
            public string roundNum { get; set; }
            public string seasonYear { get; set; }
        }

        public class Orlando
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string personId { get; set; }
            public string teamId { get; set; }
            public string jersey { get; set; }
            public string pos { get; set; }
            public string heightFeet { get; set; }
            public string heightInches { get; set; }
            public string heightMeters { get; set; }
            public string weightPounds { get; set; }
            public string weightKilograms { get; set; }
            public string dateOfBirthUTC { get; set; }
            public List<Team2> teams { get; set; }
            public Draft2 draft { get; set; }
            public string nbaDebutYear { get; set; }
            public string yearsPro { get; set; }
            public string collegeName { get; set; }
            public string lastAffiliation { get; set; }
            public string country { get; set; }
        }

        public class Team3
        {
            public string teamId { get; set; }
            public string seasonStart { get; set; }
            public string seasonEnd { get; set; }
        }

        public class Draft3
        {
            public string teamId { get; set; }
            public string pickNum { get; set; }
            public string roundNum { get; set; }
            public string seasonYear { get; set; }
        }

        public class Vega
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string personId { get; set; }
            public string teamId { get; set; }
            public string jersey { get; set; }
            public string pos { get; set; }
            public string heightFeet { get; set; }
            public string heightInches { get; set; }
            public string heightMeters { get; set; }
            public string weightPounds { get; set; }
            public string weightKilograms { get; set; }
            public string dateOfBirthUTC { get; set; }
            public List<Team3> teams { get; set; }
            public Draft3 draft { get; set; }
            public string nbaDebutYear { get; set; }
            public string yearsPro { get; set; }
            public string collegeName { get; set; }
            public string lastAffiliation { get; set; }
            public string country { get; set; }
        }

        public class Draft4
        {
            public string teamId { get; set; }
            public string pickNum { get; set; }
            public string roundNum { get; set; }
            public string seasonYear { get; set; }
        }

        public class Utah
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string personId { get; set; }
            public string teamId { get; set; }
            public string jersey { get; set; }
            public string pos { get; set; }
            public string heightFeet { get; set; }
            public string heightInches { get; set; }
            public string heightMeters { get; set; }
            public string weightPounds { get; set; }
            public string weightKilograms { get; set; }
            public string dateOfBirthUTC { get; set; }
            public List<object> teams { get; set; }
            public Draft4 draft { get; set; }
            public string nbaDebutYear { get; set; }
            public string yearsPro { get; set; }
            public string collegeName { get; set; }
            public string lastAffiliation { get; set; }
            public string country { get; set; }
        }

        public class LeaguePlayers
        {
            public List<Standard> standard { get; set; }
            public List<Orlando> orlando { get; set; }
            public List<Vega> vegas { get; set; }
            public List<Utah> utah { get; set; }

            public string GetNameById(string personId)
            {
            for (int i = 0;  i < standard.Count; i++ )
            {
                if (standard[i].personId == personId)
                {
                    return standard[i].firstName;
                }
            }

            return "";
        }
        }


    }
}
