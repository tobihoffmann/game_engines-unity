
using UnityEngine;
using UnityEngine.InputSystem;

public class Melee : MonoBehaviour
{
    [SerializeField][Tooltip("Input actions for melee attack")]
    private InputAction melee;
    
    [SerializeField][Tooltip("Center of the AttackCircle")]
    private Transform attackCenter;
    
    [SerializeField][Tooltip("Radian of the AttackCircle")]
    private float attackRange = 0.5f;
    
    [SerializeField][Tooltip("Damage of the Melee Attack")]
    private float MeleeDamage;
    
    [SerializeField][Tooltip("LayerMask for detecting enemies")]
    private LayerMask enemyLayers;
    
    [SerializeField][Tooltip("AnimatorObject for MeleeAnimation")]
    private Animator animator;
    
    private void OnEnable()
    {
        melee.Enable();
        
    }
    private void OnDisable()
    {
        melee.Disable();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (melee.triggered)
        {
            animator.SetTrigger("MeleeTrigger");
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackCenter.position, attackRange, enemyLayers);
        
        Debug.Log("MeleeAttack");
        
        
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            //give damage to the enemy 
            //just pseudo code, because no enemyManager implemented yet
            
            //enemy.EnemyManager.SetHitpoints(-MeleeDamage);
        }
        
        
    
    }
    //Draws an circle to show the melee attack area
    
    void OnDrawGizmosSelected()
    {
        if (attackCenter == null) return;

        Gizmos.DrawWireSphere(attackCenter.position, attackRange);
    }
    
    
}


