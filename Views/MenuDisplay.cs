using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class MenuDisplay : Dialogue
    {
        private string WelcomeAccent = "+------------------------------+";
        private string accent = "---------";
        private string WelcomeMessage = "| Welcome to the Auction House |";
        private string prompt = "> ";
        private string OptionPrompt = "Please select an option between {0} and {1}";

        private bool showWelcome = true;
        private bool IsLoggedIn = false;

        private MenuValidate validate { get; }

        private Idisplay[] DisplayOptions;

        private Account acct;
        /// <summary>
        /// Constructor for MenuDisplay, exclusively used by MainMenu
        /// </summary>
        /// <param name="Title">A Unique Name given to each of the classes that implements IDisplay</param>
        /// <param name="database">The Database, which handles moving and providing the data to the rest of the program</param>
        /// <param name="newsession">A boolean that determines whether the welcome Accent and welcome Message should be displayed</param>
        /// <param name="options">A list of dialogue objects that are used to determine what to display</param>
        public MenuDisplay(string Title, Database database, bool newsession, params Idisplay[] options) : base(Title, database)
        {
            validate = new MenuValidate();
            this.showWelcome = newsession;
            this.DisplayOptions = new Idisplay[options.Length];
            Array.Copy(options, this.DisplayOptions, options.Length);
        }
        /// <summary>
        /// Alternate constructor used to determine whether address should be grabbed or not, exclusively used by ClientMenu
        /// </summary>
        /// <param name="Title">A unique name given to a class that implements IDisplay</param>
        /// <param name="database">The database, which provides the rest of the program with data such as accounts and products.</param>
        /// <param name="account"> The account the user is currently using in their session</param>
        /// <param name="newsession">A boolean that descides whether the address is to be asked for or not.</param>
        /// <param name="IsLoggedIn">A boolean to Determine whether the user is logged in or not</param>
        /// <param name="options">A list of dialogue objects that are used to determine what to display</param>
        public MenuDisplay(string Title, Database database, Account account, bool newsession, bool IsLoggedIn, params Idisplay[] options) : base(Title, database)
        {
            validate = new MenuValidate();
            this.showWelcome = newsession;
            this.IsLoggedIn = IsLoggedIn;
            this.acct = account;
            this.DisplayOptions = new Idisplay[options.Length];
            Array.Copy(options, this.DisplayOptions, options.Length);
        }

        /// <summary>
        /// The Primary method classes emplementing IDisplay and extending Dialogue will use to provide the user with a user interface.
        /// </summary>
        public override void Display()
        {
            while(true)
            {
                Idisplay UserChoice;

                if (showWelcome == true)
                {
                    validate.WriteLine(WelcomeAccent);
                    validate.WriteLine(WelcomeMessage);
                    validate.WriteLine(WelcomeAccent);
                    validate.WriteLine();
                    showWelcome = false;
                } else if (IsLoggedIn == true)
                {
                    if (acct.Address == null)
                    {
                        GetAddressDialogue getaddress = new GetAddressDialogue(database, acct);
                        getaddress.Display();
                    }
                }

                validate.WriteLine(Title);
                validate.WriteLine(accent);

                for (int i = 0; i < DisplayOptions.Length; i++)
                {
                    validate.WriteLine("({0}) {1}", i + 1, DisplayOptions[i].Title);
                }

                validate.WriteLine();
                validate.WriteLine(OptionPrompt, 1, DisplayOptions.Length);
                GetOptions(out UserChoice);

                UserChoice.Display();

                if (UserChoice is ExitDialogue) break;
            }
        }
        /// <summary>
        /// A function to get an input from the user. This uses a child class of the UserInterface class.
        /// </summary>
        /// <param name="option">A list of dialogue objects that are used to determine what to display</param>
        private void GetOptions(out Idisplay option)
        {
            int opt = validate.Read(OptionPrompt, prompt, 1, DisplayOptions.Length);
            option = DisplayOptions[opt - 1];
        }
    }
}
