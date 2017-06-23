using System;
using System.Collections.Generic;

namespace LotteryManager
{
    public class Ticket
    {
        private List<int> _whiteBalls = new List<int>();
        private List<int> _redBalls = new List<int>();

        public DateTime TicketDate { get; private set; }

        public Ticket(DateTime date)
        {
            TicketDate = date;
        }

        public bool AddWhiteBall(int whiteball)
        {
            bool retval = false;
            if (!_whiteBalls.Contains(whiteball))
            {
                _whiteBalls.Add(whiteball);
                _whiteBalls.Sort();
                retval = true;
            }

            return retval;
        }

        public bool AddRedBall(int redball)
        {
            bool retval = false;
            if (!_redBalls.Contains(redball))
            {
                _redBalls.Add(redball);
                _redBalls.Sort();
                retval = true;
            }

            return retval;
        }

        public string Show()
        {
            string wb = "";

            foreach (int ball in _whiteBalls)
            {
                wb += ball.ToString() + ", ";
            }
            string rb = _redBalls[0].ToString();
            string retval = String.Format("{0} - {1}{2}", TicketDate, wb, rb);

            return retval;
        }

        public void Clear()
        {
            _whiteBalls.Clear();
            _redBalls.Clear();
        }

        public List<int> GetWhiteBalls()
        {
            return _whiteBalls;
        }
        public List<int>GetRedBalls()
        {
            return _redBalls;
        }
     }
}