using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [Header("Gun Settings")]
    [SerializeField]
    private Projectile _projectilePrefab;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _startDamage = 1f;

    [SerializeField]
    private float _startShootsPerSecond = 5f;

    private float _cooldown;

    public float Damage { get; private set; }
    public float StartDamage => _startDamage;

    public float ShootsPerSecond { get; private set; }
    public float StartShootsPerSecond => _startShootsPerSecond;

    public bool CanShoot => _cooldown <= 0f;

    private void Awake()
    {
        Damage = _startDamage;
        ShootsPerSecond = _startShootsPerSecond;
        
        this.ValidateSerializedFields();
    }

    private void Update()
    {
        if (_cooldown > 0f)
            _cooldown -= Time.deltaTime;
    }

    public void Shoot(Vector2 direction)
    {
        if (!CanShoot)
            return;

        Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        Projectile projectile = Instantiate(_projectilePrefab, _firePoint.position, rotation);
        projectile.Launch(direction.normalized, Damage);

        _cooldown = 1f / ShootsPerSecond;
    }

    public void SetDamage(float damage)
    {
        if (damage <= 0f)
        {
            Debug.LogError("Damage must be greater than 0.");
            return;
        }

        Damage = damage;
    }

    public void SetShootsPerSecond(float shootsPerSecond)
    {
        if (shootsPerSecond <= 0f)
        {
            Debug.LogError("Shoots per second must be greater than 0.");
            return;
        }

        ShootsPerSecond = shootsPerSecond;
    }
}