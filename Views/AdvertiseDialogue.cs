using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class AdvertiseDialogue : Dialogue
    {
        private const string TITLE = "Advertise Product";
        private const string Header = "Product Advertisement for {0}({1})";
        private const string UserPrompt = "> ";
        private const string ProductNamePrompt = "Product name";
        private const string ProductDescPrompt = "Product description";
        private const string PricePrompt = "Product Price ($d.cc)";

        private const string ProductNameError = "          Product name cannot be empty";
        private const string ProductDescError = "          Product Description cannot be empty";
        private const string NameDescSimilarError = "          Product Description cannot be the same as name of product";
        private const string PriceError = "        A currency value is required, e.g. $54.95, $9.99, $2314.15";

        private Account account;
        private AdvertiseValidate validate;

        public AdvertiseDialogue(Database database, Account acct) : base(TITLE, database)
        {
            validate = new AdvertiseValidate(database);
            this.account = acct;
        }

        public override void Display()
        {
            validate.WriteLine();
            validate.WriteLine(Header, account.Name, account.Email);
            foreach (char c in $"Product Advertisement for {account.Name}({account.Email})")
            {
                validate.Write("-");
            }
            validate.WriteLine();
            validate.WriteLine();

            validate.WriteLine(ProductNamePrompt);
            string ProductName = validate.Read(ProductNameError, UserPrompt, false);
            validate.WriteLine();

            string ProductDesc = "";
            while(true)
            {
                validate.WriteLine(ProductDescPrompt);
                ProductDesc = validate.Read(ProductDescError, UserPrompt, false);

                if (ProductDesc != ProductName) break;

                validate.WriteLine(NameDescSimilarError);
                validate.WriteLine();
            }
            validate.WriteLine();

            validate.WriteLine(PricePrompt);
            string ProductPrice = validate.Read(PriceError, UserPrompt, true);
            validate.WriteLine();

            Advertisement advert = new Advertisement(ProductName, ProductDesc, ProductPrice, account.Name);

            database.AddAdvertisement(advert, account);

            validate.WriteLine("Successfully added product {0}, {1}, {2}", advert.ProductName, advert.ProductDescription, advert.ProductPrice);
            validate.WriteLine();
        }
    }
}
