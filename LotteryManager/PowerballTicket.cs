using System;
namespace LotteryManager
{
	public class PowerballTicket : Ticket
	{
		public PowerballTicket(DateTime date) : base(date)
		{
			TicketDate = DateTime.Today;
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

		public PowerballTicket(string csFileData) : base(DateTime.Today)
		{

			string[] fields = csFileData.Split(',');
			for (int i = 0; i < fields.Length; i++)
			{
				switch (i)
				{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
						AddWhiteBall(Convert.ToInt16(fields[i]));
						break;

					case 5:
						AddRedBall(Convert.ToInt16(fields[i]));
						break;
				}
			}
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
}
