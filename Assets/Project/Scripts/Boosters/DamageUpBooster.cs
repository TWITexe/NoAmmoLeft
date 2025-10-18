using UnityEngine;

namespace Boosters
{
    public class DamageUpBooster : Booster
    {
        [SerializeField]
        private float _damageMultiplier = 2f;

        private Gun _gun;

        protected override void ApplyEffect()
        {
            float damage = _gun.Damage * _damageMultiplier;
            _gun.SetDamage(damage);
        }

        protected override void RemoveEffect()
        {
            _gun.SetDamage(_gun.StartDamage);
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