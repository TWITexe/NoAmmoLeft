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

    private IWeapon Weapon => _weapon;

    private void Awake()
    {
        _input = GetComponent<IInputReader>();
        _movement = GetComponent<PlayerMovement>();
        _animation = GetComponent<PlayerAnimation>();

        this.ValidateSerializedFields();
    }

    private void Update()
    {
        Vector2 move = _input.Move;
        _movement.Move(move);
        _animation.UpdateAnimation(move);

        if (_input.IsShooting)
        {
            Vector2 shootDirection = transform.right;
            Weapon.Shoot(shootDirection);
        }
    }
}