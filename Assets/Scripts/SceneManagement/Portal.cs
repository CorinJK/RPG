using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Scripts.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        private const string _playerTag = "Player";

        enum DestinationIdentifier
        {
            A, B, C, D
        }

        [SerializeField] private DestinationIdentifier _destination;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _sceneToLoad = -1;
        [SerializeField] private float _fadeOutTime = 0.5f;
        [SerializeField] private float _fadeInTime = 1f;
        [SerializeField] private float _fadeWaitTime = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == _playerTag)
                StartCoroutine(Transition());
        }

        private IEnumerator Transition()
        {
            if (_sceneToLoad < 0) 
            {
                Debug.LogError("Scene to load not set");
                yield break; 
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            //yield return fader.FadeOut(_fadeOutTime);
            yield return SceneManager.LoadSceneAsync(_sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            //yield return new WaitForSeconds(_fadeWaitTime);
            //yield return fader.FadeIn(_fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag(_playerTag);
            player.GetComponent<NavMeshAgent>().Warp(otherPortal._spawnPoint.position);
            player.transform.rotation = otherPortal._spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal._destination != _destination) continue;
                return portal;
            }
            return null;
        }
    }
}