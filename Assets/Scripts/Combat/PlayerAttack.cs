using Scripts.Core;
using Scripts.Movement;
using UnityEngine;

namespace Scripts.Combat
{
    public class PlayerAttack : MonoBehaviour, IAction
    {
        private const string _attack = "attack_1";
        private const string _stopAttack = "stopAttack";

        [SerializeField] private PlayerMove _playerMove;
        [SerializeField] private ActionScheduler _actionScheduler;
        [SerializeField] private Animator _animator;

        private Health _target;

        [SerializeField] private float _weaponRange = 2f;
        [SerializeField] private float _timeBetweenAttacks = 1f;
        [SerializeField] private float _damage = 5f;
        private float _timeSinseLastAttack = 0f;

        private void Update()
        {
            _timeSinseLastAttack += Time.deltaTime;

            if (_target == null) return;
            if (_target.IsDead()) return;

            if (!GetIsInRange())
                _playerMove.MoveTo(_target.transform.position);
            else
            {
                _playerMove.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);

            if (_timeSinseLastAttack >= _timeBetweenAttacks)
            {
                // Animation event
                _animator.SetTrigger(_attack);
                _timeSinseLastAttack = 0;
            }
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null)
                return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = GetComponent<Health>();
        }

        // Animation event
        private void Hit()
        {
            _target.TakeDamage(_damage);
        }

        public void Cancel()
        {
            _animator.SetTrigger(_stopAttack);
            _target = null;
        }

        private bool GetIsInRange() =>
        Vector3.Distance(transform.position, _target.transform.position) < _weaponRange;
    }
}