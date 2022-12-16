namespace Sportiada.Services.Admin.Models
{
    using System.Collections.Generic;
    public class FootballTeamAdminModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Logo { get; set; }

        public IEnumerable<FootballSquadAdminModel> Squads { get; set; }

        public IEnumerable<FootballStadiumAdminModel> Stadiums { get; set; }

        public CityAdminModel City { get; set; }
    }
}
