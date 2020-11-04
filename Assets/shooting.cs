
using UnityEngine;
using UnityEngine.InputSystem;

public class shooting : MonoBehaviour
{
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    [SerializeField][Tooltip("Input actions for shooting")]
    private InputAction shoot;


    

    

    private void OnEnable()
    {
        shoot.Enable();
    }
    private void OnDisable()
    {
        shoot.Disable();
    }

    void Update()
    { 
        if (shoot.triggered)
        {
            Shoot();
        }
        
        
    }


    void Shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

       
        

    }
    
}
