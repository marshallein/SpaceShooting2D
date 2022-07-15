using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class BasicEnemy : ScriptableObject
{
    public int health;

    [Range(0f, 100f)] public float percentage;

    [HideInInspector]public double _weight;
}
