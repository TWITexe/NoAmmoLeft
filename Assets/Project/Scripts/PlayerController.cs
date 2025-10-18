using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(IInputReader))]
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerController : MonoBehaviour
{
    private IInputReader _input;
    private PlayerMovement _movement;
    private PlayerAnimation _animation;

    private void Awake()
    {
        _input = GetComponent<IInputReader>();
        _movement = GetComponent<PlayerMovement>();
        _animation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        Vector2 move = _input.Move;
        _movement.Move(move);
        _animation.UpdateAnimation(move);
    }
}