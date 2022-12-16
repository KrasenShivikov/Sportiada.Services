namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballPlayerCountryAdminService
    {
        void Create(int playerId, int countryId, bool mainCountry);

        void Delete(int playerId, int countryId);

        FootballPlayerCountry GetPlayerCountry(int playerId, int countryId); 
    }
}
