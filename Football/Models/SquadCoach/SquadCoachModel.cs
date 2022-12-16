namespace Sportiada.Services.Football.Models.SquadCoach
{
    using Sportiada.Services.Football.Models.Coach;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class SquadCoachModel
    {
        public int SquadId { get; set; }
        public int CoachId { get; set; }

        public CoachModel Coach { get; set; }

        public string Position { get; set; }

        public string SquadType { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public bool LeftInSeason { get; set; }
    }
}
