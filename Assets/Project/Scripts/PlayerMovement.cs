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
    private Health _playerHealth;
    private KickAndStun _kickAndStun;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _kickAndStun = GetComponent<KickAndStun>();
        _playerHealth = GetComponent<Health>();
        this.ValidateSerializedFields();

        MoveSpeed = _startMoveSpeed;
    }

    public void Move(Vector2 input)
    {
        if (_playerHealth.IsAlive && !_kickAndStun.IsStun)
            _moveInput = input;
    }

    private void FixedUpdate()
    {
        if ( _playerHealth.IsAlive && !_kickAndStun.IsStun)
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