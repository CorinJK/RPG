using Scripts.Control;
using Scripts.Core;
using Scripts.Movement;
using UnityEngine;

namespace Scripts.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private const string _attack = "attack_1";
        private const string _stopAttack = "stopAttack";

        [SerializeField] private Mover _mover;
        [SerializeField] private ActionScheduler _actionScheduler;
        [SerializeField] private Animator _animator;

        private Health _target;

        [SerializeField] private float _weaponRange = 2f;
        [SerializeField] private float _timeBetweenAttacks = 1f;
        [SerializeField] private float _damage = 5f;
        private float _timeSinseLastAttack = Mathf.Infinity;

        private void Update()
        {
            _timeSinseLastAttack += Time.deltaTime;

            if (_target == null) return;
            if (_target.IsDead()) return;

            if (!GetIsInRange())
                _mover.MoveTo(_target.transform.position);
            else
            {
                _mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);

            if (_timeSinseLastAttack >= _timeBetweenAttacks)
            {
                // Animation event
                TriggerAttack();
                _timeSinseLastAttack = 0;
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
                return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        // Animation event
        private void Hit()
        {
            if (_target == null) return;
            _target.TakeDamage(_damage);
        }

        public void Cancel()
        {
            StopAttack();
            _target = null;
        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger(_stopAttack);
            _animator.SetTrigger(_attack);
        }

        private void StopAttack()
        {
            _animator.ResetTrigger(_attack);
            _animator.SetTrigger(_stopAttack);
        }

        private bool GetIsInRange() =>
            Vector3.Distance(transform.position, _target.transform.position) < _weaponRange;
    }
}