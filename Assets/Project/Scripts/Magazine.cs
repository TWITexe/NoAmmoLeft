using System;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int AmountAmmo { get; private set; }

    public event Action<int> AmmoChanged;

    public void AddAmmo(int amount)
    {
        if (amount <= 0) return;

        AmountAmmo += amount;
        AmmoChanged?.Invoke(AmountAmmo);
    }

    public void RemoveAmmo(int amount)
    {
        if (amount <= 0) return;

        if (AmountAmmo <= 0)
        {
            // TODO: Play sound
            // TODO: View
            return;
        }

        AmountAmmo -= amount;
        AmmoChanged?.Invoke(AmountAmmo);
    }
}