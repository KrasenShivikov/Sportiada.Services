

namespace Sportiada.Services.Football.Implementations
{
    using Data;
    using Sportiada.Services.Football.Interfaces;
    using Sportiada.Services.Football.Models.Coach;
    using Sportiada.Services.Football.Models.Country;
    using Sportiada.Services.Football.Models.SquadCoach;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class CoachService : ICoachService
    {
        private readonly SportiadaDbContext db;
        public CoachService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SquadCoachModel> CoachesBySquad(int squadId)
        {
            var coaches = db.FootballSquadCoaches
            .Where(sp => sp.SquadId == squadId)
            .Select(sc => new SquadCoachModel
            {
                Coach = new CoachModel
                {
                    Id = sc.CoachId,
                    FirstName = sc.Coach.FirstName,
                    LastName = sc.Coach.LastName,
                    ShortName = sc.Coach.ShortName,
                    BirthDate = sc.Coach.BirthDate.Value,
                    Picture = sc.Coach.Picture,
                    Country = new CountryModel
                    {
                        Name = sc.Coach.Country.Name,
                        PicturePath = sc.Coach.Country.PicturePath
                    }
                },
                FromDate = sc.FromDate,
                ToDate = sc.ToDate,
                Position = sc.Position,
                SquadType = sc.SquadType,
                LeftInSeason = sc.LeftInSeason,
            });

            return coaches;
        }
    }
}
