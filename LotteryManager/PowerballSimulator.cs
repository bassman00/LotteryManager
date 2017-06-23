using System;
using System.Collections.Generic;

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

    public class PowerballTicket : Ticket
    {
        public PowerballTicket(DateTime date) : base(date)
        {
            
        }

        public PowerballTicket(DateTime date, int w1, int w2, int w3, int w4, int w5, int r1) 
            : base(date)
        {
            AddWhiteBall(w1);
			AddWhiteBall(w2);
			AddWhiteBall(w3);
			AddWhiteBall(w4);
			AddWhiteBall(w5);
			AddRedBall(r1);
		}

        public void AddTicketNumbers(int w1, int w2, int w3, int w4, int w5, int r1)
		{
			AddWhiteBall(w1);
			AddWhiteBall(w2);
			AddWhiteBall(w3);
			AddWhiteBall(w4);
			AddWhiteBall(w5);
			AddRedBall(r1);
		}
	}

    public class PowerballSimulator
    {
		private string totalPlays = "Total  ";
		private string loser = "Loser  ";
		private string pbOnly = "PB Only";
		private string pbPlusOne = "PB + 1 ";
		private string pbPlusTwo = "PB + 2 ";
		private string pbPlusThree = "PB + 3 ";
		private string pbPlusFour = "PB + 4 ";
		private string jackpot = "Jackpot";
		private string threeWhite = "3 WB   ";
		private string fourWhite = "4 WB   ";
		private string fiveWhite = "5 WB   ";

        private readonly Dictionary<string, long> WinGrid = new Dictionary<string, long>();
        private readonly Dictionary<string, double> PrizeList = new Dictionary<string, double>();

        public List<Ticket> PlayerTicketList = new List<Ticket>();

        public PowerballSimulator()
        {
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

			WinGrid.Add(totalPlays, 0);
			WinGrid.Add(loser, 0);
			WinGrid.Add(pbOnly, 0);
			WinGrid.Add(pbPlusOne, 0);
			WinGrid.Add(pbPlusTwo, 0);
			WinGrid.Add(threeWhite, 0);
			WinGrid.Add(pbPlusThree, 0);
			WinGrid.Add(fourWhite, 0);
			WinGrid.Add(pbPlusFour, 0);
			WinGrid.Add(fiveWhite, 0);
			WinGrid.Add(jackpot, 0);
		}

        public int AddPlayerTicket(Ticket pticket)
        {
            PlayerTicketList.Add(pticket);
            return PlayerTicketList.Count;
        }

        public void Run()
        {
			var Ticket = new PowerballTicket(DateTime.Today);
			Random rand = new Random();

			string input = "";

			while (input.ToUpper() != "Q")
			{
				WinGrid[totalPlays]++;
				//Console.WriteLine(PlayerTicket.Show());
				QuickPick(rand, Ticket);
                foreach(Ticket PlayerTicket in PlayerTicketList)
				    CompareTickets(PlayerTicket, Ticket);
				if (WinGrid[totalPlays] % 100000 == 0)
                {
					Console.Clear();
					foreach (Ticket PlayerTicket in PlayerTicketList)
                        Console.WriteLine(PlayerTicket.Show());

                    Console.WriteLine();
                    DisplayWins();
				}
			}
		}

		public void QuickPick(Random rand, Ticket ticket)
		{
            ticket.Clear();
			for (int i = 0; i < PowerballLimits.WhiteBallCount; i++)
			{
                int whiteBall = rand.Next(PowerballLimits.WhiteBallMin, PowerballLimits.WhiteBallMax + 1);
                while (!ticket.AddWhiteBall(whiteBall))
                    whiteBall = rand.Next(PowerballLimits.WhiteBallMin, PowerballLimits.WhiteBallMax + 1);
			}
            for (int i = 0; i < PowerballLimits.RedBallCount; i++)
            {
                int redBall = rand.Next(PowerballLimits.RedBallMin, PowerballLimits.RedBallMax + 1);
                while (!ticket.AddRedBall(redBall))
                    redBall = rand.Next(PowerballLimits.RedBallMin, PowerballLimits.RedBallMax + 1);
            }
   		}

		public string CompareTickets(Ticket t1, Ticket t2)
		{
			bool winner = false;
			int whiteMatch = 0;
			int redMatch = 0;

			foreach (int number in t1.GetWhiteBalls())
			{
				if (t2.GetWhiteBalls().Contains(number))
				{
					winner = true;
					whiteMatch++;
				}
			}
			if (t1.GetRedBalls()[0] == t2.GetRedBalls()[0])
			{
				redMatch++;
				winner = true;
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
				string line = String.Format("{0} - {1,15:n0} - {2,22:c}\n",
											entry.Key, entry.Value, dollars);
				output += line;
			}
			Console.WriteLine(output);
            string totalLine = String.Format("Total 'Winnings': {0,22:c}\nTotal Net: {1,30:c}",
											 netWinnings + (WinGrid[totalPlays] * -PrizeList[totalPlays]),
											 netWinnings);
			Console.WriteLine(totalLine);
		}

	}
}
