using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LotteryManager
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var pbs = new PowerballSimulator(26, 100000);
            pbs.LoadTickets("PowerballNumbers.csv");

            string input = "";
            while (input.ToUpper() != "Q")
            {
                pbs.ClearStats();
                pbs.Run();
                Console.WriteLine("Enter Q <ENTER> to quit or <ENTER> to rerun.");
                input = Console.ReadLine();
            }
        }
    }
}
