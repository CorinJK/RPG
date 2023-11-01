using UnityEngine;

namespace Scripts.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        static bool _hasSpawned = false;

        [SerializeField] private GameObject _persistentObjectPrefab;

        private void Awake()
        {
            if (_hasSpawned) return;

            SpawnPersistentObjects();

            _hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(_persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}