using UnityEngine;
using static NTC.Pool.NightPool;

public class Gun : MonoBehaviour, IWeapon
{
    [Header("Gun Settings")]
    [SerializeField]
    private Projectile _projectilePrefab;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private Magazine _magazine;

    [SerializeField]
    private float _shootsPerSecond = 5f;

    public float ShootsPerSecond
    {
        get => _shootsPerSecond;
        set => _shootsPerSecond = value;
    }

    [SerializeField]
    private float _damage = 1f;
    public float DamageMultiplier { get; set; } = 1f;

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
        if (_magazine.AmountAmmo == 0) return;
        
        _magazine.RemoveAmmo(1);

        Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        Projectile projectile = Spawn(_projectilePrefab, _firePoint.position, rotation);
        projectile.SetDamage(_damage * DamageMultiplier);
        projectile.Launch(direction.normalized);

        _cooldown = 1f / _shootsPerSecond;
    }
}