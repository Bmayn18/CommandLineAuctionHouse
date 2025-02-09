using CommandLineAuctionHouse.Model;
using CommandLineAuctionHouse.Views;

namespace CommandLineAuctionHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileName = "Database.txt";

            Database database = new Database(FileName);
            MainMenu menu = new MainMenu(database);
            menu.Display();
            database.Save();
        }
    }
}
