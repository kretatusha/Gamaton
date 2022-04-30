using System;
using Source.Runtime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    public void TakeDamage()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.TryGetComponent(out Bot bot))
            bot.TakeDamage();
    }
}
