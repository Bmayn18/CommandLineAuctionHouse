using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class GetAddressDialogue : Dialogue
    {
        private const string TITLE = "Get Address";
        private const string Header = "Personal Details for {0}({1})";
        private const string UserPrompt = "> ";

        private const string AddressPrompt = "Please provide your home address.";
        private const string UnitNumPrompt = "Unit number (0 = none):";
        private const string StreetNumPrompt = "Street number:";
        private const string StreetNamePrompt = "Street name:";
        private const string StreetSuffixPrompt = "Street suffix:";
        private const string CityPrompt = "City:";
        private const string StatePrompt = "State (ACT, NSW, NT, QLD, SA, TAS, VIC, WA):";
        private const string PostCodePrompt = "Postcode:";

        private const string UnitNumError = "          Unit number must be a non-negative integer";
        private const string StreetNumError = "        Street Number must be a positive integer";
        private const string StreetNameError = "           Street Name must be a text string";
        private const string StreetSuffixError = "         Street Suffix must be a text string";
        private const string CityError = "         City must be a text string";
        private const string StateError = "        State must be a valid state (ACT, NSW, NT, QLD, SA, TAS, VIC, WA";
        private const string PostCodeError = "        Postcode must be between 1000 and 9999";


        private Database db;
        private Account acct;
        private AddressValidate validate;

        public GetAddressDialogue(Database database, Account account) : base(TITLE, database)
        {
            validate = new AddressValidate(database);
            this.db = database;
            this.acct = account;
        }

        public override void Display()
        {
            validate.WriteLine(Header, acct.Name, acct.Email);
            foreach (char c in $"Personal Details for {acct.Name}({acct.Email})")
            {
                validate.Write("-");
            }
            validate.WriteLine();

            validate.WriteLine();
            validate.WriteLine(AddressPrompt);
            validate.WriteLine();

            validate.WriteLine(UnitNumPrompt);
            string UnitNumber = validate.Read(UnitNumError, UserPrompt, "UnitNumber");
            validate.WriteLine();

            validate.WriteLine(StreetNumPrompt);
            string StreetNumber = validate.Read(StreetNumError, UserPrompt, "StreetNumber");
            validate.WriteLine();

            validate.WriteLine(StreetNamePrompt);
            string StreetName = validate.Read(StreetNameError, UserPrompt, "String");
            validate.WriteLine();

            validate.WriteLine(StreetSuffixPrompt);
            string StreetSuffix = validate.Read(StreetSuffixError, UserPrompt, "String");
            validate.WriteLine();

            validate.WriteLine(CityPrompt);
            string City = validate.Read(CityError, UserPrompt, "String");
            validate.WriteLine();

            validate.WriteLine(StatePrompt);
            string State = validate.Read(StateError, UserPrompt, "State");
            validate.WriteLine();

            validate.WriteLine(PostCodePrompt);
            string PostCode = validate.Read(PostCodeError, UserPrompt, "PostCode");
            validate.WriteLine();
            Address address = new Address(UnitNumber, StreetNumber, StreetName, StreetSuffix, City, State, PostCode);
            acct.SetAddress(address);

            int.TryParse(UnitNumber, out int _UnitNumber);

            if (_UnitNumber == 0)
            {
                validate.WriteLine("Address has been updated to {0} {1} {2}, {3} {4} {5}", address.AddressNumber, 
                    address.StreetName, address.StreetSuffix, address.City, address.State, address.PostCode);
            } else
            {
                validate.WriteLine("Address has been updated to {0}/{1} {2} {3}, {4} {5} {6}", address.UnitNumber, 
                    address.AddressNumber, address.StreetName, address.StreetSuffix, address.City, address.State, 
                    address.PostCode);
            }

            validate.WriteLine();

        }
    }
}
