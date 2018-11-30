namespace Sportiada.Services.Football.Models.LineUp
{
    using System.Collections.Generic;

    public class LineUpViewModel
    {
        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public int PlayerNumber { get; set; }

        public string CountryPicture { get; set; }

        public Dictionary<string, string> Goals { get; set; }

        public Dictionary<string, string> Cards { get; set; }

        public string PictureOut { get;set; }
    }
}
