using System;
using System.Collections;
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
    

    Vector2 mousePos;

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
        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (shoot.triggered)
        {
            Shoot();
        }
    }


    void Shoot()
    {
        //Vector2 lookDir = mousePos - prb.position;

        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //prb.rotation = angle;


        
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

       
        

    }
    
}
