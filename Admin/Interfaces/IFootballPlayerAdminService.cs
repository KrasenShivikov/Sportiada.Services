namespace Sportiada.Services.Admin.Interfaces
{ 
    using Models;
    using System;
    using System.Collections.Generic;
    public interface IFootballPlayerAdminService
    {
        IEnumerable<FootballPlayerAdminModel> All();

        FootballPlayerProfileAdminModel ById(int id);

        void Create(string firstName, string lastName, DateTime birthDate, string profileName, string birthPlace, string Foot, int Height);

        void Update(int id, string firstName, string lastName, DateTime birthDate, string profileName, string birthPlace, string Foot, int Height);

        void Delete(int id);
    }
}
