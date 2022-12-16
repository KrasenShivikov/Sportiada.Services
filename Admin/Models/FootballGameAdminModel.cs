namespace Sportiada.Services.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class FootballGameAdminModel : FootballGameListAdminModel
    {
        public FootballRoundAdminModel Round { get; set; }

        public IEnumerable<FootballGameStatisticAdminModel> GameStatistics { get; set; }

        public IEnumerable<FootballGameRefereeAdminModel> Referees { get; set; }
    }
}
