using Sportiada.Services.Football.Models.Country;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Football.Models.Coach
{
    public class CoachModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ShortName { get; set; }

        public int CountryId { get; set; }

        public CountryModel Country { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age => GetAge();

        public string Description { get; set; }

        public string Picture { get; set; }

        private int GetAge()
        {
            DateTime current = DateTime.UtcNow;
            int result = current.Year - this.BirthDate.Year;

            if ((current.Month < this.BirthDate.Month) || (current.Month == this.BirthDate.Month && current.Day < this.BirthDate.Day))
            {
                result = result - 1;
            }

            return result;
        }
    }
}
