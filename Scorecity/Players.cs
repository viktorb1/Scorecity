using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PlayerDatabase
{
    public class Players
    {
        public static async Task<LeaguePlayers> loadPlayers()
        {
            var http = new HttpClient();
            //var url = "http://data.nba.net/data/10s/prod/v1/" + DateTime.Now.ToString("yyyy") + "/players.json";
            var url = "http://data.nba.net/data/10s/prod/v1/2020/players.json";
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(LeaguePlayers));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            return (LeaguePlayers)serializer.ReadObject(ms);
        }

        public static async Task<List<Class1>> loadDLeaguePlayers()
        {
            var http = new HttpClient();
            var url = "http://gleague.nba.com/wp-json/api/v1/players.json";
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Class1>>(result);
        }
    }


    public class LeaguePlayers
    {
        public League league { get; set; }
    }

    public class League
    {
        public Standard[] standard { get; set; }
    }

    public class Standard
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string personId { get; set; }
        public string teamId { get; set; }
        public string jersey { get; set; }
        public bool isActive { get; set; }
        public string pos { get; set; }
        public string heightFeet { get; set; }
        public string heightInches { get; set; }
        public string heightMeters { get; set; }
        public string weightPounds { get; set; }
        public string weightKilograms { get; set; }
        public string dateOfBirthUTC { get; set; }
        public Team_[] teams { get; set; }
        public Draft draft { get; set; }
        public string nbaDebutYear { get; set; }
        public string yearsPro { get; set; }
        public string collegeName { get; set; }
        public string lastAffiliation { get; set; }
        public string country { get; set; }
    }

    public class Draft
    {
        public string teamId { get; set; }
        public string pickNum { get; set; }
        public string roundNum { get; set; }
        public string seasonYear { get; set; }
    }

    public class Team_
    {
        public string teamId { get; set; }
        public string seasonStart { get; set; }
        public string seasonEnd { get; set; }
    }



    public class Rootobject
    {
        public List<Class1> dlp { get; set; }
    }

    public class Class1
    {
        public _Data _data { get; set; }
    }

    public class _Data
    {
        public int tid { get; set; }
        public string fn { get; set; }
        public string ln { get; set; }
        public int pid { get; set; }
        public string num { get; set; }
        public string pos { get; set; }
        public string dob { get; set; }
        public string ht { get; set; }
        public int y { get; set; }
        public string sn { get; set; }
        public string ty { get; set; }
        public string co { get; set; }
        public string la { get; set; }
        public string dy { get; set; }
        public string pc { get; set; }
        public string fa { get; set; }
        public string s { get; set; }
        public string ta { get; set; }
        public string tn { get; set; }
        public string tc { get; set; }
        public string permalink { get; set; }
    }

}











