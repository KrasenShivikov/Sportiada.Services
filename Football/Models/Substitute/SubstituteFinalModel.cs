namespace Sportiada.Services.Football.Models.Substitute
{
    using System.Collections.Generic;

    public class SubstituteFinalModel : SubstituteModel
    {
        public Dictionary<string, string> Goals { get; set; }

        public Dictionary<string, string> Cards { get; set; }
    }
}
