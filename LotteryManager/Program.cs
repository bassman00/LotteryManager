﻿using System;
using System.Collections.Generic;

namespace LotteryManager
{
    class MainClass
    {
		public static void QuickPick(Random rand, Ticket ticket)
		{
			for (int i = 0; i < 5; i++)
			{
				int whiteBall = rand.Next(1, 70);
				while (!ticket.AddWhiteBall(whiteBall))
					whiteBall = rand.Next(1, 70);
			}
			int redBall = rand.Next(1, 27);
			while (!ticket.AddRedBall(redBall))
				redBall = rand.Next(1, 27);

			//Console.WriteLine(ticket.Show());

		}

        static string totalPlays = "Total  ";
        static string loser = "Loser  ";
        static string pbOnly = "PB Only";
        static string pbPlusOne = "PB + 1 ";
        static string pbPlusTwo = "PB + 2 ";
		static string pbPlusThree = "PB + 3 ";
		static string pbPlusFour = "PB + 4 ";
        static string grandPrize = "Jackpot";
        static string threeWhite = "3 WB   ";
        static string fourWhite = "4 WB   ";
        static string fiveWhite = "5 WB   ";

		public static Dictionary<string, int> WinGrid = new Dictionary<string, int>();



        public static string CompareTickets(Ticket t1, Ticket t2)
        {
            bool winner = false;
            int whiteMatch = 0;
            int redMatch = 0;

            foreach(int number in t1.GetWhiteBalls())
            {
                if(t2.GetWhiteBalls().Contains(number))
                {
                    winner = true;
                    whiteMatch++;
                }                    
            }
            if(t1.GetRedBalls()[0] == t2.GetRedBalls()[0])
            {
                redMatch++;
                winner = true;
            }
            string results = "";

            if (winner)
            {
                if(redMatch > 0)
                {
                    switch(whiteMatch)
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
							WinGrid[grandPrize]++;
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

		public static void Main(string[] args)
        {
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
            WinGrid.Add(grandPrize, 0);

            var PlayerTicket = new Ticket(DateTime.Today);
            PlayerTicket.AddRedBall(13);
            PlayerTicket.AddWhiteBall(3);
            PlayerTicket.AddWhiteBall(8);
            PlayerTicket.AddWhiteBall(10);
            PlayerTicket.AddWhiteBall(38);
            PlayerTicket.AddWhiteBall(41);

			Ticket Ticket = new Ticket(DateTime.Today);
            Random rand = new Random();

            string input = "";
            long ctr = 0;

            while(input.ToUpper() != "Q")
            {
                ctr++;
                WinGrid[totalPlays]++;
                Ticket.Clear();
                //Console.WriteLine(PlayerTicket.Show());
                QuickPick(rand, Ticket);
                CompareTickets(PlayerTicket, Ticket);
                if(ctr % 100000 == 0)
                {
                    DisplayWins();
                }
            }
        }

        private static void DisplayWins()
        {
            Console.Clear();
            string output = "";
            foreach(KeyValuePair<string, int> entry in WinGrid)
            {
                string line = String.Format("{0} - {1,15:n0}\n", entry.Key, entry.Value);
                output += line;
            }
            Console.WriteLine(output);
        }
    }
}