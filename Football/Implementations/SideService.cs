namespace Sportiada.Services.Football.Implementations
{
    using Interfaces;
    using Models.Player;
    using Models.Season;
    using Models.Side;
    using Models.Team;
    using Sportiada.Data;
    using Services.Models;
    using System.Linq;

    public class SideService : ISideService
    {
        private readonly SportiadaDbContext db;

        public SideService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public SideTeamModel ById(int id)
         => this.db
            .FootballSides
            .Where(s => s.Id == id)
            .Select(s => new SideTeamModel
            {
                Id = s.Id,
                Name = s.Name,
                Season = new SeasonModel
                {
                    Id = s.Season.Id,
                    Name = s.Season.Name
                },
                Team = new TeamModel
                {
                    Id = s.Team.Id,
                    Name = s.Team.Name
                },
                Players = s.Players.Select(p => new PlayerSideModel
                {
                    Id = p.Player.Id,
                    Name = p.Player.Name,
                    PlayerNumber = p.PlayerNumber,
                    PlayerPicture = p.PlayerPicture,
                    FirstTeam = p.FirstTeam,
                    Under23 = p.Under23,
                    Under18 = p.Under18,
                    OnLoanOut = p.OnLoamOut,
                    Country = new CountryFootballGameModel
                    {
                        PicturePath = p.Player.Countries.Any() ? p.Player.Countries.Where(c => c.MainCountry == true).First().Country.LargePicturePath : string.Empty
                    }
                }).ToList()                     
            }).First();
    }
}
