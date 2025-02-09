using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Model
{
    /// <summary>
    /// A class to store an address
    /// </summary>
    public class Address
    {
        /// <summary>
        /// A non mandatory field to store a unit number
        /// </summary>
        public int UnitNumber { get; }
        /// <summary>
        /// a mandatory field to store an address number
        /// </summary>
        public int AddressNumber { get; }

        /// <summary>
        /// A mandatory field to store the streetname of the address
        /// </summary>
        public string StreetName { get; }

        /// <summary>
        /// a mandatory field to store the suffix of the street of the address
        /// </summary>
        public string StreetSuffix { get; }

        /// <summary>
        /// the city the address is in
        /// </summary>
        public string City { get; }

        /// <summary>
        /// the state the address is in
        /// </summary>
        public string State { get; }

        /// <summary>
        /// the post code the address is stored in
        /// </summary>
        public int PostCode { get; }

        /// <summary>
        /// A constructor to create an address
        /// </summary>
        /// <param name="unitNumber">A non mandatory field to store a unit number</param>
        /// <param name="addressNumber">a mandatory field to store an address number</param>
        /// <param name="streetName">A mandatory field to store the streetname of the address</param>
        /// <param name="streetSuffix">a mandatory field to store the suffix of the street of the address</param>
        /// <param name="city">the city the address is in</param>
        /// <param name="state">the state the address is in</param>
        /// <param name="postCode">the post code the address is stored in</param>
        public Address(string unitNumber, string addressNumber, string streetName, string streetSuffix, string city, string state, string postCode)
        {
            int.TryParse(unitNumber, out int _unitnumber);
            this.UnitNumber = _unitnumber;
            int.TryParse(addressNumber, out int _addressnumber);
            this.AddressNumber = _addressnumber;
            this.StreetName = streetName;
            this.StreetSuffix = streetSuffix;
            this.City = city;
            this.State = state;
            int.TryParse(postCode, out int _postcode);
            this.PostCode = _postcode;
        }
    }
}
