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

    public float timeScale = 12.0f;
    public float timer;

    public int hourDay = 8;
    public int hourNight = 0;
    public int day = 1;

    public int month = 0;
    public int year = 1852;

    public List<String> monthList = new List<string>();

    public bool pausedTime;
    public bool isNight;

    // Start is called before the first frame update
    void Start()
    {
        pausedTime = false;
        BuildMonthList();
        dateTimeUI = transform.Find("DateText").GetComponent<Text>();
        hourTimeUI = transform.Find("HourText").GetComponent<Text>();
        sun2D = GameObject.FindWithTag("Sun").GetComponent<Light2D>();
        timer = timeScale;
        dateTimeUI.text = $"{monthList[month]} {day}, {year}";
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        // day cycle
        if (timer <= 0 && pausedTime == false && hourDay < 12)
        {
            hourDay++;
            AdjustLightDay(hourDay);
            hourTimeUI.text = $"{hourDay} hr";
            
            timer = timeScale;
        }


        // night cycle
        if (timer <= 0 && pausedTime == false && hourDay >= 12)
        {
            isNight = true;
            hourNight++;
            AdjustLightNight(hourNight);
            hourTimeUI.text = $"{hourNight} hr";

            if (hourNight == 12 && pausedTime == false)
            {
                day++;
                dateTimeUI.text = $"{monthList[month]} {day}, {year}";
                hourNight = 0;
                hourDay = 0;
                isNight = false;
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

        } else if (pausedTime == false)
        {
            pausedTime = true;
        }

    }

    public void ResumeGameTimer()
    {
        pausedTime = false;
        timeScale = 12.0f;
    }

    public void FFW1()
    {
        pausedTime = false;
        timeScale = 9.0f;
    }

    public void FFW2()
    {
        pausedTime = false;
        timeScale = 6.0f;
    }

    public void AdjustLightDay(int time)
    {
        float normalizedFloat = Mathf.InverseLerp(-6, 12, time);
        sun2D.intensity = normalizedFloat;
        
    }

    public void AdjustLightNight(int time)
    {
        float normalizedFloat = Mathf.InverseLerp(18, 1, time);
        sun2D.intensity = normalizedFloat;
    }

}
