using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

namespace Tumbleweed.Core.Managers
{

    public class TimeManager : MonoBehaviour
    {
        public Light2D Sun2D;
        public Text DateTimeUI;
        public Text HourTimeUI;

        public float TimeScale = 12.0f;
        public float Timer;

        public int HourDay = 8;
        public int HourNight = 0;
        public int Day = 1;

        public int Month = 0;
        public int Year = 1852;

        public static List<string> MonthList = new List<string>() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public static List<int> NumDaysListReg = new List<int>() { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public static List<int> NumDaysListLeap = new List<int>() { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public bool PausedTime;
        public bool IsNight;

        // Start is called before the first frame update
        void Start()
        {
            PausedTime = false;
            DateTimeUI = GameObject.Find("UIMainGame/DateText").GetComponent<Text>();
            HourTimeUI = GameObject.Find("UIMainGame/HourText").GetComponent<Text>();
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
                    Month = 0;
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
                MonthLeap currentMonth = new MonthLeap(MonthList[Month], NumDaysListLeap[Month]);

                // year cycle
                if (Timer <= 0 && PausedTime == false && currentMonth.MonthNameLeap == "Dec" && Day > 31)
                {
                    Year++;
                    Month = 0;
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

        public void PauseGameTimer()
        {
            if (PausedTime == true)
            {
                PausedTime = false;

            }
            else if (PausedTime == false)
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

}