using UnityEngine;

namespace Scripts.CameraLogic
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void LateUpdate()
        {
            transform.position = _target.position;
        }
    }
}