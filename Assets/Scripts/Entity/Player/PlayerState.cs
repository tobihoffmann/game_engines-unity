using AbstractClasses;
using Assets.Scripts.Item_Management;
using Item_Management;
using Managers;
using UnityEngine;

namespace Entity.Player
{
    public class PlayerState : Damageable
    {
        public delegate void PlayerStateChanged(int newValue);
        
        public delegate void PlayerIsDead();

        /// <summary>
        /// The maximum hit points of the player
        /// </summary>
        [SerializeField] [Tooltip("maximum hit points of the player")]
        private int maxHitPoints;

        public static event PlayerStateChanged OnPlayerHitPointsUpdate;
        public static event PlayerStateChanged OnMaxHitPointUpdate;
        public static event PlayerIsDead OnPlayerDeath;
        
        private void OnEnable()
        {
            Inventory.OnJuggernautUpdate += UpdateMaxHitPoints;
        }

        private void OnDisable()
        {
            Inventory.OnJuggernautUpdate -= UpdateMaxHitPoints;
        }
        
        
        /// <summary>
        /// changes current hit points of the player
        /// </summary>
        public void ChangePlayerHitPoints(int changeBy)
        {
            int updatedValue = hitPoints + changeBy;
            if (updatedValue > maxHitPoints) updatedValue = maxHitPoints;
            updatedValue = Mathf.Clamp(updatedValue, 0, maxHitPoints);
            hitPoints = updatedValue;
            //throws event with the new health value as a parameter
            OnPlayerHitPointsUpdate?.Invoke(hitPoints);
            if (hitPoints <= 0) 
                Die();
        }
        
        public override void Hit(int damage)
        {
            AudioManager.Instance.Play("PlayerDamageTaken");
            ChangePlayerHitPoints(-damage);
        }

        public override void Heal(int healAmount)
        {
           ChangePlayerHitPoints(healAmount);
        }
        
        protected override void Die()
        {
            base.Die();
            OnPlayerDeath?.Invoke();
        }

        public int GetMaxHitPoints()
        {
            return maxHitPoints;
        }

        private void UpdateMaxHitPoints(int value)
        {
            maxHitPoints += value;
            if (maxHitPoints >= 10) maxHitPoints = 10;
            OnMaxHitPointUpdate?.Invoke(maxHitPoints);
        }
    }
}
