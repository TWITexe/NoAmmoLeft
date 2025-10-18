using UnityEngine;

namespace Boosters
{
    public class RapidBulletsBooster : Booster
    {
        [SerializeField,Min(0.1f)]
        private float _fireRateMultiplier = 1.5f;

        private Gun _gun;

        protected override void ApplyEffect()
        {
            _gun.SetShootsPerSecond(_gun.StartShootsPerSecond * _fireRateMultiplier);
        }

        protected override void RemoveEffect()
        {
            _gun.SetShootsPerSecond(_gun.StartShootsPerSecond);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                _gun = player.GetComponentInChildren<Gun>();
                Collect();
            }
        }
    }
}