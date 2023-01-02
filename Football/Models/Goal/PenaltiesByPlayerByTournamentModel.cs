using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Football.Models.Goal
{
    public class PenaltiesByPlayerByTournamentModel
    {
        public string TournamentName { get; set; }

        public string Round { get; set; }

        public string Icon { get; set; }

        public string PlayerName { get; set; }

        public string Type { get; set; }
    }
}
