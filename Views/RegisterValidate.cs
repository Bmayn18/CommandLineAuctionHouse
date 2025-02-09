using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    public class RegisterValidate : UserInterface
    {
        private Database db;

        private const string EmailInUse = "        The supplied address is already in use.";
        public RegisterValidate(Database database)
        {
            this.db = database;
        }

        public string Read(string errormessage, string userprompt, string prompt, string type)
        {
            string input;
            bool InUse = false;

            while (true)
            {
                Write(userprompt);
                input = Console.ReadLine();

                if (type == "Email")
                {
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        if (!db.EmailUsed(input)) {
                            if (ValidEmail(input)) break;
                        } else
                        {
                            WriteLine(EmailInUse);
                            InUse = true;
                        }
                    }
                }
                else if (type == "Password")
                {
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        if (ValidPassword(input))
                        {
                            if (input.Length >= 8) break;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        break;
                    }
                }

                if (InUse == false)
                {
                    WriteLine(errormessage);
                }
                WriteLine();
                WriteLine(prompt);

                InUse = false;
            }

            return input;
        }

        public bool ValidEmail(string input)
        {
            string pattern = "([A-Za-z0-9._-])+([A-za-z0-9])@([A-Za-z0-9])([A-Za-z0-9-]+)([A-Za-z0-9])[.]([A-za-z]+)";
            Regex r = new Regex(pattern);

            if (r.IsMatch(input)) return true;
            else return false;
        }

        public bool ValidPassword(string input)
        {
            string CheckCapitals = "[A-Z]";
            string CheckLowerCase = "[a-z]";
            string CheckNumber = "[0-9]";
            string CheckSpecial = "[^A-Za-z0-9 ]";

            bool CapitalPresent = false;
            bool LowerCasePresent = false;
            bool NumberPresent = false;
            bool SpecialPresent = false;

            Regex r = new Regex(CheckCapitals);

            if (r.IsMatch(input)) CapitalPresent = true;
            r = new Regex(CheckLowerCase);
            if (r.IsMatch(input)) LowerCasePresent = true;
            r = new Regex(CheckNumber);
            if (r.IsMatch(input)) NumberPresent = true;
            r = new Regex(CheckSpecial);
            if (r.IsMatch(input)) SpecialPresent = true;

            if (CapitalPresent == true && LowerCasePresent == true && NumberPresent == true && SpecialPresent == true) return true;

            return false;
        }
    }
}
