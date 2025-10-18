using UnityEngine;

public class PlayerInputReader : MonoBehaviour, IInputReader
{
    [Header("Input Axes")]
    [SerializeField]
    private string _horizontalAxis = "Horizontal";

    [SerializeField]
    private string _verticalAxis = "Vertical";

    public Vector2 Move => new Vector2(
        Input.GetAxisRaw(_horizontalAxis),
        Input.GetAxisRaw(_verticalAxis)
    ).normalized;
}