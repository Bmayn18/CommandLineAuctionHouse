using CommandLineAuctionHouse.Model;
using static System.Console;

namespace CommandLineAuctionHouse.Views
{
    public class UserInterface
    {
        public virtual string Read(string errormessage, string UserPrompt)
        {
            string input;

            while(true)
            {
                Write(UserPrompt);
                input = ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    WriteLine(errormessage);
                } else
                {
                    break;
                }
            }

            return input;
        }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteLine(string text, params object[] arguements)
        {
            Console.WriteLine(text, arguements);
        }
    }
}
