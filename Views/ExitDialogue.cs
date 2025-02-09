using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    internal class ExitDialogue : Idisplay
    {
        public string Title { get; }

        private string accent = "+--------------------------------------------------+";

        private string exitmessage;

        private UserInterface write;

        public ExitDialogue(string title, string exitmessage)
        {
            write = new UserInterface();
            Title = title;
            this.exitmessage = exitmessage;
        }

        public void Display()
        {
            if (!string.IsNullOrEmpty(exitmessage))
            {
                write.WriteLine(accent);
                write.WriteLine(exitmessage);
                write.WriteLine(accent);
            } else
            {
                write.WriteLine();
            }
        }
    }
}
