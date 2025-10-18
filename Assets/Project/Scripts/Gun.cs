using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [Header("Gun Settings")]
    [SerializeField]
    private Projectile _projectilePrefab;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _shootsPerSecond = 5f;

    private float _cooldown;

    public bool CanShoot => _cooldown <= 0f;

    private void Awake()
    {
        this.ValidateSerializedFields();
    }

    private void Update()
    {
        if (_cooldown > 0f)
            _cooldown -= Time.deltaTime;
    }

    public void Shoot(Vector2 direction)
    {
        if (CanShoot == false) return;

        Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        Projectile projectile = Instantiate(_projectilePrefab, _firePoint.position, rotation);
        projectile.Launch(direction.normalized);

        _cooldown = 1f / _shootsPerSecond;
    }
}