using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class DeliveryOptDialogue : Dialogue
    {
        private const string TITLE = "Delivery Instructions";
        private const string OptionPrompt = "Please select an option between 1 and 2";
        private const string UserPrompt = "> ";
        private const string AddressPrompt = "Please provide your delivery address";
        private const string UnitNumPrompt = "Unit number (0 = none):";
        private const string StreetNumPrompt = "Street number:";
        private const string StreetNamePrompt = "Street name:";
        private const string SuffixPrompt = "Street suffix:";
        private const string CityPrompt = "City:";
        private const string StatePrompt = "State (ACT, NSW, NT, QLD, SA, TAS, VIC, Wa";
        private const string PostCodePrompt = "PosteCode (1000 .. 9999):";
        private const string DeliveryConfirm = "Thank you for your bid. If successful, ThreadExceptionEventArgs item will be provided via delivery to {0}/{1} {2} {3}, {4} {5} {6}";

        private Account acct;
        private Advertisement advertisement;

        private DeliveryValidate validate;
        private AddressValidate addressValidate;

        private string[] options = { "Click and Collect", "Home Delivery" };

        public DeliveryOptDialogue(Database db, Account account, Advertisement advert) : base(TITLE, db)
        {
            validate = new DeliveryValidate(db);
            acct = account;
            advertisement = advert;
        }

        public override void Display()
        {
            validate.WriteLine(TITLE);

            foreach (char c in TITLE)
            {
                validate.Write("-");
            }

            for (uint i = 0; i < options.Length; i++)
            {
                validate.WriteLine("({0}) {1}", i + 1, options[i]);
            }

            validate.WriteLine(OptionPrompt);
            string DeliveryChoice = validate.Read(OptionPrompt, UserPrompt, 1, options.Length, options);

            if (DeliveryChoice == "Click and Collect")
            {
                validate.WriteLine();
                validate.WriteLine("");
            } else
            {

            }
        }
    }
}
