namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballCoachAdminService
    {
        IEnumerable<FootballCoachAdminListModel> All();

        FootballCoachAdminModel ById(int id);
        void Create(string firstName, string lastName, string shortName, int countryId, DateTime birthDate, string picture, string description);

        void Update(int id, string firstName, string lastName, string shortName, int countryId, DateTime birthDate, string picture, string description);

        void Delete(int id);

        FootballCoach GetCoach(int id);
    }
}
