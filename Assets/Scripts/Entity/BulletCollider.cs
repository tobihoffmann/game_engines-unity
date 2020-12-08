
using AbstractClasses;
using Entity.Player;
using Managers;
using UnityEngine;


namespace Entity
{
    public class BulletCollider : MonoBehaviour
    {
        [SerializeField][Tooltip("Gameobject for HitEffect Animation")]
        private GameObject hitEffect;

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<Damageable>())
            {
                int damage = PlayerManager.Instance.GetPlayer().GetComponent<PlayerActions>().GetBulletDamage();
                collision.collider.GetComponent<EnemyState>().Hit(damage);
            }
            
            //initiate explosion effect and destroy bullet object
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.15f);
            Destroy(gameObject);
            
            

        }
    }
}
