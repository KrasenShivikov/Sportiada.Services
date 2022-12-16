using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballRefereeAdminModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CountryAdminModel Country { get; set; }

        public DateTime BirthDate { get; set; }

        public string Picture { get; set; }
    }
}
