namespace Sportiada.Services.Football.Models.Player
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class GoalkeeperGameModel
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }

        public bool IsInLineUp { get; set; }

        public bool Out{ get; set; }

        public int OutMinute { get; set; }

        public bool OutFirstHalf { get; set; }

        public bool In { get; set; }

        public int InMinute { get; set; }

        public bool InFirstHalf { get; set; }
    }
}
