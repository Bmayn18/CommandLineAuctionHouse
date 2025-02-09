using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLineAuctionHouse.Model;

namespace CommandLineAuctionHouse.Views
{
    internal class MainMenu : MenuDisplay
    {
        private const string TITLE = "Main Menu";
        private const string GoodbyeMessage = "| Good bye, thank you for using the Auction House! |";
        private const string exitTitle = "Exit";

        private Database DB;

        public MainMenu(Database database) : base(TITLE, database, true,
            new RegisterDialogue(database), new LogInDialogue(database),
            new ExitDialogue(exitTitle, GoodbyeMessage))
        {
            this.DB = database;
        }
    }
}
