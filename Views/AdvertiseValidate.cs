using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class AdvertiseValidate : UserInterface
    {
        private Database database;

        public AdvertiseValidate(Database db)
        {
            this.database = db;
        }

        public string Read(string errormessage, string UserPrompt, bool Currency)
        {
            string input;

            while (true)
            {
                Write(UserPrompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (Currency == true)
                    {
                        if (ValidCurrency(input)) break;
                    } else if (Currency == false)
                    {
                        break;
                    }
                }

                WriteLine(errormessage);
            }

            return input;
        }

        private bool ValidCurrency(string input)
        {
            string pattern = "([$][0-9,]+.[0-9]{2})";
            Regex r = new Regex(pattern);

            if (r.IsMatch(input)) return true;

            return false;
        }
    }
}
