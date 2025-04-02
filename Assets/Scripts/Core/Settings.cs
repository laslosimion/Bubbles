using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public int targetFps = 60;

    private void Awake()
    {
        Application.targetFrameRate = targetFps;
    }
}
