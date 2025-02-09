using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    public class AdvertisedProductValidate : UserInterface
    {
        private Database database;

        public AdvertisedProductValidate(Database database)
        {
            this.database = database;
        }

        public void Read(string errormessage, string UserPrompt, Account acct, List<Advertisement> adverts)
        {
            string input;

            while(true)
            {
                Write(UserPrompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (input == "ALL")
                    {
                        foreach (Advertisement advertisement in database.AllProducts)
                        {
                            if (advertisement.CreatedBy != acct.Name)
                            {
                                adverts.Add(advertisement);
                                break;
                            }
                        }
                        break;
                    } else
                    {
                        foreach (Advertisement advertisement in database.AllProducts)
                        {
                            if (advertisement.CreatedBy != acct.Name)
                            {
                                if (database.SearchAdvertisements(input, advertisement.ProductName, advertisement.ProductDescription))
                                {
                                    adverts.Add(advertisement);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }

                WriteLine(errormessage);
            }
        }

        public bool Read(string errormessage, string UserPrompt)
        {
            string input;

            while(true)
            {
                Write(UserPrompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (input == "yes") return true;
                    if (input == "no") return false;
                }

                WriteLine(errormessage);
            }
        }

        public int Read(string errormessage, string UserPrompt, int lowerbound, int upperbound)
        {
            string input;
            int output;

            while (true)
            {
                Write(UserPrompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    int.TryParse(input, out output);
                    if (output <= upperbound && output >= lowerbound) return output;
                }

                WriteLine(errormessage);
            }
        }

        public string Read(string errormessage, string UserPrompt, bool Currency, Advertisement ad)
        {
            string input;
            double DoubleInput;
            double ListPrice;

            while (true)
            {
                Write(UserPrompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (Currency == true)
                    {
                        if (ValidCurrency(input))
                        {
                            if (ConvertToDouble(input, out DoubleInput) && ConvertToDouble(ad.ProductPrice, out ListPrice))
                            {
                                if (DoubleInput > ListPrice) break;
                            }
                        }
                    } else if (Currency == false)
                    {
                        break;
                    }
                }

                WriteLine(errormessage);
            }

            return input;
        }

        private bool ValidCurrency(string input)
        {
            string pattern = "([$][0-9,]+.[0-9]{2})";
            Regex r = new Regex(pattern);

            if (r.IsMatch(input)) return true;

            return false;
        }

        private bool ConvertToDouble(string input, out double output)
        {
            double _input;
            string RemovedDollarSign = "";
            
            foreach (char c in input)
            {
                if (c != '$')
                {
                    RemovedDollarSign = RemovedDollarSign + c;
                }
            }

            if (double.TryParse(RemovedDollarSign, out output)) return true;

            return false;
        }

        public DateTime ReadDateTime(string errormessage, string UserPrompt)
        {
            string input;
            DateTime CurrentTime = DateTime.Now;

            while(true)
            {
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (ValidateDate(input))
                    {
                        DateTime DeliveryWindow = DateTime.Parse(input);
                        if (DeliveryWindow.Hour >= (CurrentTime.Hour + 1))
                        {
                            return DeliveryWindow;
                        }

                        WriteLine();
                        WriteLine("Delivery window start must be at least one hour in the future");
                    }
                } else
                {
                    WriteLine();
                    WriteLine(errormessage);
                }
            }
        }

        public DateTime ReadDateTime(string errormessage, string UserPrompt, DateTime WindowStart)
        {
            string input;

            while(true)
            {
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (ValidateDate(input))
                    {
                        DateTime DeliveryWindow = DateTime.Parse(input);
                        if (DeliveryWindow.Hour >= WindowStart.Hour)
                        {
                            return DeliveryWindow;
                        }

                        WriteLine();
                        WriteLine("Delivery window end must be at least one hour later than the start");
                    }
                } else
                {
                    WriteLine();
                    WriteLine(errormessage);
                }
            }
        }

        public bool ValidateDate(string input)
        {
            string pattern = "^([0-9\\/]+) ([0-9]{2}:[0-9]{2})$";
            Regex r = new Regex(pattern);

            if (r.IsMatch(input)) return true;

            return false;
        }
    }
}
