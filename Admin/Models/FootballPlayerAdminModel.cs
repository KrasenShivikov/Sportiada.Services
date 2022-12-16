namespace Sportiada.Services.Admin.Models
{
    using System;
    using System.Collections.Generic;
    public class FootballPlayerAdminModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileName { get; set; }

        public string Picture { get; set; }
        public string Team { get; set; }

        public IEnumerable<CountryAdminModel> Countries { get; set; }

    }
}
