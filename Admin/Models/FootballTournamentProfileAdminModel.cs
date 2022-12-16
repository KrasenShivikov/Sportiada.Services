namespace Sportiada.Services.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class FootballTournamentProfileAdminModel : FootballTournamentAdminModel
    {
        public IEnumerable<FootballCompetitionAdminModel> Competitions { get; set; }
    }
}
