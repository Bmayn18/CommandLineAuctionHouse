using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Model
{
    /// <summary>
    /// Class to implement the functionality of a user entering a session
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Name of the user
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// User's password
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// The user's home address
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// A boolean to determine whether the program needs to ask the user for their address
        /// </summary>
        public bool HasAddress = false;

        /// <summary>
        /// A list of advertisements the user has made
        /// </summary>
        public List<Advertisement> Advertisements = new List<Advertisement>();

        /// <summary>
        /// A list of products bought from other users
        /// </summary>
        public List<Bought> BoughtItems = new List<Bought>();

        /// <summary>
        /// Constructor to create a user
        /// </summary>
        /// <param name="name">name of the user</param>
        /// <param name="email">email of the user</param>
        /// <param name="password">password of the user</param>
        public Account(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        public Account(string name, string email, string password, Address address, bool hasaddress)
        {
            Name = name;
            Email = email;
            Password = password;
            Address = address;
            HasAddress = hasaddress;
        }

        /// <summary>
        /// A method to determine if the Email supplied and the email of the user match.
        /// </summary>
        /// <param name="email">Email being checked</param>
        /// <returns>returns true if emails match</returns>
        public bool Find(string email)
        {
            if (Email == email) return true;

            return false;
        }

        /// <summary>
        /// A method to determine if passwords match
        /// </summary>
        /// <param name="password"> the password being checked if it matches</param>
        /// <returns></returns>
        public bool PasswordMatch(string password)
        {
            if (Password == password) return true;

            return false;
        }

        /// <summary>
        /// A method to set the address of the user
        /// </summary>
        /// <param name="temp">The address the user's address is being set to</param>
        public void SetAddress(Address temp)
        {
            Address = temp;
            HasAddress = true;
        }

        /// <summary>
        /// A method to create a new advertisement, adding it to the list of advertisements
        /// </summary>
        /// <param name="temp"> advertisement being added</param>
        public void NewAdvertisement(Advertisement temp)
        {
            Advertisements.Add(temp);
        }

        /// <summary>
        /// a functyion to get the advertisements the user made
        /// </summary>
        /// <param name="advertisements"> a list of advertisement being returned</param>
        public void GetAdvertisements(List<Advertisement> advertisements)
        {
            foreach (Advertisement advert in Advertisements)
            {
                advertisements.Add(advert);
            }
        }

        /// <summary>
        /// function to get the advertisements that have been bid on
        /// </summary>
        /// <param name="advertisements"> a list of advertisements that have been bid on.</param>
        public void GetBidAdvertisements(List<Advertisement> advertisements)
        {
            foreach (Advertisement adverts in Advertisements)
            {
                if (adverts.HasBid == true)
                {
                    advertisements.Add(adverts);
                }
            }
        }

        /// <summary>
        /// A method to display the items a user bought
        /// </summary>
        /// <param name="PurchasedItems">A list of items that were purchased and are being returned</param>
        public void GetPurchaseItems(List<Bought> PurchasedItems)
        {
            foreach (Bought item in BoughtItems)
            {
                PurchasedItems.Add(item);
            }
        }
    }
}
