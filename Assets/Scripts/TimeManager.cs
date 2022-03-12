using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeManager : MonoBehaviour
{
    public Light2D sun2D;
    public Text dateTimeUI;
    public Text hourTimeUI;

    public static Action onHourChanged;
    public static Action onDayChanged;
    public static Action onMonthChanged;

    public float timeScale = 2.5f;
    public float timer;

    public int hourDay = 8;
    public int day = 1;

    public int month = 0;
    public int year = 1852;

    public List<String> monthList = new List<string>();

    public bool pausedTime;

    // Start is called before the first frame update
    void Start()
    {
        pausedTime = false;
        BuildMonthList();
        dateTimeUI = transform.Find("DateText").GetComponent<Text>();
        hourTimeUI = transform.Find("HourText").GetComponent<Text>();
        sun2D = transform.Find("Global Light 2D").GetComponent<Light2D>();
        timer = timeScale;
        dateTimeUI.text = $"{monthList[month]} {day}, {year}";
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        // day cycle
        if (timer <= 0 && pausedTime == false)
        {
            hourDay++;
            AdjustLightDay(hourDay);
            hourTimeUI.text = $"{hourDay} hr";


            if (hourDay >= 24 && pausedTime == false)
            {
                day++;
                dateTimeUI.text = $"{monthList[month]} {day}, {year}";
                hourDay = 1;
            }
            
            timer = timeScale;
        }

    }

    void BuildMonthList()
    {
        monthList.Add("Jan");
        monthList.Add("Feb");
        monthList.Add("March");
        monthList.Add("April");
        monthList.Add("May");
        monthList.Add("June");
        monthList.Add("July");
        monthList.Add("Aug");
        monthList.Add("Sept");
        monthList.Add("Oct");
        monthList.Add("Nov");
        monthList.Add("Dec");
    }

    public void PauseGameTimer()
    {
        if (pausedTime == true)
        {
            pausedTime = false;

        } else
        {
            pausedTime = true;
        }

    }

    public void ResumeGameTimer()
    {
        pausedTime = false;
        timeScale = 2.5f;
    }

    public void FFW1()
    {
        timeScale = 1.75f;
    }

    public void FFW2()
    {
        timeScale = 1.0f;
    }

    // (rangeMax - rangeMin) * value + rangeMin
    public void AdjustLightDay(int time)
    {
  
        
    }

}