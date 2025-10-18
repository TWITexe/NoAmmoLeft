using UnityEngine;
using static NTC.Pool.NightPool;

[RequireComponent(typeof(Collider2D))]
public class CollectableAmmo : MonoBehaviour, ICollectable
{
    [SerializeField]
    private int _amount = 1;

    public void Collect()
    {
        Despawn(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Magazine magazine))
        {
            magazine.AddAmmo(_amount);
            Collect();
        }
    }
}