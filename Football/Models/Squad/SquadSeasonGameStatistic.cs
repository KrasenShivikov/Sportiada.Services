namespace Sportiada.Services.Football.Models.Squad
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class SquadSeasonGameStatistic
    {
        public string Tournament { get; set; }

        public string Round { get; set; }

        public string CoachName { get; set; }

        public bool IsHost { get; set; }

        public string Opponent { get; set; }

        public int ScoredGoals { get; set; }

        public int AllowedGoals { get; set; }
    }
}
