using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Model
{   
    /// <summary>
    /// A class designed to share data around the program
    /// </summary>
    public class Database
    {
        /// <summary>
        /// Name of File for Database
        /// </summary>
        private string FileName;

        /// <summary>
        /// List of user accounts
        /// </summary>
        public List<Account> Users = new List<Account>();

        /// <summary>
        /// List of total advertisements
        /// </summary>
        public List<Advertisement> AllProducts = new List<Advertisement>();

        /// <summary>
        /// constructor for grabbing the filename variable.
        /// </summary>
        /// <param name="fileName">name of file for database</param>
        public Database(string fileName)
        {
            FileName = fileName;
            Load();
        }

        /// <summary>
        /// Loads Data from txt file and implements it through this class
        /// </summary>
        public void Load()
        {
            if (File.Exists(FileName))
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    while(true) {
                        string TypeName = sr.ReadLine();

                        if (TypeName == null) break;
                        else if (TypeName == "Account")
                        {
                            LoadAccounts(sr);
                        } else if (TypeName == "Advert")
                        {
                            LoadAdvertisements(sr);
                        } else if (TypeName == "Bought")
                        {
                            LoadBought(sr);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Save data from this class onto the txt file
        /// </summary>
        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                foreach (Account acct in Users)
                {
                    sw.WriteLine("Account");
                    sw.WriteLine(acct.Name);
                    sw.WriteLine(acct.Email);
                    sw.WriteLine(acct.Password);
                    sw.WriteLine(acct.HasAddress);
                    if (acct.HasAddress == true)
                    {
                        sw.WriteLine(acct.Address.UnitNumber);
                        sw.WriteLine(acct.Address.AddressNumber);
                        sw.WriteLine(acct.Address.StreetName);
                        sw.WriteLine(acct.Address.StreetSuffix);
                        sw.WriteLine(acct.Address.City);
                        sw.WriteLine(acct.Address.State);
                        sw.WriteLine(acct.Address.PostCode);
                    }

                    foreach (Advertisement advert in acct.Advertisements)
                    {
                        sw.WriteLine("Advert");
                        sw.WriteLine(advert.ProductName);
                        sw.WriteLine(advert.ProductDescription);
                        sw.WriteLine(advert.ProductPrice);
                        sw.WriteLine(advert.CreatedBy);
                        sw.WriteLine(advert.HasBid);

                        if (advert.HasBid == true)
                        {
                            sw.WriteLine(advert.BidderName);
                            sw.WriteLine(advert.BidderEmail);
                            sw.WriteLine(advert.BidAmount);
                            sw.WriteLine(advert.DeliveryOption);

                            if (advert.DeliveryOption == "Home Delivery")
                            {
                                sw.WriteLine(advert.Deliveryaddress.UnitNumber);
                                sw.WriteLine(advert.Deliveryaddress.StreetName);
                                sw.WriteLine(advert.Deliveryaddress.StreetName);
                                sw.WriteLine(advert.Deliveryaddress.StreetSuffix);
                                sw.WriteLine(advert.Deliveryaddress.City);
                                sw.WriteLine(advert.Deliveryaddress.State);
                                sw.WriteLine(advert.Deliveryaddress.PostCode);
                            }
                            else
                            {
                                sw.WriteLine(advert.Deliverydate[0]);
                                sw.WriteLine(advert.Deliverydate[1]);
                            }
                        }
                    }

                    foreach (Bought item in acct.BoughtItems)
                    {
                        sw.WriteLine("Bought");
                        sw.WriteLine(item.SellerEmail);
                        sw.WriteLine(item.ProductName);
                        sw.WriteLine(item.ProductDescription);
                        sw.WriteLine(item.ProductPrice);
                        sw.WriteLine(item.BidAmount);
                        sw.WriteLine(item.DeliveryOption);
                        sw.WriteLine(item.CreatedBy);
                    }
                }

                sw.Close();
            }
        }

        /// <summary>
        /// method to create an account
        /// </summary>
        /// <param name="name">Name provided on login</param>
        /// <param name="email">Email provided on Login</param>
        /// <param name="password">Password provided on login</param>
        /// <returns> returns created account to user</returns>
        public Account CreateAccount(string name, string email, string password)
        {
            Account acct = new Account(name, email, password);
            Users.Add(acct);
            return acct;
        }

        /// <summary>
        /// Method to check if Email is already used while registering
        /// </summary>
        /// <param name="email">Email user is inputting while registering</param>
        /// <returns>returns true if email is use</returns>
        public bool EmailUsed(string email)
        {
            foreach (Account account in Users)
            {
                if (account.Find(email)) return true;
            }

            return false;
        }

        /// <summary>
        /// Determines if an account exists based on a match between their email and the list of accounts stored in Database.
        /// </summary>
        /// <param name="email">the email user inputs</param>
        /// <returns>Returns an Account class variable</returns>
        public Account AccountExists(string email)
        {
            foreach (Account account in Users)
            {
                if (account.Find(email)) return account;
            }

            return null;
        }

        /// <summary>
        /// Checks if Password supplied is correct
        /// </summary>
        /// <param name="password">Password provided to be checked</param>
        /// <param name="acct">Accpunt provided so that password can be checked against it</param>
        /// <returns>return true if password is matching</returns>
        public bool PasswordCorrect(string password, Account acct)
        {
            if (acct.PasswordMatch(password)) return true;

            return false;
        }

        /// <summary>
        /// A method to add an advertisement into the List stored in user account as well as the total list stored in database
        /// </summary>
        /// <param name="temp">Advertisement being added</param>
        /// <param name="acct">The account the advertisement is being stored into</param>
        public void AddAdvertisement(Advertisement temp, Account acct)
        {
            AllProducts.Add(temp);
            acct.NewAdvertisement(temp);
        }

        /// <summary>
        /// A method to search through the name and description of an advertisement using a regular experssion provided by the user
        /// </summary>
        /// <param name="input">The string the user is searching for</param>
        /// <param name="ProductName">The name of the product being searched</param>
        /// <param name="ProductDesc"> The description of the product being searched</param>
        /// <returns>returns true if regular expression was mapped in either the product name or description</returns>
        public bool SearchAdvertisements(string input, string ProductName, string ProductDesc)
        {
            string pattern = $"({input})";
            Regex r = new Regex(pattern);

            if (r.IsMatch(ProductName)) return true;
            if (r.IsMatch(ProductDesc)) return true;
  
            return false;
        }

        /// <summary>
        /// A method to make a bid to a product
        /// </summary>
        /// <param name="advertisement">The advertisement being bid on</param>
        /// <param name="acct">The user bidding on the advertisement</param>
        /// <param name="BidAmount">the amount the user is bidding</param>
        /// <param name="deliveryOption">The way the user will retrieve their item if they win</param>
        public void MakeBid(Advertisement advertisement, Account acct, string BidAmount, string deliveryOption)
        {
            advertisement.SetBid(acct.Name, acct.Email, BidAmount, deliveryOption);
        }

        /// <summary>
        /// A method to sell a product to another user
        /// </summary>
        /// <param name="email"> The email of the buyer</param>
        /// <param name="advert"> the advertisement being sold</param>
        /// <param name="seller"> the user selling the advert</param>
        /// <param name="deliveryOption">the way the user will be retrieving their data</param>
        public void SellProduct(string email, Advertisement advert, Account seller, string deliveryOption)
        {
            Account Buyer = AccountExists(email);
            Bought advertisement = new Bought(seller.Email, advert.ProductName, advert.ProductDescription, advert.ProductPrice, advert.BidAmount, deliveryOption, seller.Name);
            Buyer.BoughtItems.Add(advertisement);
            seller.Advertisements.Remove(advertisement);
        }

        public void LoadAccounts(StreamReader reader)
        {
            string Name = reader.ReadLine();
            string Email = reader.ReadLine();
            string password = reader.ReadLine();
            string hasaddress = reader.ReadLine();
            if (hasaddress == "true")
            {
                string Unitnumber = reader.ReadLine();
                string Addressnumber = reader.ReadLine();
                string StreetName = reader.ReadLine();
                string streetsuffix = reader.ReadLine();
                string city = reader.ReadLine();
                string state = reader.ReadLine();
                string postcode = reader.ReadLine();

                Address address = new Address(Unitnumber, Addressnumber, StreetName, streetsuffix, city, state, postcode);

                Users.Add(new Account(Name, Email, password, address, true));
            } else
            {
                Users.Add(new Account(Name, Email, password));
            }
        }

        public void LoadAdvertisements(StreamReader reader)
        {
            string productname = reader.ReadLine();
            string productdescription = reader.ReadLine();
            string productprice = reader.ReadLine();
            string Createdby = reader.ReadLine();
            string HasBid = reader.ReadLine();
            if (HasBid == "true")
            {
                string Biddername = reader.ReadLine();
                string bidderEmail = reader.ReadLine();
                string bidamount = reader.ReadLine();
                string DeliveryOption = reader.ReadLine();

                if (DeliveryOption == "Home Delivery")
                {
                    string Unitnumber = reader.ReadLine();
                    string addressnumber = reader.ReadLine();
                    string streetname = reader.ReadLine();
                    string streetsuffix = reader.ReadLine();
                    string city = reader.ReadLine();
                    string state = reader.ReadLine();
                    string postcode = reader.ReadLine();

                    Address address = new Address(Unitnumber, addressnumber, streetname, streetsuffix, city, state, postcode);

                    AllProducts.Add(new Advertisement(productname, productdescription, productprice, Createdby, Biddername, bidderEmail, bidamount, DeliveryOption, bool.Parse(HasBid), address));
                }
                else
                {
                    DateTime startDate = DateTime.Parse(reader.ReadLine());
                    DateTime endDate = DateTime.Parse(reader.ReadLine());

                    AllProducts.Add(new Advertisement(productname, productdescription, productprice, Createdby, Biddername, bidderEmail, bidamount, DeliveryOption, bool.Parse(HasBid), startDate, endDate));
                }
            } else
            {

            }

        }

        public void LoadBought(StreamReader reader)
        {
            string SellerEmail = reader.ReadLine();
            string ProductName = reader.ReadLine();
            string ProductDescription = reader.ReadLine();
            string ProductPrice = reader.ReadLine();
            string BidAmount = reader.ReadLine();
            string DeliveryOption = reader.ReadLine();
            string CreatedBy = reader.ReadLine();

            Bought bought = new Bought(SellerEmail, ProductName, ProductDescription, ProductPrice, BidAmount, DeliveryOption, CreatedBy);

            AccountExists(SellerEmail).BoughtItems.Add(bought);
        }
    }
}
