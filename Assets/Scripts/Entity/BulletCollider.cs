
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    [SerializeField][Tooltip("Gameobject for HitEffect Animation")]
    public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {

        //initiate explosion effect and destroy bullet object
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.15f);
        Destroy(gameObject);

    }
}
