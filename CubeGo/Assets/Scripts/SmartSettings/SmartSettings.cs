using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartSetter : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
    }
}


namespace SmartSettings
{
    public class Data
    {
        public static bool isPlainMode = false;
        public static float jumpingTime = 0.2f;
        public static float shakeDelta = 0.15f; 
        public static float shakeTime = 0.16f;
    }
}
