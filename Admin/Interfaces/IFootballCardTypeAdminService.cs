namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballCardTypeAdminService
    {
        IEnumerable<FootballCardTypeAdminModel> CardTypes();

        void Create(string name, string picture);

        void Update(int id, string name, string picture);

        void Delete(int id);

        FootballCardType GetCardType(int id);
    }
}
