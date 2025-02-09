using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class ProductListDialogue : Dialogue
    {
        private const string TITLE = "View My Product List";
        private const string Header = "Product List for {0}({1})";
        private const string ColumnHeads = "Item #  Product name    Description     List price      Bidder name     Bidder email    Bid amt";


        private Account acct;
        private UserInterface write;

        public ProductListDialogue(Database db, Account acct) : base(TITLE, db)
        {
            write = new UserInterface();
            this.acct = acct;
        }

        public override void Display()
        {
            List<Advertisement> adverts = new List<Advertisement>();
            write.WriteLine();
            write.WriteLine(Header, acct.Name, acct.Email);
            foreach (char c in $"Product List for {acct.Name}({acct.Email})")
            {
                write.Write("-");
            }

            write.WriteLine();

            acct.GetAdvertisements(adverts);

            if (adverts.Count == 0)
            {
                write.WriteLine();
                write.WriteLine("You have no advertised products at the moment.");
                write.WriteLine();
            } else
            {
                int i = 0;

                write.WriteLine(ColumnHeads);

                foreach (Advertisement advert in adverts)
                {
                    Console.WriteLine("{0}         {1}     {2}     {3}     {4}     {5}     {6}", i + 1, advert.ProductName,
                        advert.ProductDescription, advert.ProductPrice, advert.BidderName, advert.BidderEmail, advert.BidAmount);
                    i++;
                }
                write.WriteLine();
            }
        }
    }
}
