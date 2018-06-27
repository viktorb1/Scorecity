using System.Collections.Generic;

namespace Scorecity
{
    public class TeamInfo
    {
        public Dictionary<string, string> colors { get; set; }
        public Dictionary<string, string> names { get; set; }


        public TeamInfo()
        {
            colors = new Dictionary<string, string>()
            {
                {"ATL", "#e21a37"},
                {"BKN", "#000000"},
                {"BNE", "#000000"},
                {"BOS", "#00611b"},
                {"CHA", "#00848e"},
                {"CHI", "#b00203"},
                {"CLE", "#860038"},
                {"DAL", "#006bb6"},
                {"DEN", "#feb927"},
                {"DET", "#fa002c"},
                {"GSW", "#003399"},
                {"HOU", "#cd212b"},
                {"IND", "#ffb517"},
                {"LAC", "#ed174b"},
                {"LAL", "#fdba33"},
                {"MEM", "#5d76a9"},
                {"MIA", "#98002e"},
                {"MIL", "#00471b"},
                {"MIN", "#2b6291"},
                {"NOP", "#0c2340"},
                {"NYK", "#f58426"},
                {"OKC", "#002d62"},
                {"ORL", "#0077c0"},
                {"PHI", "#ef0022"},
                {"PHX", "#e76221"},
                {"POR", "#cc0000"},
                {"SAC", "#51388a"},
                {"SAS", "#959191"},
                {"TOR", "#bd1b21"},
                {"UTA", "#f9a11e"},
                {"WAS", "#cf142b"}
            };

            names = new Dictionary<string, string>()
            {
                {"ATL", "Atlanta Hawks"},
                {"BKN", "Brooklyn Nets"},
                {"BNE", "Brooklyn Nets"},
                {"BOS", "Boston Celtics"},
                {"CHA", "Charlotte Hornets"},
                {"CHI", "Chicago Bulls"},
                {"CLE", "Cleveland Cavaliers"},
                {"DAL", "Dallas Mavericks"},
                {"DEN", "Denver Nuggets"},
                {"DET", "Detroit Pistons"},
                {"GSW", "Golden State Warriors"},
                {"HOU", "Houston Rockets"},
                {"IND", "Indiana Pacers"},
                {"LAC", "LA Clippers"},
                {"LAL", "Los Angeles Lakers"},
                {"MEM", "Memphis Grizzlies"},
                {"MIA", "Miami Heat"},
                {"MIL", "Milwaukee Bucks"},
                {"MIN", "Minnesota Timberwolves"},
                {"NOP", "New Orleans Pelicans"},
                {"NYK", "New York Knicks"},
                {"OKC", "Oklahoma City Thunder"},
                {"ORL", "Orlando Magic"},
                {"PHI", "Philadelphia 76ers"},
                {"PHX", "Phoenix Suns"},
                {"POR", "Portland Trail Blazers"},
                {"SAC", "Sacramento Kings"},
                {"SAS", "San Antonio Spurs"},
                {"TOR", "Toronto Raptors"},
                {"UTA", "Utah Jazz" },
                {"WAS", "Washington Wizards"}
            };
        }
    }
}