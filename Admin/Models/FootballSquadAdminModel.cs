namespace Sportiada.Services.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class FootballSquadAdminModel
    {
        public int Id { get; set; }

        public int SeasonId { get; set; }
        public SeasonAdminModel Season { get; set; }

        public int TeamId { get; set; }

        public FootballTeamAdminModel Team { get; set; }

        public IEnumerable<FootballSquadPlayerAdminModel> Players { get; set; }

        public IEnumerable<FootballSquadCoachAdminModel> Coaches { get; set; }
    }
}
