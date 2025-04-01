using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Bubble _bubble;
    [SerializeField] private Level[] _levels;

    public Bubble GetBubble(Vector2 position, Transform parent, Color color, int pointsDeMultiplier)
    {
        var instance = Instantiate(_bubble, parent);
        instance.transform.localPosition = position;

        var bubble = instance.GetComponent<Bubble>();
        bubble.SetPointsDemultiplier(pointsDeMultiplier);
        bubble.SetDefaultColor(color);
        bubble.Initialize();
        
        return bubble;
    }

    public Level GetLevel(int current)
    {
        var instance = Instantiate(_levels[current]);
        instance.transform.position = Vector3.zero;

        var component = instance.GetComponent<Level>();
        component.Initialize();
        
        return component;
    }
}
