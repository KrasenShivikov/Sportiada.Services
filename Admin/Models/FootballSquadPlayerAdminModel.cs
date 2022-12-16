namespace Sportiada.Services.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class FootballSquadPlayerAdminModel
    {
        public int PlayerId { get; set; }

        public FootballPlayerAdminModel Player { get; set; }

        public int SquadId { get; set; }

        public int Number { get; set; }

        public string Picture { get; set; }

        public string Country { get; set; }

        public string Position { get; set; }

        public string SquadType { get; set; }

        public string ContractStartDate { get; set; }

        public string ContractEndDate { get; set; }

        public bool OnLoan { get; set; }

        public bool JoinedInSummer { get; set; }

        public bool JoinedInWinter { get; set; }

        public bool LeftInSummer { get; set; }

        public bool LeftInwinter { get; set; }
    }
}
