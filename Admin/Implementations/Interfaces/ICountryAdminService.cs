namespace Sportiada.Services.Admin.Interfaces
{
    using Admin.Models;
    using Data.Models;
    using System.Collections.Generic;
 
    public interface ICountryAdminService
    {
        void Create(string name, string shortName, string picturePath);

        IEnumerable<CountryAdminModel> All();

        CountryAdminModel ById(int Id);

        void Change(int id, string name, string shortName, string picturePath);

        void Delete(int id);
    }
}
