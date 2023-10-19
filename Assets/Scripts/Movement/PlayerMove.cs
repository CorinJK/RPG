using Scripts.Combat;
using Scripts.Core;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Movement
{
    public class PlayerMove : MonoBehaviour, IAction
    {
        private const string _forwardSpeed = "forwardSpeed";

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private ActionScheduler _actionScheduler;

        private Vector3 _velocity;
        private Vector3 _localVelocity;
        private float _speed;

        private void Update() => 
            UpdateAnimator();

        public void StartMoveAction(Vector3 destination)
        {
            _actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            _agent.destination = destination;
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