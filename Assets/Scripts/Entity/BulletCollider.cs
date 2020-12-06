
using AbstractClasses;
using Entity.Enemy;
using Entity.Player;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                var damage = PlayerManager.Instance.GetPlayer().GetComponent<PlayerActions>().GetBulletDamage();
                collision.collider.GetComponent<EnemyState>().Hit((int) damage);
            }
            
            //initiate explosion effect and destroy bullet object
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.15f);
            Destroy(gameObject);
            
            

        }
    }
}
