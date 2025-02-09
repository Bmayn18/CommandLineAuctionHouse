using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class LogInValidate : UserInterface
    {
        private Database database;

        public LogInValidate(Database db)
        {
            this.database = db;
        }
        public bool Read(string errormessage, string prompt, Account acct)
        {
            string input;

            while(true)
            {
                Write(prompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (database.PasswordCorrect(input, acct)) return true;
                    break;
                }

                WriteLine(errormessage);
            }

            return false;
        }

        public Account Read(string Prompt)
        {
            string input;
            Account acct;

            while (true)
            {
                Write(Prompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    acct = database.AccountExists(input);
                    break;
                }

                WriteLine("        The supplied value is not a valid email address.");
            }

            return acct;
        }
    }
}
