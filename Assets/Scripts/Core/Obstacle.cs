using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    private const int CollisionsSaved = 5;
    private const int CollisionsCleanTrigger = 10;
    private const int CurrencyReward = 1;
    
    [SerializeField] private ObstacleInfo _info;
    
    private float _speed; 
    private Vector2 _moveDirection;

    private bool _initialized;
    private Vector2 _previousCollisionPoint;
    private readonly List<float> _distancesBetweenCollisions = new();
    
    public void Initialize()
    {
        _speed = Random.Range(_info.minSpeed, _info.maxSpeed);

        _moveDirection = Random.insideUnitCircle.normalized;

        _initialized = true;

        _previousCollisionPoint = transform.position;
    }
    
    private void Update()
    {
        if (!_initialized)
            return;
        
        transform.Translate(_moveDirection * (_speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var distance = Vector2.Distance(_previousCollisionPoint, transform.position);
        _distancesBetweenCollisions.Add(distance);

        _previousCollisionPoint = transform.position;

        CheckDistances();
        
        var bubble = other.gameObject.GetComponent<Bubble>();
        if (bubble)
        {
            if (!bubble.IsDragged)
                _moveDirection = new Vector2(-_moveDirection.x, -_moveDirection.y);
            
            return;
        }
        ReflectDirection(other.gameObject);
    }

    private void CheckDistances()
    {
        if (_distancesBetweenCollisions.Count < CollisionsSaved)
            return;

        var shouldDestroyObject = false;
        for (var i = _distancesBetweenCollisions.Count - 1; i > _distancesBetweenCollisions.Count - CollisionsSaved; i--)
        {
            shouldDestroyObject = _distancesBetweenCollisions[i] < 1;
        }

        if (_distancesBetweenCollisions.Count > CollisionsCleanTrigger)
            _distancesBetweenCollisions.RemoveRange(0, CollisionsSaved);

        if (!shouldDestroyObject)
            return;
        
        Main.Instance.PointsHandler.IncreaseCurrency(CurrencyReward);
        
        _distancesBetweenCollisions.Clear();
        
        gameObject.SetActive(false);
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

    private void OnDestroy()
    {
        _initialized = false;
        
        _distancesBetweenCollisions.Clear();
    }
}
