namespace Sportiada.Web.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static string MinutesAfterHalftime(this string minute, bool firstHalf)
        {
            int parseMinute = int.Parse(minute);

            if (firstHalf == true && parseMinute > 45)
            {
                int overMinutes = parseMinute - 45;

                minute = $"45 + {overMinutes}'";
            }

            else if (parseMinute > 90)
            {
                int overMinutes = parseMinute - 90;

                minute = $"90 + {overMinutes}'";
            }

            else
            {
                minute = $"{parseMinute}'";
            }

            return minute;
        }
    }
}
