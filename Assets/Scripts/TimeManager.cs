using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeManager : MonoBehaviour
{
    public Light2D Sun2D;
    public Text DateTimeUI;
    public Text HourTimeUI;

    public static Action OnHourChanged;
    public static Action OnDayChanged;
    public static Action OnMonthChanged;

    public float TimeScale = 12.0f;
    public float Timer;

    public int HourDay = 8;
    public int HourNight = 0;
    public int Day = 1;

    public int Month = 0;
    public int Year = 1852;

    public List<String> MonthList = new List<string>();

    public List<int> NumDaysListReg = new List<int>();
    public List<int> NumDaysListLeap = new List<int>();

    public bool PausedTime;
    public bool IsNight;

    // Start is called before the first frame update
    void Start()
    {
        PausedTime = false;
        BuildMonthList();
        BuildDaysInMonthRegList();
        BuildDaysInMonthLeapList();
        DateTimeUI = transform.Find("DateText").GetComponent<Text>();
        HourTimeUI = transform.Find("HourText").GetComponent<Text>();
        Sun2D = GameObject.FindWithTag("Sun").GetComponent<Light2D>();
        Timer = TimeScale;
        DateTimeUI.text = $"{MonthList[Month]} {Day}, {Year}";
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;

        // day cycle
        if (Timer <= 0 && PausedTime == false && HourDay < 12)
        {
            HourDay++;
            AdjustLightDay(HourDay);
            HourTimeUI.text = $"{HourDay} hr";
            
            Timer = TimeScale;
        }


        // night cycle
        if (Timer <= 0 && PausedTime == false && HourDay >= 12)
        {
            IsNight = true;
            HourNight++;
            AdjustLightNight(HourNight);
            HourTimeUI.text = $"{HourNight} hr";

            if (HourNight == 12 && PausedTime == false)
            {
                Day++;
                HourNight = 0;
                HourDay = 0;
                DateTimeUI.text = $"{MonthList[Month]} {Day}, {Year}";
                IsNight = false;
            }

            Timer = TimeScale;
        }

        if (!DateTime.IsLeapYear(Year))
        {
            MonthReg currentMonth = new MonthReg(MonthList[Month], NumDaysListReg[Month]);

            // year cycle
            if (Timer <= 0 && PausedTime == false && currentMonth.MonthNameReg == "Dec" && Day > 31)
            {
                Year++;
                Month = 1;
                DateTimeUI.text = $"{MonthList[Month]} {Day}, {Year}";
            }

            // month cycle normal
            if (Timer <= 0 && PausedTime == false && Day > currentMonth.DaysInMonthReg)
            {
                Month++;
                Day = 1;
                DateTimeUI.text = $"{MonthList[Month]} {Day}, {Year}";
            }

        } 
        else
        {
            MonthLeap currentMonth = new MonthLeap(MonthList[Month], NumDaysListLeap[Month] );

            // year cycle
            if (Timer <= 0 && PausedTime == false && currentMonth.MonthNameLeap == "Dec" && Day > 31)
            {
                Year++;
                Month = 1;
                DateTimeUI.text = $"{MonthList[Month]} {Day}, {Year}";
            }

            // month cycle leap
            if (Timer <= 0 && PausedTime == false && Day > currentMonth.DaysInMonthLeap)
            {
                Month++;
                Day = 1;
                DateTimeUI.text = $"{MonthList[Month]} {Day}, {Year}";
            }
        }


    }

    void BuildMonthList()
    {
        MonthList.Add("Jan");
        MonthList.Add("Feb");
        MonthList.Add("March");
        MonthList.Add("April");
        MonthList.Add("May");
        MonthList.Add("June");
        MonthList.Add("July");
        MonthList.Add("Aug");
        MonthList.Add("Sept");
        MonthList.Add("Oct");
        MonthList.Add("Nov");
        MonthList.Add("Dec");
    }

    void BuildDaysInMonthRegList()
    {
        NumDaysListReg.Add(31);
        NumDaysListReg.Add(28);
        NumDaysListReg.Add(31);
        NumDaysListReg.Add(30);
        NumDaysListReg.Add(31);
        NumDaysListReg.Add(30);
        NumDaysListReg.Add(31);
        NumDaysListReg.Add(31);
        NumDaysListReg.Add(30);
        NumDaysListReg.Add(31);
        NumDaysListReg.Add(30);
        NumDaysListReg.Add(31);
    }

    void BuildDaysInMonthLeapList()
    {
        NumDaysListLeap.Add(31);
        NumDaysListLeap.Add(29);
        NumDaysListLeap.Add(31);
        NumDaysListLeap.Add(30);
        NumDaysListLeap.Add(31);
        NumDaysListLeap.Add(30);
        NumDaysListLeap.Add(31);
        NumDaysListLeap.Add(31);
        NumDaysListLeap.Add(30);
        NumDaysListLeap.Add(31);
        NumDaysListLeap.Add(30);
        NumDaysListLeap.Add(31);
    }

    public void PauseGameTimer()
    {
        if (PausedTime == true)
        {
            PausedTime = false;

        } else if (PausedTime == false)
        {
            PausedTime = true;
        }

    }

    public void ResumeGameTimer()
    {
        PausedTime = false;
        TimeScale = 12.0f;
    }

    public void FFW1()
    {
        PausedTime = false;
        TimeScale = 9.0f;
    }

    public void FFW2()
    {
        PausedTime = false;
        TimeScale = 6.0f;
    }

    public void AdjustLightDay(int time)
    {
        float normalizedFloat = Mathf.InverseLerp(-6, 12, time);
        Sun2D.intensity = normalizedFloat;
        
    }

    public void AdjustLightNight(int time)
    {
        float normalizedFloat = Mathf.InverseLerp(18, 1, time);
        Sun2D.intensity = normalizedFloat;
    }

}
