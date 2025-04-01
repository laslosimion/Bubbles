using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public sealed class ObstacleInfo : ScriptableObject
{
    public float minSpeed = 5;
    public float maxSpeed = 10;
}
