using Scripts.Movement;
using UnityEngine;

namespace Scripts.Combat
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerMove _playerMove;

        [SerializeField] private float _weaponRange = 2f;
        private Transform _target;

        private void Update()
        {
            if (_target == null) return;

            if (!GetIsInRange())
                _playerMove.MoveTo(_target.position);
            else
                _playerMove.Stop();
        }

        private bool GetIsInRange() =>
            Vector3.Distance(transform.position, _target.position) < _weaponRange;

        public void Attack(CombatTarget combatTarget)
        {
            _target = combatTarget.transform;
        }

        public void Cancel()
        {
            _target = null;
        }
    }
}