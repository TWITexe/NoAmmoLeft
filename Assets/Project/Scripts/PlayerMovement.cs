using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    public Vector2 CurrentVelocity => _rb.linearVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        this.ValidateSerializedFields();
    }

    public void Move(Vector2 input)
    {
        _moveInput = input;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput * _moveSpeed;
    }
}