using UnityEngine;

public class BotShooter : MonoBehaviour
{
    private Transform _shootPoint;
    private Rigidbody2D _bulletPrefab;
    private float _bulletSpeed;
    private Transform _body;

    public void Init(Transform shootPoint, Rigidbody2D bulletPrefab, float bulletSpeed, Transform body)
    {
        _shootPoint = shootPoint;
        _bulletPrefab = bulletPrefab;
        _bulletSpeed = bulletSpeed;
        _body = body;
    }
    
    public void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity);
        bullet.velocity = _body.right * _bulletSpeed;
    }
}
