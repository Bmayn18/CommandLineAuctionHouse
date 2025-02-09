using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class PurchasedItemsDialogue : Dialogue
    {
        private const string TITLE = "View My Purchased Items";
        private const string Header = "Purchased Items for {0}({1})";
        private const string ColumnHeader = "Item #  Seller name    Product name    Description   List price    Amt paid     Delivery option";

        private Account account;

        private UserInterface write;

        public PurchasedItemsDialogue(Database db, Account acct) : base(TITLE, db)
        {
            write = new UserInterface();
            this.account = acct;
        }

        public override void Display()
        {
            write.WriteLine(Header, account.Name, account.Email);
            foreach (char c in $"Purchased Items for {account.Name}({account.Email})")
            {
                write.Write("-");
            }

            List<Bought> PurchasedItems = new List<Bought>();
            account.GetPurchaseItems(PurchasedItems);

            write.WriteLine();

            if (PurchasedItems.Count != 0)
            {
                write.WriteLine();
                write.WriteLine(ColumnHeader);
                int i = 0;
                foreach (Bought item in PurchasedItems)
                {
                    write.WriteLine("{0}         {1}     {2}     {3}     {4}     {5}     {6}", i + 1, item.SellerEmail,
                        item.ProductName, item.ProductDescription, item.ProductPrice, item.BidAmount, item.DeliveryOption);
                    i++;
                }

                write.WriteLine();
            } else
            {
                write.WriteLine();
                write.WriteLine("You have no purchased products at the moment");
                write.WriteLine();
            }
        }
    }
}
