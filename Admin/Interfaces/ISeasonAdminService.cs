namespace Sportiada.Services.Admin.Interfaces
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISeasonAdminService
    {
        int GetPreviousSeasonId(int id);
        SeasonAdminModel ById(int id);

        IEnumerable<SeasonAdminModel> All();

        void Create(string name, DateTime start, DateTime end);

        void Update(int id, string name, DateTime start, DateTime end);

        void Delete(int id);
    }
}
