using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleInfo _info;
    
    private float _speed; 
    private Vector2 _moveDirection;

    private bool _initialized;
    
    public void Initialize()
    {
        _speed = Random.Range(_info.minSpeed, _info.maxSpeed);

        _moveDirection = Random.insideUnitCircle.normalized;

        _initialized = true;
    }
    
    private void Update()
    {
        if (!_initialized)
            return;
        
        transform.Translate(_moveDirection * (_speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var bubble = other.gameObject.GetComponent<Bubble>();
        if (bubble)
        {
            if (!bubble.IsDragged)
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

    private void OnDestroy()
    {
        _initialized = false;
    }
}
