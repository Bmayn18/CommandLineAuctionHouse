using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class ClientMenu : MenuDisplay
    {
        private const string TITLE = "Client Menu";
        private const string SignOut = "Sign Out";
        private const string Farewell = "";

        public ClientMenu(Database db, Account account) : base(TITLE, db, account, false, true,
            new AdvertiseDialogue(db, account), 
            new ProductListDialogue(db, account), 
            new AdvertisedProductDialogue(db, account),
            new ProductBidsDialogue(db, account),
            new PurchasedItemsDialogue(db, account),  
            new ExitDialogue(SignOut, Farewell))
        {
        }
    }
}
