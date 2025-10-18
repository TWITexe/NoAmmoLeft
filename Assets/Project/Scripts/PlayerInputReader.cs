using System;
using UnityEngine;

public class PlayerInputReader : MonoBehaviour, IInputReader
{
    [Header("Input Axes")]
    [SerializeField]
    private string _horizontalAxis = "Horizontal";

    [SerializeField]
    private string _verticalAxis = "Vertical";

    [Header("Fire Button")]
    [SerializeField]
    private KeyCode _fireButton = KeyCode.Space;

    public bool IsShooting => Input.GetKey(_fireButton);

    private void Awake()
    {
        this.ValidateSerializedFields();
    }

    public Vector2 Move => new Vector2(
        Input.GetAxisRaw(_horizontalAxis),
        Input.GetAxisRaw(_verticalAxis)
    ).normalized;
}