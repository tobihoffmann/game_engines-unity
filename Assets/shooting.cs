
using UnityEngine;
using UnityEngine.InputSystem;

public class shooting : MonoBehaviour
{
     [SerializeField][Tooltip("Main Camera Object")]
     private Camera mainCam;
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    [SerializeField][Tooltip("Input actions for shooting")]
    private InputAction shoot;

    private Vector2 _mousePosition;
    private Vector2 _playerPosition;
    private Vector2 _shootDirection;
    

    

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

        _mousePosition = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _playerPosition = new Vector2(transform.position.x, transform.position.y);
        _shootDirection = new Ray2D(_playerPosition,_mousePosition - _playerPosition).direction;
        rb.AddForce(_shootDirection * bulletForce, ForceMode2D.Impulse);

        float angle = Mathf.Atan2(_shootDirection.y, _shootDirection.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }
    
}
