namespace Sportiada.Services.Football.Models.Side
{
    using System;
    using Models.Team;

    public class SidePlayerGameStatitisticModel
    {
        public int PlayerId { get; set; }

        public int SideId { get; set; }

        public TeamModel Team { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? UntilDate { get; set; }

        public int SeasonId { get; set; }
    }
}
