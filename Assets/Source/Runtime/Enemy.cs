using System;
using System.Collections;
using System.Collections.Generic;
using Source.Runtime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
