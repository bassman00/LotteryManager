using System;
using System.Collections.Generic;

namespace LotteryManager
{
    public class Ticket
    {
        private List<int> _whiteBalls = new List<int>();
        private List<int> _redBalls = new List<int>();
        private int _multiplier = 1;

        public DateTime TicketDate { get; set; }

        public Ticket()
        {           
        }

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
            var whiteBallList = String.Join(", ", _whiteBalls);
            var redBallList = String.Join(", ", _redBalls);

            string retval = String.Format("{0} - (w){1}, (r){2}", TicketDate.ToShortDateString(), 
                                          whiteBallList, redBallList);

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

        public int GetMultiplier() { return _multiplier; }
        public void SetMultiplier(int multiplier)
        {
            if ((multiplier >= 2 && multiplier <= 5) || multiplier == 10)
                _multiplier = multiplier;
        }
    }
}