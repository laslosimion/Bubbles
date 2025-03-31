using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Bubble _bubble;
    [SerializeField] private Level[] _levels;

    public static Factory Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public Bubble GetBubble(Vector2 position, Transform parent, Color color)
    {
        var instance = Instantiate(_bubble, parent);
        instance.transform.localPosition = position;

        var bubble = instance.GetComponent<Bubble>();
        bubble.Initialize();
        bubble.SetColor(color);
        
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
