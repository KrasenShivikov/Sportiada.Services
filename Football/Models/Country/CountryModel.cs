namespace Sportiada.Services.Football.Models.Country
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class CountryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string PicturePath { get; set; }
    }
}
