using UnityEngine;
using UnityEngine.Playables;

namespace Scripts.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private const string _playerTag = "Player";

        [SerializeField] private PlayableDirector _playableDirector;
        private bool _alreadyTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!_alreadyTriggered && other.gameObject.tag == _playerTag)
            {
                _alreadyTriggered = true;
                _playableDirector.Play();
            }
        }
    }
}