using Scripts.Control;
using Scripts.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace Scripts.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private const string _playerTag = "Player";
        [SerializeField] private PlayableDirector _playableDirector;
        private GameObject _player;

        private void Start()
        {
            _player = GameObject.FindWithTag(_playerTag);
            _playableDirector.played += DisableControl;
            _playableDirector.stopped += EnableControl;
        }

        private void DisableControl(PlayableDirector playableDirector)
        {
            _player.GetComponent<ActionScheduler>().CancelCurrentAction();
            _player.GetComponent<PlayerController>().enabled = false;
        }

        private void EnableControl(PlayableDirector playableDirector)
        {
            _player.GetComponent<PlayerController>().enabled = true;
        }
    }
}