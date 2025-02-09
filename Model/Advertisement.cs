using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Model
{
    public class Advertisement
    {
        public string ProductName { get; }
        public string ProductDescription { get; }
        public string ProductPrice { get; }

        public string CreatedBy { get; }

        public string BidderName { get; set; }

        public string BidderEmail { get; set; }
        public string BidAmount { get; set; }

        public string DeliveryOption { get; set; }

        public bool HasBid = false;

        public Address Deliveryaddress { get; set; }

        public List<DateTime> Deliverydate { get; set; } 



        public Advertisement(string productName, string productDescription, string productPrice, string createdby)
        {
            this.ProductName = productName;
            this.ProductDescription = productDescription;
            this.ProductPrice = productPrice;
            CreatedBy = createdby;
        }

        public Advertisement(string productName, string productDescription, string productPrice, string createdBy, string bidderName, string bidderEmail, string bidAmount, string deliveryOption, bool hasBid, Address deliveryaddress) : this(productName, productDescription, productPrice, createdBy)
        {
            BidderName = bidderName;
            BidderEmail = bidderEmail;
            BidAmount = bidAmount;
            DeliveryOption = deliveryOption;
            HasBid = hasBid;
            Deliveryaddress = deliveryaddress;
        }

        public Advertisement(string productName, string productDescription, string productPrice, string createdBy, string bidderName, string bidderEmail, string bidAmount, string deliveryOption, bool hasBid, DateTime startDate, DateTime endDate)
        {
            BidderName = bidderName;
            BidderEmail = bidderEmail;
            BidAmount = bidAmount;
            DeliveryOption = deliveryOption;
            HasBid = hasBid;
            Deliverydate!.Add(startDate);
            Deliverydate.Add(endDate);
        }

        public void SetBid(string biddername, string bidderemail, string bidamount, string deliveryoption)
        {
            BidderName = biddername;
            BidderEmail = bidderemail;
            BidAmount = bidamount;
            HasBid = true;
            SetDeliveryOption(deliveryoption);
        }

        public void SetDeliveryOption(string deliveryoption)
        {
            DeliveryOption = deliveryoption;
        }
        
        public void SetDeliveryAddress(Address address)
        {
            Deliveryaddress = address;
        }

        public void SetDeliveryDate(List<DateTime> dates)
        {
            Deliverydate = dates;
        }
    }
}
