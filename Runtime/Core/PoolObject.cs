using System;
using UnityEngine;

namespace Racer.EzPooler.Core
{
    /// <summary>
    /// Represents an object that can be managed by a pool manager.
    /// </summary>
    [DisallowMultipleComponent]
    public class PoolObject : MonoBehaviour
    {
        /// <summary>
        /// Occurs when the object is despawned.
        /// </summary>
        public event Action OnDespawned;

        internal PoolManager PoolManager { get; set; }


        /// <summary>
        /// Invokes the Despawn method after a specified delay.
        /// </summary>
        /// <param name="delay">The delay in seconds before despawning the object.</param>
        public virtual void InvokeDespawn(float delay)
        {
            Invoke(nameof(Despawn), delay);
        }

        /// <summary>
        /// Despawns the object by calling the DespawnObject method on the associated PoolManager.
        /// <remarks>
        /// Perform any cleanup operations before despawning the object by overriding this method and calling the base method as well.
        /// </remarks>
        /// </summary>
        public virtual void Despawn()
        {
            PoolManager.DespawnObject(this);
            OnDespawned?.Invoke();
        }
    }
}