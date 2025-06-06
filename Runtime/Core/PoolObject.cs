using System;
using UnityEngine;

namespace Racer.EzPooler.Core
{
    /// <summary>
    /// Base class for objects that can be pooled and reused.
    /// </summary>
    [DisallowMultipleComponent]
    public class PoolObject : MonoBehaviour
    {
        /// <summary>
        /// Event that is invoked when the object is despawned.
        /// </summary>
        public event Action OnDespawned;

        internal PoolManager PoolManager { get; set; }


        /// <summary>
        /// Invokes <see cref="Despawn"/> at specified delay.
        /// </summary>
        /// <param name="delay">The delay in seconds before despawning the object.</param>
        public virtual void InvokeDespawn(float delay)
        {
            Invoke(nameof(Despawn), delay);
        }

        /// <summary>
        /// Despawns the <see cref="PoolManager"/>'s object, returning it to the pool for reuse.
        /// <remarks>
        /// Override this method to perform any necessary cleanup operations before the object is despawned.
        /// </remarks>
        /// </summary>
        public virtual void Despawn()
        {
            PoolManager.DespawnObject(this);
            OnDespawned?.Invoke();
        }
    }
}