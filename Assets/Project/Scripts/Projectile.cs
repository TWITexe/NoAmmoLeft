using UnityEngine;
using static NTC.Pool.NightPool;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;

    [SerializeField]
    private float _lifetime = 2f;

    [SerializeField]
    private int _damage = 1;

    [SerializeField]
    private Rigidbody2D _rb;

    private void Awake()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();

        this.ValidateSerializedFields();
    }

    public void Launch(Vector2 direction)
    {
        _rb.linearVelocity = direction * _speed;
        Despawn(gameObject, _lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.ApplyDamage(_damage);
            Despawn(gameObject);
        }
    }
}