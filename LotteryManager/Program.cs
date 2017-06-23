using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LotteryManager
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var pbs = new PowerballSimulator();
			var p1 = new PowerballTicket(DateTime.Today);
            p1.AddTicketNumbers(3,8,10,38,41,13);
            pbs.AddPlayerTicket(p1);

			//var p2 = new PowerballTicket(DateTime.Today);
			//p2.AddTicketNumbers(3, 8, 9, 24, 27, 11);
			//pbs.AddPlayerTicket(p2);

			//var p3 = new PowerballTicket(DateTime.Today);
			//p3.AddTicketNumbers(9, 24, 29, 40, 49, 5);
			//pbs.AddPlayerTicket(p3);

            //var p4 = new PowerballTicket(DateTime.Today);
			//p4.AddTicketNumbers(8, 10, 13, 28, 35, 26);
			//pbs.AddPlayerTicket(p4);

			pbs.Run();
        }
    }
}
