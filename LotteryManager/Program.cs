using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LotteryManager
{
    class MainClass
    {
        public class ProgramArgs
        {
            public long Draws { get; set; }
            public long DisplayFrequency { get; set; }
            public string NumberFilePath { get; set; }
        };

        public static void Main(string[] args)
        {
            var Args = CheckArgs(args);
            var pbs = new PowerballSimulator(Args.Draws, Args.DisplayFrequency);
            try
            {
                pbs.LoadTickets(Args.NumberFilePath);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
                return;
            }
            string input = "";
            while (input.ToUpper() != "Q")
            {
                pbs.ClearStats();
                pbs.Run();
                Console.WriteLine("Enter Q <ENTER> to quit or <ENTER> to rerun.");
                input = Console.ReadLine();
            }
        }

        public static ProgramArgs CheckArgs(string[] args)
        {
            //Default params
            var Args = new ProgramArgs
            {
                Draws = 0,
                DisplayFrequency = 100000,
                NumberFilePath = "PowerballNumbers.csv"
            };

            if (args.Length > 0)
            {
                Args.Draws = Convert.ToInt64(args[0]);
                if (args.Length > 1)
                {
                    Args.DisplayFrequency = Convert.ToInt64(args[1]);
                    if (args.Length > 2)
                    {
                        Args.NumberFilePath = args[2];
                    }
                }
            }

            return Args;
        }
    }
}
