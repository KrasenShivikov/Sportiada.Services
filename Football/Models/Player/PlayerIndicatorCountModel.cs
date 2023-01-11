namespace Sportiada.Services.Football.Models.Player
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class PlayerIndicatorCountModel
    {
        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public string TeamName { get; set; }

        public int IndicatorCount { get; set; }
    }
}
