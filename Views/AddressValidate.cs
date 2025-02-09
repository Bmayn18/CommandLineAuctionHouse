using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class AddressValidate : UserInterface
    {
        private Database database;

        public AddressValidate(Database database)
        {
            this.database = database;
        }

        public string Read(string errormessage, string prompt, string type)
        {
            string input;
            int UnitNumber;
            int StreetNumber;
            int PostCode;
            bool StreetNumberZeroFlag;

            while(true)
            {
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (type == "UnitNumber")
                    {
                        if (HasValidNumber(input)) return input;

                    }
                    else if (type == "StreetNumber")
                    {
                        if (int.TryParse(input, out StreetNumber))
                        {
                            if (StreetNumber > 0) return input;
                            if (StreetNumber == 0) StreetNumberZeroFlag = true;
                        }
                    }
                    else if (type == "String")
                    {
                        if (IsTextOnly(input)) return input;
                    }
                    else if (type == "PostCode")
                    {
                        if (int.TryParse(input, out PostCode))
                        {
                            if (PostCode > 1000 && PostCode < 9999) return input;
                        }
                    }
                    else if (type == "State")
                    {
                        string inputUpper = input.ToUpper();

                        if (inputUpper == "ACT" || inputUpper == "NSW" || inputUpper == "NT"
                            || inputUpper == "QLD" || inputUpper == "SA" || inputUpper == "TAS"
                            || inputUpper == "VIC" || inputUpper == "WA")
                        {
                            return input;
                        }
                    }
                }

                WriteLine(errormessage);
            }
        }

        private bool HasValidNumber(string input)
        {
            int UnitNumber;

            if (int.TryParse(input, out UnitNumber))
            {
                if (UnitNumber >= 0) return true;
            }

            return false;
        }

        private bool IsTextOnly(string input)
        {
            string pattern = "^([A-Za-z ]+)$";
            Regex r = new Regex(pattern);

            if (r.IsMatch(input)) return true;

            return false;
        }
    }
}
