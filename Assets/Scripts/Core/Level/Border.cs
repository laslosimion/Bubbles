using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Border : MonoBehaviour
{
    public enum Direction 
    {
        Left,
        Right,
        Bottom,
        Top
    }
    public Direction direction;
}
