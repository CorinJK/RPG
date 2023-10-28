using Scripts.Combat;
using Scripts.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private const string _forwardSpeed = "forwardSpeed";

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private Fighter _fighter;
        [SerializeField] private ActionScheduler _actionScheduler;
        [SerializeField] private Health _health;

        [SerializeField] private float _maxSpeed = 6f;

        private Vector3 _velocity;
        private Vector3 _localVelocity;
        private float _speed;

        private void Update()
        {
            _agent.enabled = !_health.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            _actionScheduler.StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            _agent.destination = destination;
            _agent.speed = _maxSpeed * Mathf.Clamp01(speedFraction);
            _agent.isStopped = false;
        }

        public void Cancel() => 
            _agent.isStopped = true;

        private void UpdateAnimator()
        {
            _velocity = _agent.velocity;
            _localVelocity = transform.InverseTransformDirection(_velocity);
            _speed = _localVelocity.z;
            _animator.SetFloat(_forwardSpeed, _speed, 0.1f, Time.deltaTime);
        }
    }
}