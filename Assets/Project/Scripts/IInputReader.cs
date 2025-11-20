using UnityEngine;

public interface IInputReader
{
    Vector2 Move { get; }
    bool IsShooting { get; }
    bool IsKicking { get; }
}