using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _startMoveSpeed = 5f;

    public float MoveSpeed { get; private set; }

    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    public Vector2 CurrentVelocity => _rb.linearVelocity;
    public float StartMoveSpeed => _startMoveSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        this.ValidateSerializedFields();

        MoveSpeed = _startMoveSpeed;
    }

    public void Move(Vector2 input)
    {
        _moveInput = input;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput * MoveSpeed;
    }

    public void SetSpeed(float speed)
    {
        if (speed <= 0f)
        {
            Debug.LogError("Speed multiplier must be greater than 0.");
            return;
        }

        MoveSpeed = speed;
    }
}