using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    public class MenuValidate : UserInterface
    {

        public int Read(string errormessage, string UserPrompt, int lowerbound, int Upperbound)
        {
            string input;
            int opt;

            while(true)
            {
                Write(UserPrompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (int.TryParse(input, out opt))
                    {
                        if (opt >= lowerbound && opt <= Upperbound)
                        {
                            break;
                        }
                    }
                }

                WriteLine();
                WriteLine(errormessage, lowerbound, Upperbound);
            }

            return opt;
        }
    }
}
