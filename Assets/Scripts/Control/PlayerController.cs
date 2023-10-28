using Scripts.Combat;
using Scripts.Core;
using Scripts.Movement;
using UnityEngine;

namespace Scripts.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private Fighter _fighter;
        [SerializeField] private Health _health;

        private void Update()
        {
            if (_health.IsDead()) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!_fighter.CanAttack(target.gameObject)) 
                    continue;

                if (Input.GetMouseButton(0))
                    _fighter.Attack(target.gameObject);
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMoveAction(hit.point, 1f);
                    return true;
                }
            }
            return false;
        }

        private static Ray GetMouseRay() =>
            Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}