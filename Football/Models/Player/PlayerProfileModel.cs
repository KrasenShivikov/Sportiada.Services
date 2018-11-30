namespace Sportiada.Services.Football.Models.Player
{
    using Models.PlayerType;
    using Sportiada.Services.Football.Models.Season;
    using Sportiada.Services.Models;
    using System.Collections.Generic;

    public class PlayerProfileModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PlayerNumber { get; set; }

        public string Picture { get; set; }

        public PlayerTypeModel Type { get; set; }

        public CountryModel Country { get; set; }

        public List<SeasonModel> Seasons { get; set; }
    }
}
