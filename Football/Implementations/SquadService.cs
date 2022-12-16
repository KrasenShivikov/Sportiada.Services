namespace Sportiada.Services.Football.Implementations
{
    using Data;
    using Interfaces;
    using Sportiada.Services.Football.Models.Squad;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class SquadService : ISquadService
    {
        private readonly SportiadaDbContext db;

        public SquadService(SportiadaDbContext db)
        {
            this.db = db;
        }

        //public SquadTeamModel ById(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
