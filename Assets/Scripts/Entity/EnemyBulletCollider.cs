using AbstractClasses;
using Managers;
using UnityEngine;

namespace Entity
{
    public class EnemyBulletCollider : MonoBehaviour
    {
        [SerializeField][Tooltip("Gameobject for HitEffect Animation")]
        private GameObject hitEffect;
    
        void OnCollisionEnter2D(Collision2D collision)
        {
        
            if (collision.collider.GetComponent<Damageable>())
            {
                var damage = 1;
                PlayerManager.Instance.GetPlayer().GetComponent<Damageable>().Hit(damage);
            }
            //initiate explosion effect and destroy bullet object
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.15f);
            Destroy(gameObject);
        }
    }
}
