using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartSettings : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
}

public class PlayerSmartSettings
{
    public static bool isPlainMode = false;
    public static float jumpingTime = 0.18f;
}