using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Model
{
    public class Bought : Advertisement
    {
        public string SellerEmail { get; }

        public string AmountPaid { get; }

        public string DeliveryOption { get; }

        public Bought(string sellerEmail, string ProductName, string Description, string ListPrice, string amountPaid, string deliveryOption, string createdby) : base(ProductName, Description, ListPrice, createdby)
        {
            this.SellerEmail = sellerEmail;
            this.AmountPaid = amountPaid;
            this.DeliveryOption = deliveryOption;
        }
    }
}
