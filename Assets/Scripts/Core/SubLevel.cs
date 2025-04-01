using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubLevel : MonoBehaviour
{
    private const float EnableTopBorderDelay = 5;
    
    [SerializeField] private Obstacle[] _obstacles;
    [SerializeField] private Border _topBorder;
    
    public void Initialize()
    {
        foreach (var item in _obstacles)
        {
            item.Initialize();
        }
    }

    public void DisableTopBorder()
    {
        _topBorder.gameObject.SetActive(false);
        Invoke(nameof(EnableTopBorder), EnableTopBorderDelay);
    }

    public void EnableTopBorder()
    {
        _topBorder.gameObject.SetActive(true);
    }

    public void DestroyObstacles()
    {
        foreach (var item in _obstacles)
        {
            Destroy(item.gameObject);
        }
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(EnableTopBorder));
    }
}
