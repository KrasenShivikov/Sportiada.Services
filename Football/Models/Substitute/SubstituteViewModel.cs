namespace Sportiada.Services.Football.Models.Substitute
{
    using System.Collections.Generic;

    public class SubstituteViewModel : SubstituteModel
    {
        public Dictionary<string, string> Goals { get; set; }

        public Dictionary<string, string> Cards { get; set; }

        public string Minute { get; set; }
    }
}
