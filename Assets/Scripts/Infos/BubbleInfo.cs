using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Bubble", menuName = "ScriptableObjects/Bubble", order = 1)]
public class BubbleInfo : ScriptableObject
{
   public float increaseScaleSpeed = 0.001f;
   public int increasePointsSpeed = 1;
   public int pointsDeMultiplier = 5;
}
