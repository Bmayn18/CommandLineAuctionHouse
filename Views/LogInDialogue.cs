using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class LogInDialogue : Dialogue
    {
        private const string TITLE = "Sign In";
        private const string accent = "-------";
        private const string EmailPrompt = "Please enter your email address";
        private const string PasswordPrompt = "Please enter your password";
        private const string EmailError = "        The supplied value is not a valid email address.";
        private const string PasswordError = "         The supplied value is not a valid password";
        private const string UserPrompt = "> ";


        /// <summary>
        /// A class with inherits from the UserInterface class, designed to specifically validate inputs surrounding the login.
        /// </summary>
        private LogInValidate validate;

        /// <summary>
        /// Constructor to pull the database into this file
        /// </summary>
        /// <param name="database">The Database, which handles moving and providing the data to the rest of the program</param>
        public LogInDialogue(Database database) : base(TITLE, database)
        {
            validate = new LogInValidate(database);
        }
        /// <summary>
        /// The Display method which provides the interace for the user to log in
        /// </summary>
        public override void Display()
        {
            validate.WriteLine();
            validate.WriteLine(Title);
            validate.WriteLine(accent);
            validate.WriteLine();

            validate.WriteLine(EmailPrompt);
            Account acct = validate.Read(UserPrompt);
            validate.WriteLine();

            if (acct != null)
            {
                validate.WriteLine(PasswordPrompt);
                if (validate.Read(PasswordPrompt, UserPrompt, acct))
                {
                    validate.WriteLine();
                    MenuDisplay client = new ClientMenu(database, acct);
                    client.Display();
                } else
                {
                    validate.WriteLine(PasswordError);
                    validate.WriteLine();
                }
            } else
            {
                validate.WriteLine(EmailError);
                validate.WriteLine();
            }

            
        }
    }
}
