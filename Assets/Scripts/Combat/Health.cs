using UnityEngine;

namespace Scripts.Combat
{
    public class Health : MonoBehaviour
    {
        private const string die = "die";

        [SerializeField] private float _healthPoints = 100f;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerAttack _playerAttack;

        private bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            _healthPoints = Mathf.Max(_healthPoints - damage, 0);
            Debug.Log(_healthPoints);

            if (_healthPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            _animator.SetTrigger(die);
        }
    }
}