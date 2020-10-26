using UnityEngine;

namespace AbstractClasses
{
    public abstract class Damageable : MonoBehaviour
    {
        /// <summary>
        /// The actual hit points of the entity
        /// </summary>
        [SerializeField] [Tooltip("current hitpoints of the entity")]
        public int hitPoints;
        
        /// <summary>
        /// Used to prevent attacking a dead entity.
        /// </summary>
        public bool IsDead { get; private set; }
        
        /// <summary>
        /// Does damage to the entity
        /// </summary>
        public abstract void Hit(int damage);

        /// <summary>
        /// Heals to the entity
        /// </summary>
        public abstract void Heal(int hitPoints);


        /// <summary>
        /// Lets the entity die and prevents multiple deaths.
        /// </summary>
        protected virtual void Die()
        {
            if (IsDead)
                return;
            IsDead = true;
            
            Debug.Log("Entity is dead.");
        }
    }
}
