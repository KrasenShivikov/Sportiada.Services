namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballGameAdminService
    {
        FootballGameAdminModel ById(int id);
        IEnumerable<FootballGameListAdminModel> GamesByRoundId(int roundId);

        void Create(int attendance, DateTime date, int roundId);

        void Update(int id, int attendance, DateTime date, int roundId);

        void Delete(int id);

        FootballGame GetGame(int id);
    }
}
