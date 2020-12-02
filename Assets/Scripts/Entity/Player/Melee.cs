﻿
using Entity.Enemy;
using Managers;
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

        if (hitEnemies.Length > 0)
        {
            AudioManager.Instance.Play("PlayerMeleeHit");
        }
        else
        {
            AudioManager.Instance.Play("PlayerMeleeSwing");
        }
        
        foreach(Collider2D enemy in hitEnemies)
        {
            //give damage to the enemy 
            enemy.GetComponent<EnemyState>().Hit((int)MeleeDamage);
            
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


