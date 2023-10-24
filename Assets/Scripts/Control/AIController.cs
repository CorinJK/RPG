using Scripts.Combat;
using Scripts.Core;
using Scripts.Movement;
using UnityEngine;

namespace Scripts.Control
{
    public class AIController : MonoBehaviour
    {
        private const string _playerTag = "Player";

        [SerializeField] private Fighter _fighter;
        [SerializeField] private Health _health;
        [SerializeField] private Mover _mover;
        [SerializeField] private ActionScheduler _actionScheduler;
        [SerializeField] private PatrolPath _patrolPath;

        [SerializeField] private float _chaseDistance = 5f;
        [SerializeField] private float _suspicionTime = 5f;
        [SerializeField] private float _waypointTolerance = 1f;
        [SerializeField] private float _waypointDwellTime = 3f;

        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int _currentWaypointIndex = 0;

        private Vector3 _guardPosition;
        private GameObject _player;

        private void Start()
        {
            _player = GameObject.FindWithTag(_playerTag);
            _guardPosition = transform.position;
        }

        private void Update()
        {
            if (_health.IsDead()) return;

            if (IsAttackToPlayer() && _fighter.CanAttack(_player))
                AttackBehaviour();
            else if (_timeSinceLastSawPlayer < _suspicionTime)
                SuspicionBehaviour();
            else
                PatrolBehaviour();

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            _timeSinceLastSawPlayer = 0;
            _fighter.Attack(_player);
        }

        private void SuspicionBehaviour() =>
            _actionScheduler.CancelCurrentAcion();

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = _guardPosition;

            if (_patrolPath != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (_timeSinceArrivedAtWaypoint > _waypointDwellTime)
                _mover.StartMoveAction(nextPosition);
            else
                SuspicionBehaviour();
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < _waypointTolerance;
        }

        private void CycleWaypoint() =>
            _currentWaypointIndex = _patrolPath.GetNextIndex(_currentWaypointIndex);

        private Vector3 GetCurrentWaypoint() =>
            _patrolPath.GetWaypoint(_currentWaypointIndex);

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