using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballTournamentAdminModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }

        public CountryAdminModel Country { get; set; }
    }
}
