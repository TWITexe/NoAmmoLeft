namespace Boosters
{
    public class DamageUpBooster : Booster
    {
        [UnityEngine.SerializeField]
        private float _damageMultiplier = 2f;

        protected override void ApplyEffect()
        {
            Player.Gun.DamageMultiplier *= _damageMultiplier;
        }

        protected override void RemoveEffect()
        {
            Player.Gun.DamageMultiplier /= _damageMultiplier;
        }
    }
}