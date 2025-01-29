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
            _rb2D.velocity = Vector2.up * 3f;
        }

        private void Update()
        {
            if (transform.position.y > 5.5f)
                Despawn();
        }

        public override void Despawn()
        {
            base.Despawn();
            _rb2D.velocity = Vector2.zero;
        }
    }
}