using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _minMoveToAnimate = 0f;

    [SerializeField]
    private float _rotationSpeed = 10f;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        this.ValidateSerializedFields();
    }

    public void UpdateAnimation(Vector2 moveDirection)
    {
        bool isMoving = moveDirection.sqrMagnitude > _minMoveToAnimate;
        _animator.SetBool(IsMoving, isMoving);

        if (isMoving == false) return;

        RotateTowards(moveDirection);
    }

    private void RotateTowards(Vector2 moveDirection)
    {
        float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        Quaternion targetRot = Quaternion.Euler(0f, 0f, targetAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * _rotationSpeed);
    }
}