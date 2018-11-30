namespace Sportiada.Services.AlpineSki.Models.Intermediate
{
    using Data.Models.AlpineSki;

    public class IntermediateAlpineSkiModel
    {
        public string Name { get; set; }

        public string Time { get; set; }

        public int Place { get; set; }

        public string Difference { get; set; }

        public string Speed { get; set; }

        public ResultAlpineSki ResultAlpineSki { get; set; }
    }
}
