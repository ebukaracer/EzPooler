using System.Collections.Generic;
using UnityEngine;

namespace Racer.EzPooler.Core
{
    /// <summary>
    /// Manages a pool of reusable objects to optimize performance by reducing the need for frequent instantiation and destruction.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class PoolManager : MonoBehaviour
    {
        /// <summary>
        /// Queue to hold the pool objects.
        /// </summary>
        private readonly Queue<PoolObject> _queue = new();

        /// <summary>
        /// Current count of objects in the pool.
        /// </summary>
        private int _currentObjectsCount;

        /// <summary>
        /// Initial capacity of the pool.
        /// </summary>
        [SerializeField] private int capacity = 5;

        /// <summary>
        /// Flag to determine if the pool should automatically grow when capacity is reached.
        /// </summary>
        [SerializeField] private bool autoGrow;

        /// <summary>
        /// Prefab of the pool object to be instantiated.
        /// </summary>
        [SerializeField] private PoolObject poolObjectPrefab;

        /// <summary>
        /// Queue to hold the pool objects for the editor.
        /// </summary>
        [field: SerializeField]
        internal List<PoolObject> CachedObjects { get; private set; } = new();


        private void Awake()
        {
            // Assign the objects in editor list to the runtime queue.
            foreach (var poolObj in CachedObjects)
            {
                poolObj.PoolManager = this;
                _queue.Enqueue(poolObj);
                _currentObjectsCount++;
            }
        }

        private void GrowQueue()
        {
            var poolObj = Instantiate(poolObjectPrefab, transform);

            if (!poolObj)
            {
                Debug.LogWarning(
                    $"[{poolObj}] does not contain [{nameof(PoolObject)} Component], either add or inherit from it.",
                    poolObj);
                return;
            }

            poolObj.PoolManager = this;

            if (poolObj.gameObject.activeInHierarchy)
                poolObj.gameObject.SetActive(false);

            _queue.Enqueue(poolObj);
            _currentObjectsCount++;
        }

        /// <summary>
        /// Spawns an object from the pool at the prefab's position and rotation.
        /// </summary>
        /// <returns>The spawned pool object.</returns>
        public PoolObject SpawnObject()
        {
            Transform objTransform;
            return SpawnObject((objTransform = poolObjectPrefab.transform).position, objTransform.rotation);
        }

        /// <summary>
        /// Spawns an object from the pool at the specified position.
        /// </summary>
        /// <param name="position">The position to spawn the object at.</param>
        /// <returns>The spawned pool object.</returns>
        public PoolObject SpawnObject(Vector3 position) => SpawnObject(position, poolObjectPrefab.transform.rotation);

        /// <summary>
        /// Spawns an object from the pool at the specified position and rotation.
        /// </summary>
        /// <param name="position">The position to spawn the object at.</param>
        /// <param name="rotation">The rotation to spawn the object with.</param>
        /// <returns>The spawned pool object.</returns>
        public PoolObject SpawnObject(Vector3 position, Quaternion rotation)
        {
            if (_queue.Count == 0)
            {
                if (autoGrow && _currentObjectsCount >= capacity)
                {
                    GrowQueue();
                    capacity++;
                }
                else if (_currentObjectsCount < capacity)
                {
                    GrowQueue();
                }
                else
                {
                    Debug.LogError(
                        $"Pool out of objects, no more game objects available in the [{name}] pool.\n"
                        + "Make sure to increase the [Capacity] or tick the [Auto Grow] check-box in the inspector.\n\n",
                        this);

                    return null;
                }
            }

            var poolObj = _queue.Dequeue();

            poolObj.transform.SetPositionAndRotation(position, rotation);
            poolObj.gameObject.SetActive(true);

            return poolObj;
        }

        /// <summary>
        /// Despawns an object, returning it to the pool.
        /// </summary>
        /// <param name="poolObj">The pool object to despawn.</param>
        public void DespawnObject(PoolObject poolObj)
        {
            if (!poolObj.gameObject.activeInHierarchy)
                return;

            poolObj.gameObject.SetActive(false);

            _queue.Enqueue(poolObj);
        }

        /// <summary>
        /// Despawns an object at specified delay, returning it to the pool.
        /// </summary>
        /// <param name="poolObj">The pool object to despawn.</param>
        /// <param name="delay">The delay in seconds before despawning the object.</param>
        public void DespawnObject(PoolObject poolObj, float delay)
        {
            poolObj.InvokeDespawn(delay);
        }
    }
}