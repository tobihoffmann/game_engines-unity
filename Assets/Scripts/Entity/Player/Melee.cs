
using Entity.Enemy;
using UnityEngine;
using UnityEngine.InputSystem;

public class Melee : MonoBehaviour
{
    [SerializeField][Tooltip("Input actions for melee attack")]
    private InputAction melee;
    
    [SerializeField][Tooltip("Center of the AttackCircle")]
    private Transform attackCenter;
    
    [SerializeField][Tooltip("Radian of the AttackCircle")]
    private float attackRange;
    
    [SerializeField][Tooltip("Damage of the Melee Attack")]
    private float MeleeDamage;
    
    [SerializeField][Tooltip("CoolDown Time of the Melee Attack")]
    private float CoolDown;
    
    [SerializeField][Tooltip("LayerMask for detecting enemies")]
    private LayerMask enemyLayers;
    
    private Animator animator;
    private float t;
    
    private void OnEnable()
    {
        melee.Enable();
        
    }
    private void OnDisable()
    {
        melee.Disable();
        
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        t = CoolDown;
    }
    
    // Update is called once per frame
    void Update()
    {
        t = t - Time.deltaTime;
        if (melee.triggered)
        {
            if (t <= 0)
            {
                animator.SetTrigger("MeleeTrigger");
                Attack();
            }
           
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
            enemy.GetComponent<Hitregistration>().Hit((int)MeleeDamage);
            
        }

        t = CoolDown;
        
    }
    //Draws an circle to show the melee attack area
    
    void OnDrawGizmosSelected()
    {
        if (attackCenter == null) return;

        Gizmos.DrawWireSphere(attackCenter.position, attackRange);
    }
    
    
}


