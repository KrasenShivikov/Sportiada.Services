using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Football.Models.Player
{
    public class GoalkeeperStatisticModel
    {
        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public string TeamName { get; set; }

        public int MatchCount { get; set; }

        public int CleanWebCount { get; set; }

        public int SavedPenalties { get; set; }

        public int UnsavedPenalties { get; set; }
    }
}
