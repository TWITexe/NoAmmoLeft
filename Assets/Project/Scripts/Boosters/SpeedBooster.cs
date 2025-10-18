namespace Boosters
{
    public class SpeedBooster : Booster
    {
        [UnityEngine.SerializeField]
        private float _speedMultiplier = 1.5f;

        protected override void ApplyEffect()
        {
            Player.MovementSpeed *= _speedMultiplier;
        }

        protected override void RemoveEffect()
        {
            Player.MovementSpeed /= _speedMultiplier;
        }
    }
}