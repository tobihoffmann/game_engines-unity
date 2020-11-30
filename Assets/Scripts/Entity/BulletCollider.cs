
using Entity.Enemy;
using UnityEngine;

namespace Entity
{
    public class BulletCollider : MonoBehaviour
    {
        [SerializeField][Tooltip("Gameobject for HitEffect Animation")]
        private GameObject hitEffect;

        [SerializeField][Tooltip("BulletDamage")]
        private float damage;

        
        

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.name == "Spider")
            {
                //collision.collider.GetComponent<Spider>().Hit((int)Player.GetComponent<shooting>().GetDamage());
                collision.collider.GetComponent<Spider>().Hit((int) damage);
            }
            
            //initiate explosion effect and destroy bullet object
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.15f);
            Destroy(gameObject);
            
            

        }
    }
}
