using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationSmartSettings : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}

public class PlayerSmartSettings
{
    public static bool isPlainMode = true;
    public static float jumpingTime = 0.18f;
}