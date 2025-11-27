using System;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using static NTC.Pool.NightPool;

public class Gun : MonoBehaviour, IWeapon
{
    [Header("Gun Settings")]
    [SerializeField]
    private Projectile _projectilePrefab;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Magazine _magazine;

    [SerializeField]
    private UI.Timer _timer;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _startDamage = 1f;

    [SerializeField]
    private float _startShootsPerSecond = 5f;

    [SerializeField]
    private Health _health;

    private float _cooldown;

    private bool _isEnabled = false;

    public event Action NoAmmoLeft;

    public float Damage { get; private set; }
    public float StartDamage => _startDamage;

    public float ShootsPerSecond { get; private set; }
    public float StartShootsPerSecond => _startShootsPerSecond;

    public bool CanShoot => _cooldown <= 0f;

    public bool IsEnabled => _isEnabled;

    private void Awake()
    {
        this.ValidateSerializedFields();

        Damage = _startDamage;
        ShootsPerSecond = _startShootsPerSecond;

        _timer.OnTimerStart += TurnOff;
        _timer.OnTimerEnd += TurnOn;
    }

    private void OnDestroy()
    {
        _timer.OnTimerEnd -= TurnOff;
        _timer.OnTimerStart -= TurnOff;
    }

    private void Update()
    {
        if (_cooldown > 0f)
            _cooldown -= Time.deltaTime;
    }

    public void Shoot(Vector2 direction)
    {
        if (!CanShoot || !_isEnabled || !_health.IsAlive)
            return;

        if (_magazine.AmountAmmo <= 0)
            return;

        Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        Projectile projectile = Spawn(_projectilePrefab, _firePoint.position, rotation);
        projectile.Launch(direction.normalized, Damage);
        _magazine.RemoveAmmo(1);

        _cooldown = 1f / ShootsPerSecond;

        if (_magazine.AmountAmmo <= 0 || !_health.IsAlive)
        {
            NoAmmoLeft?.Invoke();
            TurnOff();
        }
           
            
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

    private void TurnOff()
    {
        SetEnable(false);
    }

    private void TurnOn()
    {
        SetEnable(true);
        if (_magazine.AmountAmmo <= 0)
            NoAmmoLeft?.Invoke();
    }

    private void SetEnable(bool value)
    {
        _isEnabled = value;
        _spriteRenderer.enabled = value;
    }
}