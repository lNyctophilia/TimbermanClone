using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSUnlocker : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        QualitySettings.SetQualityLevel(1);
    }
}
