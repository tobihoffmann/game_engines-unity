
using UnityEngine;
using UnityEngine.InputSystem;

public class Melee : MonoBehaviour
{
    [SerializeField][Tooltip("Input actions for melee attack")]
    private InputAction melee;
    
    [SerializeField][Tooltip("Center of the AttackCircle")]
    private Transform attackPoint;
    
    [SerializeField][Tooltip("Radian of the AttackCircle")]
    private float attackRange = 0.5f;
    
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
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        Debug.Log("MeleeAttack");
        
        
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
        }
        
        
    
    }
    //Draws an circle to show the melee attack area
    /*
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    */
    
}


