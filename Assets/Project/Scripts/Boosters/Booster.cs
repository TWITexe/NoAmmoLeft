using System;
using UnityEngine;
using System.Collections;
using NTC.Pool;
using static NTC.Pool.NightPool;

namespace Boosters
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Booster : MonoBehaviour, ICollectable, IPoolable
    {
        [SerializeField]
        protected float Duration = 5f;

        private Collider2D _collider;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            OnSpawn();
        }

        protected virtual void OnSpawn()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_collider == null)
                _collider = GetComponent<Collider2D>();

            _spriteRenderer.enabled = true;
            _collider.enabled = true;
            _collider.isTrigger = true;
        }

        public void Collect()
        {
            ApplyEffect();
            Hide();
            StartCoroutine(DeactivateAfterTime());
        }

        private void Hide()
        {
            _collider.enabled = false;
            _spriteRenderer.enabled = false;
        }

        private IEnumerator DeactivateAfterTime()
        {
            yield return new WaitForSeconds(Duration);
            RemoveEffect();
            Despawn(gameObject);
        }

        protected abstract void ApplyEffect();
        protected abstract void RemoveEffect();

        protected abstract void OnTriggerEnter2D(Collider2D other);

        void ISpawnable.OnSpawn()
        {
            OnSpawn();
        }

        public void OnDespawn() { }
    }
}