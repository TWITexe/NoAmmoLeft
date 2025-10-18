using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int AmountAmmo { get; private set; }

    public void AddAmmo(int amount)
    {
        if (amount <= 0) return;

        AmountAmmo += amount;
    }

    public void RemoveAmmo(int amount)
    {
        if (amount <= 0) return;

        if (AmountAmmo < amount)
        {
            // TODO: Play sound
            // TODO: View
            return;
        }

        AmountAmmo -= amount;
    }
}