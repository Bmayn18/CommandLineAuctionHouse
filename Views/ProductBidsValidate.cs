using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class ProductBidsValidate : UserInterface
    {
        private Database db;

        public ProductBidsValidate(Database db)
        {
            this.db = db;
        }

        public new bool Read(string errormessage, string UserPrompt)
        {
            string input;

            while (true)
            {
                Write(UserPrompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (input == "yes") return true;
                    if (input == "no") return false;
                }

                WriteLine(errormessage);
            }
        }

        public int Read(string errormessage, string UserPrompt, int lowerbound, int upperbound)
        {
            string input;
            int output;

            while (true)
            {
                Write(UserPrompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    int.TryParse(input, out output);
                    if (output <= upperbound && output >= lowerbound) return output;
                }

                WriteLine(errormessage);
            }
        }
    }
}
