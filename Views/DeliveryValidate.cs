using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class DeliveryValidate : UserInterface
    {
        private Database db;

        public DeliveryValidate(Database db)
        {
            this.db = db;
        }

        public string Read(string errormessage, string Userprompt, int lowerbound, int upperbound, string[] options)
        {
            string input;
            int _input;

            while (true)
            {
                Write(Userprompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (int.TryParse(input, out _input))
                    {
                        if (_input >= lowerbound && _input <= upperbound) break;
                    }

                    WriteLine(errormessage);
                }
            }
            return options[_input];
        }
    }
}
