using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CommandLineAuctionHouse.Views
{
    internal class ProductBidsDialogue : Dialogue
    {
        private const string TITLE = "View Bids On My Products";
        private const string Header = "List Product Bids for {0}({1})";
        private const string ColumnHeader = "Item #  Product name    Description     List price      Bidder name     Bidder email    Bid amt";
        private const string SellPrompt = "would you like to sell something (yes or no)?";
        private const string UserPrompt = "> ";
        private const string PickProductPrompt = "Please enter an integer between 1 and 1";
        private const string SellConfirmMessage = "You have sold {0} to {1} for {2}";

        private const string SellPromptError = "Please enter yes or no";

        private Account account;
        private ProductBidsValidate validate;

        public ProductBidsDialogue(Database db, Account acct) : base(TITLE, db)
        {
            validate = new ProductBidsValidate(db);
            this.account = acct;
        }

        public override void Display()
        {
            validate.WriteLine(Header, account.Name, account.Email);
            foreach (char c in $"List Product Bids for {account.Name}({account.Email})")
            {
                validate.Write("-");
            }

            validate.WriteLine();

            List<Advertisement> adverts = new List<Advertisement>();
            account.GetBidAdvertisements(adverts);

            if (adverts.Count == 0)
            {
                validate.WriteLine();
                validate.WriteLine("No bids were found.");
                validate.WriteLine();
            } else
            {
                int i = 0;
                foreach (Advertisement advertisement in adverts)
                {
                    validate.WriteLine("{0}         {1}     {2}     {3}     {4}     {5}     {6}", i + 1, advertisement.ProductName,
                        advertisement.ProductDescription, advertisement.ProductPrice, advertisement.BidderName, advertisement.BidderEmail, advertisement.BidAmount);
                    i++;
                }

                validate.WriteLine();
                validate.WriteLine(SellPrompt);
                if (validate.Read(SellPromptError, UserPrompt))
                {
                    validate.WriteLine();
                    validate.WriteLine(PickProductPrompt);
                    int choice = validate.Read(PickProductPrompt, UserPrompt, 1, adverts.Count);
                    validate.WriteLine();

                    database.SellProduct(adverts[choice - 1].BidderEmail, adverts[choice - 1], account, adverts[choice - 1].DeliveryOption);

                    validate.WriteLine(SellConfirmMessage, adverts[choice - 1].ProductName, adverts[choice - 1].BidderName, adverts[choice - 1].BidAmount);
                    validate.WriteLine();
                }
            }
        }
    }
}
