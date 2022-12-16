namespace Sportiada.Services.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    public class FootballPlayerProfileAdminModel : FootballPlayerAdminModel
    {
        public int Age => GetAge();

        public int Height { get; set; }

        public string BirthPlace { get; set; }

        public DateTime BirthDate { get; set; }

        public string Foot { get; set; }

        public IEnumerable<FootballPlayerTransferAdminModel> Transfers { get; set; }

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