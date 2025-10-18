using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [Header("Gun Settings")]
    [SerializeField]
    private Projectile _projectilePrefab;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _fireRate = 5f; // shoots per second

    private float _cooldown;

    public bool CanShoot => _cooldown <= 0f;
    private bool HasBullet => _projectilePrefab != null && _firePoint != null;

    private void Update()
    {
        if (_cooldown > 0f)
            _cooldown -= Time.deltaTime;
    }

    public void Shoot(Vector2 direction)
    {
        if (!CanShoot || !HasBullet)
            return;

        var projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
        projectile.Launch(direction.normalized);

        _cooldown = 1f / _fireRate;
    }
}