using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(IInputReader))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Gun _weapon;

    private IInputReader _input;
    private PlayerMovement _movement;
    private PlayerAnimation _animation;
    private KickAndStun _kickAndStun;
    private Health _playerHealth;

    private IWeapon Weapon => _weapon;

    private void Awake()
    {
        _input = GetComponent<IInputReader>();
        _movement = GetComponent<PlayerMovement>();
        _animation = GetComponent<PlayerAnimation>();
        _kickAndStun = GetComponent<KickAndStun>();
        _playerHealth = GetComponent<Health>();

        this.ValidateSerializedFields();
    }

    private void Update()
    {

        Vector2 move = _input.Move;
        if (_playerHealth.IsAlive && !_kickAndStun.IsStun)
        {
            _movement.Move(move);
            _animation.UpdateAnimation(move);
        }

        Debug.Log(_weapon.IsEnabled);

        if (_input.IsShooting)
        {
            Vector2 shootDirection = transform.right;
            Weapon.Shoot(shootDirection);
        }
        if (_input.IsKicking && _weapon.IsEnabled == false)
        {
            _kickAndStun.Kick();
        }
    }
}