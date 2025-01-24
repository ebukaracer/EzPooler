using Racer.EzPooler.Core;
using UnityEngine;

namespace Racer.EzPooler.Samples
{
    internal class SquareSpawner : MonoBehaviour
    {
        private PoolManager _poolManager;

        
        private void Awake()
        {
            _poolManager = GetComponent<PoolManager>();
        }

        public void Spawn()
        {
            _poolManager.SpawnObject(new Vector3(Random.Range(-5, 5), -4.5f, 0f));
        }
    }
}