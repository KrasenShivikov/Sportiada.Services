namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FootballPlayerAdminService : IFootballPlayerAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballPlayerAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<FootballPlayerAdminModel> All()
            => this.db.FootballPlayers
                       .Select(p => new FootballPlayerAdminModel
                       {
                           Id = p.Id,
                           FirstName = p.FirstName,
                           LastName = p.LastName,
                           ProfileName = p.ProfileName,
                           Picture = p.Squads.FirstOrDefault().Picture,
                           Countries = p.Countries.Count == 0 ? null :
                                       p.Countries
                                        .Where(c => c.MainCountry)
                                        .Select(c => new CountryAdminModel
                                        {
                                            Id = c.CountryId,
                                            Name = c.Country.Name,
                                            PicturePath = c.Country.PicturePath
                                        }).ToList(),
                       }).OrderByDescending(p => p.Id).ToList();


        public FootballPlayerProfileAdminModel ById(int id)
        {
            var player = this.db.FootballPlayers
                   .Where(p => p.Id == id)
                   .Select(p => new FootballPlayerProfileAdminModel
                   {
                       Id = p.Id,
                       FirstName = p.FirstName,
                       LastName = p.LastName,
                       Picture = p.Squads.FirstOrDefault().Picture,
                       ProfileName = p.ProfileName,
                       BirthDate = p.BirthDate.Value,
                       BirthPlace = p.BirthPlace,
                       Height = p.Height,
                       Foot = p.Foot,
                       Transfers = p.Transfers.Any() ? p.Transfers.Select(t => new FootballPlayerTransferAdminModel
                       {
                           Id = t.Id,
                           Season = t.Season,
                           Date = t.Date,
                           CurrentTeam = t.CurrentTeam,
                           CurrentTeamCountryFlag = t.CurrentTeamCountryFlag,
                           CurrentTeamLogo = t.CurrentTeamLogo,
                           PlayerId = t.PlayerId,
                           OnLoan = t.OnLoan,
                           PreviousTeam = t.PreviousTeam,
                           PreviousTeamCountryFlag = t.PreviousTeamCountryFlag,
                           TransferPrice = t.TransferPrice,
                           PreviousTeamLogo = t.PreviousTeamLogo
                       }).OrderBy(t => t.SplittedDate[2])
                         .ThenBy(t => t.SplittedDate[1])
                         .ThenBy(t => t.SplittedDate[0]): null,
                       Countries = p.Countries.Any() ? p.Countries.Select(c => new CountryAdminModel
                       {
                           Id = c.CountryId,
                           Name = c.Country.Name,
                           PicturePath = c.Country.PicturePath,
                           ShortName = c.Country.ShortName
                       }) : null
                   }).FirstOrDefault();

            return player;
        }


        public void Create(string firstName, string lastName, DateTime birthDate, string profileName, string birthPlace, string Foot, int Height)
        {
            FootballPlayer player = new FootballPlayer
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                ProfileName = profileName,
                BirthPlace = birthPlace,
                Foot = Foot,
                Height = Height
            };

            this.db.Add(player);
            this.db.SaveChanges();

        }

        public void Update(int id, string firstName, string lastName, DateTime birthDate, string profileName, string birthPlace, string foot, int height)
        {
            FootballPlayer player = this.db.FootballPlayers.Where(p => p.Id == id).FirstOrDefault();
            player.FirstName = firstName;
            player.LastName = lastName;
            player.BirthDate = birthDate;
            player.ProfileName = profileName;
            player.BirthPlace = birthPlace;
            player.Foot = foot;
            player.Height = height;

            this.db.Update(player);
            this.db.SaveChanges();

        }

        public void Delete(int id)
        {
            FootballPlayer player = this.db.FootballPlayers.Where(p => p.Id == id).FirstOrDefault();
            this.db.Remove(player);
            this.db.SaveChanges();
        }  
    }
}
