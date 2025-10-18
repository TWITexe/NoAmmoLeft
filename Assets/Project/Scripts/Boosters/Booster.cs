using UnityEngine;
using static NTC.Pool.NightPool;

namespace Boosters
{
    public abstract class Booster : MonoBehaviour, ICollectable
    {
        [SerializeField]
        protected float Duration = 5f;

        private float _timer;

        protected PlayerController Player;

        public void Activate(PlayerController player)
        {
            Player = player;
            ApplyEffect();
            _timer = Duration;
            StartCoroutine(DeactivateAfterTime());
        }

        private System.Collections.IEnumerator DeactivateAfterTime()
        {
            yield return new WaitForSeconds(Duration);
            RemoveEffect();
        }

        protected abstract void ApplyEffect();
        protected abstract void RemoveEffect();

        public void Collect()
        {
            Despawn(this);
        }
    }
}