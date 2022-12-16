namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballGameRefereeAdminService
    {
        FootballGameRefereeAdminModel ById(int id);
        void Create(int gameId, int refereeId, int typeId);

        void Update(int id, int gameId, int refereeId, int typeId);

        void Delete(int id);

        FootballGameReferee GetGameReferee(int id);
    }
}
