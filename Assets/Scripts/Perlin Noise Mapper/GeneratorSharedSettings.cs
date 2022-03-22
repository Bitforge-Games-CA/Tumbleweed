using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSharedSettings : MonoBehaviour
{
    public static GeneratorSharedSettings current;

    public int MasterSeed;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        MasterSeed = Convert.ToInt32(NewGameMenu.Seed);
    }

}
