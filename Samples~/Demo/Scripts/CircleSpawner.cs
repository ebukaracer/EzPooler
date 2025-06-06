using System.Collections;
using Racer.EzPooler.Core;
using UnityEngine;

namespace Racer.EzPooler.Samples
{
    internal class CircleSpawner : MonoBehaviour
    {
        private PoolManager _poolManager;
        private WaitForSeconds _wait;

        [SerializeField] private float nextSpawnDuration = 1f;


        private void Start()
        {
            _poolManager = GetComponent<PoolManager>();
            _wait = new WaitForSeconds(nextSpawnDuration);

            StartCoroutine(RandomSpawning());
        }

        private IEnumerator RandomSpawning()
        {
            while (true)
            {
                yield return _wait;

                var po = _poolManager.SpawnObject(new Vector3(Random.Range(-3f, 3f), Random.Range(-4f, 4f), 0f),
                    Quaternion.identity);

                po.InvokeDespawn(1.5f);
            }
        }
    }
}