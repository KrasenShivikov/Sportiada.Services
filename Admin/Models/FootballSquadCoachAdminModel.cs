using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballSquadCoachAdminModel
    {
        public int SquadId { get; set; }
        public int CoachId { get; set; }

        public FootballCoachAdminModel Coach { get; set; }

        public string Position { get; set; }

        public string SquadType { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public bool LeftInSeason { get; set; }
    }
}
