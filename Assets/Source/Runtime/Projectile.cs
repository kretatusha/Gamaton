using Source.Runtime;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage();
        Destroy(gameObject);
    }
}
