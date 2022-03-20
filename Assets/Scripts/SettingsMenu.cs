using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // settings
    public int DefaultFrameRate = -1; // -1 = unlimited (best perfomance)

    // settings methods

    public void SetFrameRateDefault()
    {
        Application.targetFrameRate = DefaultFrameRate;
    }

    public void SetFrameRate30()
    {
        Application.targetFrameRate = 30;
    }

    public void SetFrameRate60()
    {
        Application.targetFrameRate = 60;
    }

    public void SetFrameRate90()
    {
        Application.targetFrameRate = 90;
    }

    public void SetFrameRate120()
    {
        Application.targetFrameRate = 120;
    }

}
