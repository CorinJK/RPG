using Scripts.Combat;
using Scripts.Core;
using UnityEngine;

namespace Scripts.Control
{
    public class AIController : MonoBehaviour
    {
        private const string _playerTag = "Player";

        [SerializeField] private Fighter _fighter;
        [SerializeField] private Health _health;
        [SerializeField] private float _chaseDistance = 5f;

        private GameObject _player;

        private void Start() => 
            _player = GameObject.FindWithTag(_playerTag);

        private void Update()
        {
            if (_health.IsDead()) return;

            if (IsAttackToPlayer() && _fighter.CanAttack(_player))
                _fighter.Attack(_player);
            else
                _fighter.Cancel();
        }

        private bool IsAttackToPlayer() =>
            Vector3.Distance(_player.transform.position, transform.position) < _chaseDistance;

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
    }
}