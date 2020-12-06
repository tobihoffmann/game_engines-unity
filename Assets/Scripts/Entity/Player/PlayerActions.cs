using System;
using DG.Tweening;
using Managers;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Player
{
    public class PlayerActions : MonoBehaviour
    {     
        [Header("Walking")]
        [SerializeField][Tooltip("Movement speed of the player")]
        private float movementSpeed = 5f;
        
        [Header("Shooting")]
        [SerializeField] [Tooltip("Damage for shooting")]
        private float shootingDamage;
        
        [SerializeField][Tooltip("Coodown time for shooting")]
        private float shootingCoolDown;

        [SerializeField][Tooltip("FirePoint Object")]
        private Transform firePoint;
        
        [SerializeField][Tooltip("Prefab for Bullet Object")]
        private GameObject bulletPrefab;
        
        [SerializeField][Tooltip("Float variable for the Bullet force")]
        private float bulletForce = 50f;

        private float _shootingTimer;
        private bool _shootingIsOnCooldown;
        
        [Header("Melee")]
        [SerializeField][Tooltip("Damage of the Melee Attack")]
        private float meleeDamage;
    
        [SerializeField][Tooltip("CoolDown Time of the Melee Attack")]
        private float meleeCoolDown;
        
        [SerializeField][Tooltip("Radian of the AttackCircle")]
        private float meleeRange;
    
        [SerializeField][Tooltip("LayerMask for detecting enemies")]
        private LayerMask enemyLayers;

        private float _meleeTimer;
        private bool _meleeIsOnCooldown;
        
        [Header("Dash")]
        [SerializeField][Tooltip("Dash distance of the player")]
        private float dashDistance = 500f;
        
        [SerializeField] [Tooltip("Cooldown time for Dash")]
        private float dashCooldown;

        [SerializeField] [Tooltip("All Layers with Objects to collide with on dash")]
        private LayerMask dashLayerMask;

        private Vector2 _dashDirection;
        private float _dashTimer;
        private bool _dashIsOnCooldown;
        private bool _dashTriggered;
        private Vector3 _dashPosition;

        private Controls _controls;
        private Camera _mainCamera;
        private Rigidbody2D _player;
        
        // Shooting
        private Vector2 _playerPosition;
        private Vector2 _mousePosition;
        private Vector2 _shootDirection;
        private Vector2 _firepointPosition;

        // Movement
        private Vector2 _walkingDirection;

        // Animation
        private Animator _animator;

        // Animation Triggers
        private const string DashDown = "DashDown";
        private const string DashUp =  "DashUp";
        private const string DashLeft =  "DashLeft";
        private const string DashRight = "DashRight";
        
        private const string ShootDown = "ShootDown";
        private const string ShootUp =  "ShootUp";
        private const string ShootLeft =  "ShootLeft";
        private const string ShootRight = "ShootRight";
        
        private const string Melee = "Melee";

        private void Awake() => _controls = new Controls();
        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();
        
        
        // Start is called before the first frame update
        void Start()
        {
            _shootingTimer = 0;
            _meleeTimer = 0;
            _dashTimer = 0;

            _player = gameObject.GetComponent<Rigidbody2D>();
            _animator = gameObject.GetComponent<Animator>();

            _controls.Player.Dash.performed += _ => _dashTriggered = true;
            _controls.Player.Shoot.performed += _ => Shoot();
            _controls.Player.Melee.performed += _ => MeleeAttack();
            
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            _playerPosition = PlayerManager.Instance.GetPlayerPosition();
            
            // Shooting Cooldown
            _shootingTimer = _shootingTimer - Time.deltaTime;
            if (_shootingTimer <= 0)
            {
                _shootingIsOnCooldown = false;
            }
           
            // Melee Cooldown
            _meleeTimer = _meleeTimer - Time.deltaTime;
            if (_meleeTimer <= 0)
            {
                _meleeIsOnCooldown = false;
            }

            // Dash CoolDown
            _dashTimer = _dashTimer - Time.deltaTime;
            if (_dashTimer <= 0)
            {
                _dashIsOnCooldown = false;
            }
            
            Walk();
        }

        void FixedUpdate()
        {
            // Handle walking "physics"
            if (_animator.GetBool("isFinished"))
            {
                _player.MovePosition(_player.position + _walkingDirection * (Time.deltaTime * movementSpeed));
            }
            
            if (_dashTriggered)
            {
                Dash();
                _dashTriggered = false;
            }
        }

        /** Player Actions **/
        
        /// <summary>
        /// Reads Movement inputs and passes information to the Animator
        /// </summary>
        private void Walk()
        {
            _walkingDirection = new Vector2(_controls.Player.WalkingHorizontal.ReadValue<float>(), _controls.Player.WalkingVertical.ReadValue<float>()).normalized;
            
            _animator.SetFloat("xMovement",_controls.Player.WalkingHorizontal.ReadValue<float>());
            _animator.SetFloat("yMovement",_controls.Player.WalkingVertical.ReadValue<float>());
        }
        
        /// <summary>
        /// Dash Ability for the player: Handles physics, sound and animation triggers
        /// </summary>
        private void Dash()
        {
            if (!_dashIsOnCooldown)
            {
                //Play audio
                AudioManager.Instance.Play("PlayerDash");

                _mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _dashDirection = new Ray2D(_playerPosition,_mousePosition - _playerPosition).direction;
                Vector2 playerOffset = _player.GetComponent<BoxCollider2D>().offset;
                _playerPosition += playerOffset;
                _dashPosition = _playerPosition + _dashDirection * dashDistance;
                
                //Avoid obstacle objects
                //TODO: FIX IT BABY
                RaycastHit2D rcHitToDashPosition = Physics2D.Raycast(_playerPosition, _dashDirection, dashDistance, dashLayerMask);
                if (rcHitToDashPosition.collider != null)
                {
                    _dashPosition = rcHitToDashPosition.point;
                }

                _player.transform.DOMove(_dashPosition, 0.5f);
                

                // Handle Animation
                float dashAngle = Vector2.SignedAngle(Vector2.up, _dashDirection);
                if (dashAngle < 45 && dashAngle > -45)
                    _animator.SetTrigger(DashUp);
                else if (dashAngle >= 45 && dashAngle <= 135)
                    _animator.SetTrigger(DashLeft);
                else if (dashAngle <= -45 && dashAngle >= -135)
                    _animator.SetTrigger(DashRight);
                else
                    _animator.SetTrigger(DashDown);
                
                _dashTimer = dashCooldown;
                _dashIsOnCooldown = true;
            }
        }
        
        /// <summary>
        /// Shoot Ability for the player: Handles physics, sound and animation triggers
        /// </summary>
        private void Shoot()
        {
            if (!_shootingIsOnCooldown)
            {
                // Play Audio
                AudioManager.Instance.Play("PlayerGunShot");
            
                _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _firepointPosition = new Vector2(firePoint.transform.position.x, firePoint.transform.position.y);
                _shootDirection = new Ray2D(_firepointPosition, _mousePosition - _firepointPosition).direction;

                //initiate the bullet
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                // Makes sure the bullet doesn't collide with the Object that is shooting it
                Physics2D.IgnoreCollision(bullet.GetComponent<BoxCollider2D>(), transform.GetComponent<BoxCollider2D>());

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                //turn the bullet in the correct direction
                float angle = Mathf.Atan2(_shootDirection.y, _shootDirection.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = angle;

                //addForce in direction of Mouse position
                rb.AddForce(_shootDirection * bulletForce, ForceMode2D.Impulse);
                
                // Handle Animations
                float shootingAngle = Vector2.SignedAngle(Vector2.up, _shootDirection);
                
                if (shootingAngle < 45 && shootingAngle > -45)
                    _animator.SetTrigger(ShootUp);
                else if (shootingAngle >= 45 && shootingAngle <= 135)
                    _animator.SetTrigger(ShootLeft);
                else if (shootingAngle <= -45 && shootingAngle >= -135)
                    _animator.SetTrigger(ShootRight);
                else
                    _animator.SetTrigger(ShootDown);

                _shootingTimer = shootingCoolDown;
                _shootingIsOnCooldown = true;
            }
        }
        
        /// <summary>
        /// Returns the Value set for Shooting Damage
        /// TODO: Could make this internal, if we put BulletCollider.cs into the same Namespace
        /// </summary>
        public float GetBulletDamage() 
        { 
            return shootingDamage;;
        }
        
        /// <summary>
        /// Melee Ability for the player: Handles physics, sound and animation triggers and passes damage values to EnemyState
        /// </summary>
        private void MeleeAttack()
        {
            if (!_meleeIsOnCooldown)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_playerPosition, meleeRange, enemyLayers);
            
                // Play Audio
                if (hitEnemies.Length > 0) AudioManager.Instance.Play("PlayerMeleeHit");
                else AudioManager.Instance.Play("PlayerMeleeSwing");
            
                foreach(Collider2D enemy in hitEnemies)
                {
                    //give damage to the enemy 
                    enemy.GetComponent<EnemyState>().Hit((int)meleeDamage);
                }
                // Handle Animations
                _animator.SetTrigger(Melee);
            
                _meleeTimer = meleeCoolDown;
                _meleeIsOnCooldown = true;
            }
        }
    }
}
