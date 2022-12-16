using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballPlayerTransferAdminModel
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public string Season { get; set; }

        public string Date { get; set; }

        public List<int> SplittedDate => SplitDate();

        public string PreviousTeam { get; set; }

        public string PreviousTeamLogo { get; set; }

        public string PreviousTeamCountryFlag { get; set; }

        public string CurrentTeam { get; set; }

        public string CurrentTeamLogo { get; set; }

        public string CurrentTeamCountryFlag { get; set; }

        public string TransferPrice { get; set; }

        public string OnLoan { get; set; }

        private List<int> SplitDate()
        {
            int a;
            List<int> result = new List<int>();
            Dictionary <string, int> months = new Dictionary<string, int>
            {
                { "януари", 1 },
                { "февруари", 2 },
                { "март", 3 },
                { "април", 4 },
                { "май", 5 },
                { "юни", 6 },
                { "юли", 7 },
                { "август", 8 },
                { "септември", 9 },
                { "октомври", 10},
                { "ноември", 11},
                { "декември" ,12 },
            };


            var splittedDate = this.Date.Split(" ");

            if (splittedDate.Length == 3)
            {
                foreach (var e in splittedDate)
                {
                    if (int.TryParse(e, out a))
                    {
                        result.Add(int.Parse(e));
                    }
                    else
                    {

                        bool isSame = months.ContainsKey(e);
                        int value = isSame ? months[e.Trim().ToLower()] : 0;
                        result.Add(value);
                    }
                }
            }

            else
            {
                result.Add(0);
                result.Add(0);
                result.Add(0);
            }

            return result;
        }
    }
}
