using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Events;

namespace Tumbleweed.Core.Managers
{

    public class TimeManager : MonoBehaviour
    {
        public static TimeManager current;

        public Light2D Sun2D;
        public Text DateTimeUI;
        public Text HourTimeUI;

        public float TimeScale = 12.0f;
        public float Timer;
        public int TimerInt;
        bool isInt;

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


        public event EventHandler OnTickShort; // 50 times a second
        public event EventHandler OnTickHour; // every 12 seconds
        public event EventHandler OnTickDay; // every 288 seconds or 4.8 minutes
        public event EventHandler OnTickMonth; // roughly every 144 minutes
        public event EventHandler OnTickYear; // roughly every 1728 minutes

        public event EventHandler OnTimeScaleChanged;

        // Start is called before the first frame update
        void Start()
        {
            // instatiate the singleton
            if (current == null)
            {
                current = this;
            }
            else if (current != this)
            {
                Destroy(current);
            }

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
            if (PausedTime == false)
            {
                Timer -= Time.deltaTime;

                // short tick
                OnTickShort?.Invoke(this, EventArgs.Empty);

                // day cycle
                if (Timer <= 0 && HourDay < 12)
                {
                    HourDay++;
                    OnTickHour?.Invoke(this, EventArgs.Empty);
                    AdjustLightDay(HourDay);
                    HourTimeUI.text = $"{HourDay} hr";
                    if (HourDay >= 6)
                    {
                        IsNight = false;
                    }
                    Timer = TimeScale;
                }

                // night cycle
                if (Timer <= 0 && HourDay >= 12)
                {
                    HourNight++;
                    OnTickHour?.Invoke(this, EventArgs.Empty);
                    HourTimeUI.text = $"{HourNight} hr";
                    if (HourNight >= 6)
                    {
                        IsNight = true;
                        AdjustLightNight(HourNight);
                    }
                    if (HourNight == 12 && PausedTime == false)
                    {
                        Day++;
                        OnTickDay?.Invoke(this, EventArgs.Empty);
                        HourNight = 0;
                        HourDay = 0;
                        DateTimeUI.text = $"{MonthList[Month]} {Day}, {Year}";
                    }

                    Timer = TimeScale;
                }

                if (!DateTime.IsLeapYear(Year))
                {
                    MonthReg currentMonth = new MonthReg(MonthList[Month], NumDaysListReg[Month]);

                    // year cycle
                    if (Timer <= 0 && currentMonth.MonthNameReg == "Dec" && Day > 31)
                    {
                        Year++;
                        OnTickYear?.Invoke(this, EventArgs.Empty);
                        Month = 0;
                        DateTimeUI.text = $"{MonthList[Month]} {Day}, {Year}";
                    }

                    // month cycle normal
                    if (Timer <= 0 && Day > currentMonth.DaysInMonthReg)
                    {
                        Month++;
                        OnTickMonth?.Invoke(this, EventArgs.Empty);
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
                        OnTickYear?.Invoke(this, EventArgs.Empty);
                        Month = 0;
                        DateTimeUI.text = $"{MonthList[Month]} {Day}, {Year}";
                    }

                    // month cycle leap
                    if (Timer <= 0 &&  Day > currentMonth.DaysInMonthLeap)
                    {
                        Month++;
                        OnTickMonth?.Invoke(this, EventArgs.Empty);
                        Day = 1;
                        DateTimeUI.text = $"{MonthList[Month]} {Day}, {Year}";
                    }
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
            Timer = RescaleValue(0, TimeScale, 0, 12, Timer);
            TimeScale = 12.0f;

            TimeScaleEventArgs args = new TimeScaleEventArgs();
            args.TimeScale = TimeScale;
            OnTimeScaleChanged?.Invoke(this, args);
        }

        public void FFW1()
        {
            PausedTime = false;
            Timer = RescaleValue(0, TimeScale, 0, 6, Timer);
            TimeScale = 6.0f;

            TimeScaleEventArgs args = new TimeScaleEventArgs();
            args.TimeScale = TimeScale;
            OnTimeScaleChanged?.Invoke(this, args);
        }

        public void FFW2()
        {
            PausedTime = false;
            Timer = RescaleValue(0, TimeScale, 0, 3, Timer);
            TimeScale = 3.0f;

            TimeScaleEventArgs args = new TimeScaleEventArgs();
            args.TimeScale = TimeScale;
            OnTimeScaleChanged?.Invoke(this, args);
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

        public float RescaleValue(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
        {

            float OldRange = OldMax - OldMin;
            float NewRange = NewMax - NewMin;
            float NewValue = ((OldValue - OldMin) * NewRange / OldRange) + NewMin;

            return NewValue;
        }

    }

    public class TimeScaleEventArgs : EventArgs
    {
        public float TimeScale;
    }
}