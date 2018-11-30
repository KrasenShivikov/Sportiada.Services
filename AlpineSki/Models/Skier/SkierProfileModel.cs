namespace Sportiada.Services.AlpineSki.Models.Skier
{
    using System;

    public class SkierProfileModel : SkierModel
    {
        public DateTime BirthDate { get; set; }

        public string Club { get; set; }

        public string Gender { get; set; }

        public int Age => GetAge();

        public int GetAge()
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (BirthDate.Year * 100 + BirthDate.Month) * 100 + BirthDate.Day;

            return (a - b) / 10000;
        }
    }
}
