using UnityEngine;

public interface IWeapon
{
    void Shoot(Vector2 direction);
    bool CanShoot { get; }
}