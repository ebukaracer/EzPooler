#if UNITY_EDITOR
using Racer.EzPooler.Core;
using UnityEditor;
using UnityEngine;

namespace Racer.EzPooler.Editor
{
    [CustomEditor(typeof(PoolManager))]
    internal class PoolManagerEditor : UnityEditor.Editor
    {
        private PoolManager _poolManager;
        private SerializedProperty _capacity;
        private SerializedProperty _poolObjectPrefab;


        private void OnEnable()
        {
            _capacity = serializedObject.FindProperty("capacity");
            _poolObjectPrefab = serializedObject.FindProperty("poolObjectPrefab");
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();

            _poolManager = (PoolManager)target;

            GUILayout.Space(10);

            ResolveInvalidReference();

            if (GUILayout.Button(new GUIContent("Pre-Instantiate Objects",
                    tooltip:
                    "Prepares the pool with the specified amount(capacity) of objects that will be used at runtime." +
                    "\nLeaving empty will result to spawning the objects dynamically during play.")))
            {
                ClearPool();
                InstantiatePoolObjects();
            }

            if (GUILayout.Button(new GUIContent("Clear Objects",
                    tooltip: "Clears the pre-instantiated objects.")))
            {
                ClearPool();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void InstantiatePoolObjects()
        {
            var prefab = _poolObjectPrefab.objectReferenceValue;

            if (!prefab)
            {
                Debug.LogError("Prefab field is null. Please assign a valid prefab.", this);
                return;
            }

            for (var i = 0; i < _capacity.intValue; i++)
            {
                var poolObj = PrefabUtility.InstantiatePrefab(prefab, _poolManager.transform) as PoolObject;

                if (!poolObj)
                {
                    Debug.LogWarning(
                        $"[{poolObj}] does not contain [{nameof(PoolObject)} Component], either add it or inherit from it.",
                        poolObj);
                    continue;
                }

                poolObj.gameObject.SetActive(false);
                _poolManager.CachedObjects.Add(poolObj);
            }
        }

        private void ResolveInvalidReference()
        {
            if (!_poolObjectPrefab.objectReferenceValue)
                ClearPool();
        }

        private void ClearPool()
        {
            for (var i = _poolManager.transform.childCount - 1; i >= 0; i--)
            {
                var child = _poolManager.transform.GetChild(i);
                DestroyImmediate(child.gameObject);
            }

            _poolManager.CachedObjects.Clear();
        }
    }
}
#endif