namespace Boosters
{
    public class RapidBulletsBooster : Booster
    {
        [UnityEngine.SerializeField]
        private float _fireRateMultiplier = 1.5f;

        protected override void ApplyEffect()
        {
            Player.Gun.ShootsPerSecond *= _fireRateMultiplier;
        }

        protected override void RemoveEffect()
        {
            Player.Gun.ShootsPerSecond /= _fireRateMultiplier;
        }
    }
}