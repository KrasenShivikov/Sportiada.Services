namespace Sportiada.Services.Football.Models.Squad
{
    using System;
    using Models.Team;

    public class SquadPlayerGameStatitisticModel
    {
        public int PlayerId { get; set; }

        public int SquadId { get; set; }

        public TeamModel Team { get; set; }

        public int SeasonId { get; set; }
    }
}
