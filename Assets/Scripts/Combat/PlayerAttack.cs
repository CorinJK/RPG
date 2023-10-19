using Scripts.Core;
using Scripts.Movement;
using UnityEngine;

namespace Scripts.Combat
{
    public class PlayerAttack : MonoBehaviour, IAction
    {
        private const string _attack = "attack_1";

        [SerializeField] private PlayerMove _playerMove;
        [SerializeField] private ActionScheduler _actionScheduler;
        [SerializeField] private Animator _animator;

        [SerializeField] private float _weaponRange = 2f;
        private Transform _target;

        private void Update()
        {
            if (_target == null) return;

            if (!GetIsInRange())
                _playerMove.MoveTo(_target.position);
            else
            {
                _playerMove.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            _animator.SetTrigger(_attack);
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget.transform;
        }

        public void Cancel() => 
            _target = null;

        private bool GetIsInRange() =>
            Vector3.Distance(transform.position, _target.position) < _weaponRange;
    }
}