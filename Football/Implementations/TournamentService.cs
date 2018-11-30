namespace Sportiada.Services.Football.Implementations
{
    using Interfaces;
    using Models.Tournament;
    using System.Collections.Generic;  
    using Sportiada.Data;
    using System.Linq;
    

    public class TournamentService : ITournamentService
    {
        private readonly SportiadaDbContext db;

        public TournamentService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<TournamentModel> All()
           => this.db
            .Tournaments
            .Select(t => new TournamentModel
            {
                Id = t.Id,
                Name = t.Name
            });
    }
}
