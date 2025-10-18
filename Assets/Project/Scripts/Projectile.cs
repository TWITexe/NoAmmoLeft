using UnityEngine;

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
        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Health>()?.ApplyDamage(_damage);
        Destroy(gameObject);
    }
}