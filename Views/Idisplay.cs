using CommandLineAuctionHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineAuctionHouse.Views
{
    public interface Idisplay
    {
        public string Title { get; }

        public abstract void Display(); 
    }
}
