using AbstractClasses;
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
        
        public static event PlayerIsDead OnPlayerDeath;
        
        
        /// <summary>
        /// changes the entities current hit points
        /// </summary>
        public void ChangePlayerHitPoints(int changeBy)
        {
            int updatedValue = hitPoints + changeBy;
            if (updatedValue > maxHitPoints) updatedValue = maxHitPoints;
            updatedValue = Mathf.Clamp(updatedValue, 0, maxHitPoints);
            hitPoints = updatedValue;
            //throws an event with the new health value as a parameter
            OnPlayerHitPointsUpdate?.Invoke(hitPoints);
            if (hitPoints <= 0) 
                Die();
        }

        public override void Hit(int damage)
        {
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
    }
}
