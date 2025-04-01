using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleInfo _info;
    
    private float _speed; 
    private Vector2 _moveDirection;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _speed = Random.Range(_info.minSpeed, _info.maxSpeed);

        _moveDirection = Random.insideUnitCircle.normalized;
    }
    
    private void Update()
    {
        transform.Translate(_moveDirection * (_speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Bubble>())
        {
            _moveDirection = new Vector2(-_moveDirection.x, -_moveDirection.y);
            
            return;
        }
        ReflectDirection(other.gameObject);
    }
    
    private void ReflectDirection(GameObject wall)
    {
        var border = wall.GetComponent<Border>();
        if (border == null)
            return;
        if (border.direction is Border.Direction.Top or Border.Direction.Bottom)
            _moveDirection = new Vector2(_moveDirection.x, -_moveDirection.y);
        else  if (border.direction is Border.Direction.Left or Border.Direction.Right)
            _moveDirection = new Vector2(-_moveDirection.x, _moveDirection.y);
    }
}
