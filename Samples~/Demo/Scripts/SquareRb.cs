using Racer.EzPooler.Core;
using UnityEngine;

namespace Racer.EzPooler.Samples
{
    internal class SquareRb : PoolObject
    {
        private Rigidbody2D _rb2D;

        
        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
#if UNITY_6000_0_OR_NEWER
            _rb2D.linearVelocity = Vector2.up * 3f;
#else
            _rb2D.velocity = Vector2.up * 3f;
#endif
        }

        private void Update()
        {
            if (transform.position.y > 5.5f)
                Despawn();
        }

        public override void Despawn()
        {
            base.Despawn();

#if UNITY_6000_0_OR_NEWER
            _rb2D.linearVelocity = Vector2.zero;
#else
            _rb2D.velocity = Vector2.zero;
#endif
        }
    }
}