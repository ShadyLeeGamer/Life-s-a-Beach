using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{
    public float Hp { get; set; }

    public event Action OnKill;

    public void Kill()
    {
    }

    public void TakeDamage(float amount)
    {
    }
}
