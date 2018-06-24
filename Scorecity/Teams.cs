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
                {"ATL", "#E2363D"},
                {"BKN", "#071922"},
                {"BOS", "#06864C"},
                {"CHA", "#1D8CAA"},
                {"CHI", "#CC1244"},
                {"CLE", "#B71F38"},
                {"DAL", "#006BB7"},
                {"DEN", "#4B90CD"},
                {"DET", "#ED164B"},
                {"GSW", "#0446AC"},
                {"HOU", "#D31145"},
                {"IND", "#00265D"},
                {"LAC", "#006BB7"},
                {"LAL", "#FDC82F"},
                {"MEM", "#23375B"},
                {"MIA", "#98012E"},
                {"MIL", "#003614"},
                {"MIN", "#231F20"},
                {"NOP", "#B5985A"},
                {"NYK", "#F4822C"},
                {"OKC", "#F05033"},
                {"ORL", "#0476BC"},
                {"PHI", "#0046AD"},
                {"PHX", "#3E2680"},
                {"POR", "#E2383F"},
                {"SAC", "#393996"},
                {"SAS", "#202020"},
                {"TOR", "#CD1041"},
                {"UTA", "#042244"},
                {"WAS", "#042A5C"}
            };
        }
    }
}