using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthReg
{
    public string MonthNameReg;
    public int DaysInMonthReg;

    public MonthReg(string monthName, int daysInMonth)
    {
        this.MonthNameReg = monthName;
        this.DaysInMonthReg = daysInMonth;
    }
}
