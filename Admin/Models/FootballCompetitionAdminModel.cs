using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballCompetitionAdminModel
    {
        public int Id { get; set; }

        public string Name => $"{this.SeasonName} - {this.TypeName}";


        public int SeasonId { get; set; }

        public string SeasonName { get; set; }

        public string CountryName { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public int TournamentId { get; set; }
        public string TournamentName { get; set; }
    }
}
