
using UnityEngine;
using UnityEngine.InputSystem;

public class shooting : MonoBehaviour
{
    public Camera cam;
    public Rigidbody2D prb;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    //public InputMaster controls;
    [SerializeField][Tooltip("Input actions for shooting")]
    private InputAction shoot;


    // Update is called once per frame

    private Vector2 _mousePosition;
    private Vector2 _playerPosition;
    private Vector2 _dashDirection;
    private Vector2 mousePos;

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
        /*
        _mousePosition = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _playerPosition = new Vector2(transform.position.x, transform.position.y);
        _dashDirection = new Ray2D(_playerPosition, _mousePosition - _playerPosition).direction;
        */
        

        mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 lookDir = (Vector3)mousePos - firePoint.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        firepoint.rotation = angle;
        


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
