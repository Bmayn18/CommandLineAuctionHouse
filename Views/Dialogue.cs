using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    public abstract class Dialogue : Idisplay
    {
        public string Title { get; }

        public Database database { get; }

        public Dialogue(string title, Database database)
        {
            Title = title;
            this.database = database;
        }

        public abstract void Display();
    }
}
