using UnityEngine;

namespace Boosters
{
    public class SpeedBooster : Booster
    {
        [SerializeField]
        private float _speedMultiplier = 1.5f;

        private PlayerMovement _movement;

        protected override void ApplyEffect()
        {
            if (_movement == null)
            {
                Debug.LogWarning("SpeedBooster: PlayerMovement not found.");
                return;
            }

            _movement.SetSpeed(_movement.StartMoveSpeed * _speedMultiplier);
        }

        protected override void RemoveEffect()
        {
            if (_movement == null)
            {
                Debug.LogWarning("SpeedBooster: PlayerMovement not found.");
                return;
            }

            _movement.SetSpeed(_movement.StartMoveSpeed);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerMovement player))
            {
                _movement = player.GetComponentInChildren<PlayerMovement>();
                Collect();
            }
        }
    }
}