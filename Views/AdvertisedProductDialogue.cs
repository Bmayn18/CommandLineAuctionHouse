using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class AdvertisedProductDialogue : Dialogue
    {
        private const string TITLE = "Search For Advertised Products";
        private const string Heading = "Product search for {0}({1})";
        private const string UserPrompt = "> ";

        private const string SearchPrompt = "Please supply a search phrase (ALL to see all products";
        private const string SearchResultPrompt = "Search Result";
        private const string ColumnHeads = "Item #  Product name    Description     List price      Bidder name     Bidder email    Bid amt";
        private const string TableMenu = "Successfully added product {0}, {1}, {2}";

        private const string BidPrompt = "Would you like to place a bid on any of these items (yes or no)?";
        private const string PickItemPrompt = "Please enter a non-negative integer between 1 and {0}:";
        private const string BiddingIdentifier = "Bidding for {0} (regular price {1}), current highest bid {2}";
        private const string BidAmountPrompt = "How much do you bid?";
        private const string BidConfirmedMessage = "Your bid of {0} for {1} is placed";

        private const string SearchError = "No result was found please try again";
        private const string BidChoiceError = "Please enter yes or no";
        private const string PriceError = "        A currency value is required, e.g. $54.95, $9.99, $2314.15";

        private Account account;

        private AdvertisedProductValidate validate;

        public AdvertisedProductDialogue(Database database, Account account) : base(TITLE, database)
        {
            validate = new AdvertisedProductValidate(database);
            this.account = account;
        }

        public override void Display()
        {
            List<Advertisement> adverts = new List<Advertisement>();

            validate.WriteLine();
            validate.WriteLine(Heading, account.Name, account.Email);

            foreach (char c in $"Product search for {account.Name}({account.Email})")
            {
                validate.Write("-");
            }

            validate.WriteLine();
            validate.WriteLine();

            validate.WriteLine(SearchPrompt);
            validate.Read(SearchError, UserPrompt, account, adverts);

            validate.WriteLine();
            validate.WriteLine(SearchResultPrompt);

            foreach (char c in SearchResultPrompt)
            {
                validate.Write("-");
            }

            if (adverts.Count == 0)
            {
                validate.WriteLine();
                validate.WriteLine(SearchError);
                validate.WriteLine();
            } else
            {
                validate.WriteLine();
                validate.WriteLine(ColumnHeads);
                int i = 0;
                foreach (Advertisement advertisement in adverts)
                {
                    validate.WriteLine("{0}         {1}     {2}     {3}     {4}     {5}     {6}", i + 1, advertisement.ProductName,
                        advertisement.ProductDescription, advertisement.ProductPrice, advertisement.BidderName, advertisement.BidderEmail, advertisement.BidAmount);
                    i++;
                }
                validate.WriteLine();
                validate.WriteLine();
                validate.WriteLine(BidPrompt);
                if (validate.Read(BidChoiceError, UserPrompt))
                {
                    validate.WriteLine();
                    validate.WriteLine(PickItemPrompt, adverts.Count);
                    int choice = validate.Read(PickItemPrompt, UserPrompt, 1, adverts.Count);

                    validate.WriteLine();
                    validate.WriteLine(BiddingIdentifier, adverts[choice - 1].ProductName, adverts[choice - 1].ProductPrice, adverts[choice - 1].BidAmount);

                    validate.WriteLine();
                    validate.WriteLine();
                    validate.WriteLine(BidAmountPrompt);
                    string BidAmount = validate.Read(PriceError, UserPrompt, true, adverts[choice - 1]);

                    validate.WriteLine(BidConfirmedMessage, BidAmount, adverts[choice - 1].ProductName);
                    validate.WriteLine();
                    string DeliveryChoice = GetDeliveryOption();

                    adverts[choice - 1].SetBid(account.Name, account.Email, BidAmount, DeliveryChoice);

                    if (DeliveryChoice == "Click and Collect")
                    {
                        List<DateTime> dates = GetDeliveryTimes();
                        adverts[choice - 1].SetDeliveryDate(dates);

                        DateTime Startwindow = dates[0];
                        DateTime Endwindow = dates[1];

                        validate.WriteLine();
                        validate.WriteLine("Thank you for your bid. If successful, the item will be provided via collection between {0} on {1} and {2} on {3}", Startwindow.TimeOfDay, Startwindow.Date, Endwindow.TimeOfDay, Endwindow.Date);
                        validate.WriteLine();

                    } else if (DeliveryChoice == "Home Delivery")
                    {
                        Address DeliveryAddress = GetDeliveryAddress();
                        adverts[choice - 1].SetDeliveryAddress(DeliveryAddress);

                        validate.WriteLine();
                        validate.WriteLine("Thank you for your bid. If successful, the item will be provided via delivery to {0}/{1} {2} {3}, {4} {5} {6}", DeliveryAddress.UnitNumber, DeliveryAddress.AddressNumber, DeliveryAddress.StreetName, DeliveryAddress.StreetSuffix, DeliveryAddress.City, DeliveryAddress.State, DeliveryAddress.PostCode);
                        validate.WriteLine();
                    }
                }
            }
        }

        public string GetDeliveryOption()
        {
            string[] options = { "Click and Collect", "Home Delivery" };

            validate.WriteLine("Delivery Instructions");

            foreach (char c in "Delivery Instructions")
            {
                validate.Write("-");
            }
            validate.WriteLine();

            for (int i = 0; i < 2; i++)
            {
                validate.WriteLine("({0}) {1}", i + 1, options[i]);
            }
            validate.WriteLine();
            validate.WriteLine("Please select an option between 1 and 2");
            string choice = options[(validate.Read(PickItemPrompt, UserPrompt, 1, 2)) - 1];

            return choice;
        }

        public Address GetDeliveryAddress()
        {
            AddressValidate addressValidate = new AddressValidate(database);
            addressValidate.WriteLine();

            addressValidate.WriteLine("Please provide your delivery address");
            addressValidate.WriteLine();

            addressValidate.WriteLine("Unit number (0 = none):");
            string UnitNumber = addressValidate.Read("          Unit number must be a non-negative integer", "> ", "UnitNumber");
            addressValidate.WriteLine();

            addressValidate.WriteLine("Street number:");
            string StreetNumber = addressValidate.Read("        Street Number must be a positive integer", "> ", "StreetNumber");
            addressValidate.WriteLine();

            addressValidate.WriteLine("Street name:");
            string StreetName = addressValidate.Read("           Street Name must be a text string", "> ", "String");
            addressValidate.WriteLine();

            addressValidate.WriteLine("Street suffix:");
            string StreetSuffix = addressValidate.Read("         Street Suffix must be a text string", "> ", "String");
            addressValidate.WriteLine();

            addressValidate.WriteLine("City:");
            string City = addressValidate.Read("         City must be a text string", "> ", "String");
            addressValidate.WriteLine();

            addressValidate.WriteLine("State (ACT, NSW, NT, QLD, SA, TAS, VIC, WA):");
            string State = addressValidate.Read("        State must be a valid state (ACT, NSW, NT, QLD, SA, TAS, VIC, WA)", "> ", "State");
            addressValidate.WriteLine();

            addressValidate.WriteLine("Postcode (1000 .. 9999)");
            string PostCode = addressValidate.Read("        Postcode must be between 1000 and 9999", "> ", "PostCode");

            Address address = new Address(UnitNumber, StreetNumber, StreetName, StreetSuffix, City, State, PostCode);
            return address;
        }

        public List<DateTime> GetDeliveryTimes()
        {
            List<DateTime> deliveryTimes = new List<DateTime>();
            DateTime CurrentTime = DateTime.Now;

            validate.WriteLine();
            validate.WriteLine("Delivery window start (dd/mm/yyyy hh:mm)");
            DateTime DeliveryWindowStart = validate.ReadDateTime("Please enter a valid date and time", "> ");
            validate.WriteLine();

            deliveryTimes.Add(DeliveryWindowStart);

            validate.WriteLine("Delivery window end (dd/mm/yyyy hh:mm");
            DateTime DeliveryWindowEnd = validate.ReadDateTime("Please enter a valid date and time", "> ", DeliveryWindowStart);
            validate.WriteLine();

            deliveryTimes.Add(DeliveryWindowEnd);

            return deliveryTimes;
        }
    }
}
