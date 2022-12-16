namespace Sportiada.Services.Football.Models.LineUp
{
    using System.Collections.Generic;

    public class LineUpFinalModel
    {
        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public int PlayerNumber { get; set; }

        public string CountryPicture { get; set; }

        public string PlayerPicture { get; set; }

        public Dictionary<string, List<string>> Goals { get; set; }

        public Dictionary<string, string> Cards { get; set; }

        public string PictureOut { get;set; }
    }
}
