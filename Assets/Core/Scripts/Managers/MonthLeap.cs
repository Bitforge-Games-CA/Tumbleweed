using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tumbleweed.Core.Managers
{

    public class MonthLeap
    {
        public string MonthNameLeap;
        public int DaysInMonthLeap;

        public MonthLeap(string monthName, int daysInMonth)
        {
            this.MonthNameLeap = monthName;
            this.DaysInMonthLeap = daysInMonth;
        }
    }

}
