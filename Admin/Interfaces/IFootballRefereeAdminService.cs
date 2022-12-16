namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballRefereeAdminService
    {
        IEnumerable<FootballRefereeAdminModel> All();

        void Create(string name, DateTime birthdate, int countryId, string pictureId);

        void Update(int id, string name, DateTime birthdate, int countryId, string pictureId);

        void Delete(int id);

        FootballReferee GetReferee(int id);
    }
}
