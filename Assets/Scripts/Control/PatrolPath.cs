using UnityEngine;

namespace Scripts.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private float _waypointGizmoRadius = 0.3f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);

                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(GetWaypoint(i), _waypointGizmoRadius);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
                return 0;

            return i + 1;
        }

        public Vector3 GetWaypoint(int i) =>
            transform.GetChild(i).position;
    }
}