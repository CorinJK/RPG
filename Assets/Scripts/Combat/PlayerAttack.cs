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
        private Transform _target;
        private Health _health;

        [SerializeField] private float _weaponRange = 2f;
        [SerializeField] private float _timeBetweenAttacks = 1f;
        [SerializeField] private float _damage = 5f;
        private float _timeSinseLastAttack = 0f;

        private void Update()
        {
            _timeSinseLastAttack += Time.deltaTime;

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
            if (_timeSinseLastAttack >= _timeBetweenAttacks)
            {
                // Animation event
                _animator.SetTrigger(_attack);
                _timeSinseLastAttack = 0;
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget.transform;
        }

        // Animation event
        private void Hit()
        {
            _health = _target.GetComponent<Health>();
            _health.TakeDamage(_damage);
        }

        public void Cancel() =>
            _target = null;

        private bool GetIsInRange() =>
            Vector3.Distance(transform.position, _target.position) < _weaponRange;
    }
}