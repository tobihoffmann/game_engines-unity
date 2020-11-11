
using UnityEngine;

public class BulletCollider : MonoBehaviour
{

    public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {

        //initiate explosion effect and destroy bullet object
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.15f);
        Destroy(gameObject);

    }
}
