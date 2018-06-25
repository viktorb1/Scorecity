using System.Collections.Generic;

namespace Scorecity
{
    public class TeamColors
    {
        public Dictionary<string, string> teams { get; set; }

        public TeamColors()
        {
            teams = new Dictionary<string, string>()
            {
                {"ATL", "#e21a37"},
                {"BKN", "#000000"},
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
        }
    }
}