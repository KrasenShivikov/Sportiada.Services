namespace Sportiada.Services.Football.Models.Player
{
    using Models.Country;
    using Models.PlayerType;
    using Sportiada.Services.Football.Models.Season;
    using Sportiada.Services.Models;
    using Services.Admin.Models;
    using System.Collections.Generic;
    using System;

    public class PlayerProfileModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age => GetAge();

        public int PlayerNumber { get; set; }

        public string SquadType { get; set; }

        public string Picture { get; set; }

        public DateTime BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public string Foot { get; set; }

        public int Height { get; set; }

        public string Position { get; set; }

        public string ContractStartDate { get; set; }

        public string ContractEndDate { get; set; }

        public bool OnLoan { get; set; }

        public bool JoinedInSummer { get; set; }

        public bool JoinedInWinter { get; set; }

        public bool LeftInSummer { get; set; }

        public bool LeftInWinter { get; set; }

        public IEnumerable<FootballPlayerTransferAdminModel> Transfers { get; set; }

        public PlayerTypeModel Type { get; set; }

        public IEnumerable<CountryModel> Countries { get; set; }

        public List<SeasonModel> Seasons { get; set; }

        private int GetAge()
        {
            DateTime current = DateTime.UtcNow;
            int result = current.Year - this.BirthDate.Year;

            if ((current.Month < this.BirthDate.Month) || (current.Month == this.BirthDate.Month && current.Day < this.BirthDate.Day))
            {
                result = result - 1;
            }

            return result;
        }
    }
}
