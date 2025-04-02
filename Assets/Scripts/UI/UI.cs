using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UI : MonoBehaviour
{
    [SerializeField] private GameObject ProgresstUI;
    [SerializeField] private GameObject LevelSelectUI;
    [SerializeField] private GameObject LevelWonUI;
    [SerializeField] private GameObject LevelLostUI;

    public void HideAll()
    {
        HideLevelUI();
        HideLevelSelect();
        HideLevelWon();
        HideLevelLost();
    }

    public void ShowLevelUI()
    {
        HideAll();
        ProgresstUI.gameObject.SetActive(true);
    }
    
    public void HideLevelUI()
    {
        ProgresstUI.gameObject.SetActive(false);
    }
    
    public void ShowLevelSelect()
    {
        HideAll();
        LevelSelectUI.gameObject.SetActive(true);
    }
    
    public void HideLevelSelect()
    {
        LevelSelectUI.gameObject.SetActive(false);
    }
    
    public void ShowLevelWon()
    {
        HideAll();
        LevelWonUI.gameObject.SetActive(true);
    }
    
    public void HideLevelWon()
    {
        LevelWonUI.gameObject.SetActive(false);
    }
    
    public void ShowLevelLost()
    {
        HideAll();
        LevelLostUI.gameObject.SetActive(true);
    }
    
    public void HideLevelLost()
    {
        LevelLostUI.gameObject.SetActive(false);
    }
}
