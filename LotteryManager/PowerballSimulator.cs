using System;
using System.Collections.Generic;
using System.IO;

namespace LotteryManager
{
    public static class PowerballLimits
    {
        private static int _whiteBallCount = 5;
        private static int _whiteBallMin = 1;
        private static int _whiteBallMax = 69;
        public static int WhiteBallCount { get { return _whiteBallCount; } }
        public static int WhiteBallMin { get { return _whiteBallMin; } }
        public static int WhiteBallMax { get { return _whiteBallMax; } }

        private static int _redBallCount = 1;
        private static int _redBallMin = 1;
        private static int _redBallMax = 26;
        public static int RedBallCount { get { return _redBallCount; } }
        public static int RedBallMin { get { return _redBallMin; } }
        public static int RedBallMax { get { return _redBallMax; } }

    }


    public class PowerballSimulator
    {
        static private string totalDraws = "Drawings";
        static private string totalPlays = "Total   ";
        static private string loser = "Loser   ";
        static private string pbOnly = "PB Only ";
        static private string pbPlusOne = "PB + 1  ";
        static private string pbPlusTwo = "PB + 2  ";
        static private string pbPlusThree = "PB + 3  ";
        static private string pbPlusFour = "PB + 4  ";
        static private string jackpot = "Jackpot ";
        static private string threeWhite = "3 WB    ";
        static private string fourWhite = "4 WB    ";
        static private string fiveWhite = "5 WB    ";

        private readonly List<string> statKeys = new List<string>
        {
            totalDraws, totalPlays, loser, pbOnly, pbPlusOne, pbPlusTwo,
            threeWhite, pbPlusThree, fourWhite, pbPlusFour, fiveWhite, jackpot
        };

        private readonly Dictionary<string, long> WinGrid = new Dictionary<string, long>();
        private readonly Dictionary<string, double> PrizeList = new Dictionary<string, double>();
        private readonly Dictionary<int, long> WhiteNumberFrequency = new Dictionary<int, long>();
		private readonly Dictionary<int, long> RedNumberFrequency = new Dictionary<int, long>();

        private Random rand;

		public List<Ticket> PlayerTicketList = new List<Ticket>();

        public long NumberOfDraws { get; set; }
        public long DisplayFrequency { get; set; }

        public PowerballSimulator(long draws, long displayFrequency)
        {
			rand = new Random();
			NumberOfDraws = draws;
            DisplayFrequency = draws > 0 && displayFrequency > draws ? draws : displayFrequency;

            PrizeList.Add(totalDraws, 0);
			PrizeList.Add(totalPlays, -2.0);
			PrizeList.Add(loser, 0.0);
			PrizeList.Add(pbOnly, 4.0);
			PrizeList.Add(pbPlusOne, 4.0);
			PrizeList.Add(pbPlusTwo, 7.0);
			PrizeList.Add(threeWhite, 7.0);
			PrizeList.Add(pbPlusThree, 100.0);
			PrizeList.Add(fourWhite, 100.0);
			PrizeList.Add(pbPlusFour, 50000.0);
			PrizeList.Add(fiveWhite, 1000000.00);
			PrizeList.Add(jackpot, 20000000.00);

            foreach (string stat in statKeys)
                WinGrid.Add(stat, 0);

            for (int i = 1; i < PowerballLimits.WhiteBallMax + 1; i++)
            {
                WhiteNumberFrequency.Add(i, 0);
            }
            for (int i = 1; i < PowerballLimits.RedBallMax + 1; i++)
            {
                RedNumberFrequency.Add(i, 0);
            }
		}

        public void LoadTickets(string fpath)
        {
            var ticketStream = new StreamReader(fpath);
            while(!ticketStream.EndOfStream)
            {
                string line = ticketStream.ReadLine();
                line = line.Trim();
                if(line.Length > 0 && line[0] != '#') //skip lines starting with #
                {
                    var ticket = new PowerballTicket(line);
                    AddPlayerTicket(ticket);
                }
            }
        }

        public void ClearStats()
        {
            foreach (string stat in statKeys)
                WinGrid[stat] = 0;
        }

        public int AddPlayerTicket(Ticket pticket)
        {
            PlayerTicketList.Add(pticket);
            return PlayerTicketList.Count;
        }

