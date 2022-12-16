namespace Sportiada.Services.Admin.Interfaces
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface ICityAdminService
    {
        IEnumerable<CityAdminModel> All();

        CityAdminModel ById(int id);

        void Create(string name, int countryId);

        void Update(int id, string name, int countryId);

        void Delete(int id);
    }
}
