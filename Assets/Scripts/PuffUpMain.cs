using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffUpMain : MonoBehaviour
{
    private Level _currentLevel;
    
    private void Start()
    {
        _currentLevel = Factory.Instance.GetLevel(0);
    }
}
