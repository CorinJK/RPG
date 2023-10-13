using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private const string forwardSpeed = "forwardSpeed";

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;

        [SerializeField] private Vector3 _velocity;
        private Vector3 _localVelocity;
        private float _speed;

        private void Update()
        {
            if (Input.GetMouseButton(0))
                MoveToCursore();

            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            _velocity = _agent.velocity;
            _localVelocity = transform.InverseTransformDirection(_velocity);
            _speed = _localVelocity.z;
            _animator.SetFloat(forwardSpeed, _speed, 0.1f, Time.deltaTime);
        }

        private void MoveToCursore()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
                _agent.destination = hit.point;
        }
    }
}