﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballPlayerInAdminModel
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public FootballPlayerGameAdminModel Player { get; set; }

        public int SubstituteId { get; set; }

        public string InIcon { get; set; }
    }
}
