using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballPlayerGameAdminModel
    {
        public int Id { get; set; }

        public string ProfileName { get; set; }

        public int Number { get; set; }

        public string Picture { get; set; }

        public string Country { get; set; }
    }
}
