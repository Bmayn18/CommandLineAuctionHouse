using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class RegisterDialogue : Dialogue
    {
        private const string TITLE = "Register";
        private const string Header = "Registration";
        private const string accent = "------------";

        private const string NamePrompt = "Please enter your name";
        private const string EmailPrompt = "Please enter your email address";
        private const string UserPrompt = "> ";
        private const string PasswordPrompt = "Please choose a password";
        private const string ConfirmMessage = "Client {0}({1}) has successfully registered at the Auction House";
        private const string EmailError = "        The supplied value is not a valid email address";
        private const string PasswordError = "         The supplied value is not a valid password";
        private const string NameError = "         The supplied value is not a valid name";

        /// <summary>
        /// A class that inherits from the UserInterface class and is designed to specifically validate this dialogue class
        /// </summary>
        private RegisterValidate validate;

        /// <summary>
        /// The Database, which handles moving and providing the data to the rest of the program
        /// </summary>
        private Database database;

        /// <summary>
        /// The acct class, which works to keep the user in a session by implementing their fields such as name, and email.
        /// </summary>
        private Account acct;

        /// <summary>
        /// An array holding the strings that descibes the constraints on creating passwrods to the user
        /// </summary>
        private string[] Conditions = { "* At least 8 characters", "* No white space characters", "* At least one upper-case letter", 
                                        "* At least one lower-case letter", "* At least one digit", "* At least one special character" };
        /// <summary>
        /// A constructor for pulling the database from the menu class
        /// </summary>
        /// <param name="database">The Database, which handles moving and providing the data to the rest of the program</param>
        public RegisterDialogue(Database database) : base(TITLE, database)
        {
            validate = new RegisterValidate(database);
            this.database = database;
        }
        /// <summary>
        /// The display method which provides an interace for users to create an account, which will grant them access to the rest of the program..
        /// </summary>
        public override void Display()
        {
            string name;
            string email;
            string password;

            validate.WriteLine();
            validate.WriteLine(Header);
            validate.WriteLine(accent);
            validate.WriteLine();

            validate.WriteLine(NamePrompt);
            name = validate.Read(NameError, UserPrompt, NamePrompt, "Name");
            validate.WriteLine();

            validate.WriteLine(EmailPrompt);
            email = validate.Read(EmailError, UserPrompt, EmailPrompt, "Email");
            validate.WriteLine();

            validate.WriteLine(PasswordPrompt);

            for (int i = 0; i < Conditions.Length; i++)
            {
                Console.WriteLine(Conditions[i]);
            }
            password = validate.Read(PasswordError, UserPrompt, PasswordPrompt, "Password");
            validate.WriteLine();

            this.acct = database.CreateAccount(name, email, password);

            validate.WriteLine(ConfirmMessage, name, email);
            validate.WriteLine();
        }
    }
}
