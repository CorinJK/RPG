using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] NavMeshAgent Agent;

        void Update()
        {
            Agent.destination = _target.position;
        }
    }
}