
using AbstractClasses;
using Entity.Enemy;
using UnityEngine;


public class EnemyState : Damageable
{
    
    /// <summary>
    /// The maximum hit points of the enemy
    /// </summary>
    [SerializeField] [Tooltip("maximum hit points of the enemy")]
    private int maxHitPoints;

    [SerializeField][Range(0,100)]
    private int dropChance;

    [SerializeField]
    private GameObject healthKit;

    public void ChangeEnemyHitPoints(int changeBy)
    {
        int updatedValue = hitPoints + changeBy;
        if (updatedValue > maxHitPoints) updatedValue = maxHitPoints;
        updatedValue = Mathf.Clamp(updatedValue, 0, maxHitPoints);
        hitPoints = updatedValue;
        //throws event with the new health value as a parameter
        
        if (hitPoints <= 0)
        {
            int r = Random.Range(0, 100);
            if (gameObject.GetComponent<Demon>() != null && r <= dropChance)
            {
                Debug.Log("this is true");
                Instantiate(healthKit, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    public override void Hit(int damage)
    {
        gameObject.GetComponent<StateMachine>().Chase();
        ChangeEnemyHitPoints(-damage);
    }

    public override void Heal(int hitPoints)
    {
        ChangeEnemyHitPoints(hitPoints);
    }

    
}