        public void Run()
        {
			var Ticket = new PowerballTicket(DateTime.Today);

			string input = "";

            while (input.ToUpper() != "Q")
			{
				//Console.WriteLine(PlayerTicket.Show());
				if (NumberOfDraws > 0 && WinGrid[totalDraws] == NumberOfDraws)
					break;

				WinGrid[totalDraws]++;
				QuickPick(rand, Ticket);
                foreach (Ticket PlayerTicket in PlayerTicketList)
                {
					CompareTickets(PlayerTicket, Ticket);
                }
				if (WinGrid[totalPlays] % DisplayFrequency == 0)
                {
                    DisplayScreen();
                    if(Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Q)
                            input = "Q";
                        else
                        {
                            if (key.Key == ConsoleKey.P)
                            {
                                while (!Console.KeyAvailable) {}

                                Console.ReadKey(true);
                            }
                        }
                    }
                }
			}
            DisplayScreen();
		}

		public void QuickPick(Random rand, Ticket ticket)
		{
            ticket.Clear();
			for (int i = 0; i < PowerballLimits.WhiteBallCount; i++)
			{
                int whiteBall = rand.Next(PowerballLimits.WhiteBallMin, PowerballLimits.WhiteBallMax + 1);
                while (!ticket.AddWhiteBall(whiteBall))
                {
                    whiteBall = rand.Next(PowerballLimits.WhiteBallMin, PowerballLimits.WhiteBallMax + 1);
                }
				WhiteNumberFrequency[whiteBall]++;
			}
            for (int i = 0; i < PowerballLimits.RedBallCount; i++)
            {
                int redBall = rand.Next(PowerballLimits.RedBallMin, PowerballLimits.RedBallMax + 1);
                while (!ticket.AddRedBall(redBall))
                {
                    redBall = rand.Next(PowerballLimits.RedBallMin, PowerballLimits.RedBallMax + 1);
                }
				RedNumberFrequency[redBall]++;
			}
   		}

		public string CompareTickets(Ticket t1, Ticket t2)
		{
			bool winner = false;
			int whiteMatch = 0;
			int redMatch = 0;

			WinGrid[totalPlays]++; //=PlayerTicketList.Count;

            foreach (int number in t1.GetWhiteBalls())
			{
				if (t2.GetWhiteBalls().Contains(number))
				{
					winner = true;
					whiteMatch++;
				}
			}
            foreach (int number in t1.GetRedBalls())
            {
                if (t2.GetRedBalls().Contains(number))
                {
                    redMatch++;
                    winner = true;
                }
            }
			string results = "";

			if (winner)
			{
				if (redMatch > 0)
				{
					switch (whiteMatch)
					{
						case 0:
							WinGrid[pbOnly]++;
							break;

						case 1:
							WinGrid[pbPlusOne]++;
							break;

						case 2:
							WinGrid[pbPlusTwo]++;
							break;

						case 3:
							WinGrid[pbPlusThree]++;
							break;

						case 4:
							WinGrid[pbPlusFour]++;
							break;

						case 5:
							WinGrid[jackpot]++;
							break;

					};
				}
				else
				{
					switch (whiteMatch)
					{
						case 3:
							WinGrid[threeWhite]++;
							break;

						case 4:
							WinGrid[fourWhite]++;
							break;

						case 5:
							WinGrid[fiveWhite]++;
							break;

                        default:
                            WinGrid[loser]++;
                            break;
					};
				}
				results = String.Format("Winner!  Matched {0} white and {1} red", whiteMatch, redMatch);
			}
			else
            {
				WinGrid[loser]++;
				results = "Loser!";
			}

			return results;
		}

		private void DisplayWins()
		{
			string output = "";
			double netWinnings = 0;
			foreach (KeyValuePair<string, long> entry in WinGrid)
			{
				double dollars = entry.Value * PrizeList[entry.Key];
				netWinnings += dollars;
				string line = String.Format("{0} - {1,15:n0} - {2,20:c0}\n",
											entry.Key, entry.Value, dollars);
				output += line;
			}
			Console.WriteLine(output);
            string totalLine = String.Format("Total 'Winnings': {0,20:c0}\nTotal Net: {1,28:c0}",
											 netWinnings + (WinGrid[totalPlays] * -PrizeList[totalPlays]),
											 netWinnings);
			Console.WriteLine(totalLine);
		}

        public void DisplayScreen()
        {
			Console.Clear();
			foreach (Ticket PlayerTicket in PlayerTicketList)
				Console.WriteLine(PlayerTicket.Show());

			Console.WriteLine();
			DisplayWins();
		}

	}
}
