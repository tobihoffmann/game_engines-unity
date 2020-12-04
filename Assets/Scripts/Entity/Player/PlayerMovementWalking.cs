using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Player
{
    public class PlayerMovementWalking : MonoBehaviour
    {
        

        [SerializeField][Tooltip("Movement speed of the player")]
        private float movementSpeed = 5f;
        
        private Controls _controls;
        private Rigidbody2D _player;
        
        private Vector2 _walkingDirection;
        
        private Animator _animator;
        private String _currentState;
        
       

        private bool _isShooting;
        private bool _isDashing;

        
        
        // States
        private const string PlayerIdleFront = "PlayerIdle_front";
        private const string PlayerIdleBack =  "PlayerIdle_back";
        private const string PlayerIdleLeft =  "PlayerIdle_left";
        private const string PlayerIdleRight = "PlayerIdle_right";
        
        private const string PlayerWalkingFront = "PlayerWalking_front";
        private const string PlayerWalkingBack =  "PlayerWalking_back";
        private const string PlayerWalkingLeft =  "PlayerWalking_left";
        private const string PlayerWalkingRight = "PlayerWalking_right";
        
        private const string PlayerDashFront = "PlayerDash_front";
        private const string PlayerDashBack =  "PlayerDash_back";
        private const string PlayerDashLeft =  "PlayerDash_left";
        private const string PlayerDashRight = "PlayerDash_right";
        
        private const string PlayerShootingFront = "PlayerShooting_front";
        private const string PlayerShootingBack =  "PlayerShooting_back";
        private const string PlayerShootingLeft =  "PlayerShooting_left";
        private const string PlayerShootingRight = "PlayerShooting_right";
        
        private const string PlayerMelee = "PlayerMelee";

        // private enum states
        // {
        //     PlayerIdle_front,
        //     PlayerIdle_back,
        //     PlayerIdle_left,
        //     PlayerIdle_right,
        //     PlayerWalking_front,
        //     PlayerWalking_back,
        //     PlayerWalking_left,
        //     PlayerWalking_right,
        //     PlayerDash_front,
        //     PlayerDash_back,
        //     PlayerDash_left,
        //     PlayerDash_right,
        //     PlayerShooting_front,
        //     PlayerShooting_back,
        //     PlayerShooting_left,
        //     PlayerShooting_right,
        //     PlayerMelee
        // }

        

        private void Awake() => _controls = new Controls();
        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();
      

        void Start()
        {
            _player = GetComponent<Rigidbody2D>();
            _animator = gameObject.GetComponent<Animator>();
            _controls.Player.Dash.performed += _ => _isDashing = true;
            _controls.Player.Shoot.performed += _ => _isShooting = true;
            _controls.Player.Melee.performed += _ => ChangeState(PlayerMelee);
        }
        

        private void Update()
        {
            _walkingDirection =new Vector2(_controls.Player.WalkingHorizontal.ReadValue<float>(), _controls.Player.WalkingVertical.ReadValue<float>()).normalized;
        }

        void FixedUpdate()
        {
            _player.MovePosition(_player.position + _walkingDirection * (Time.deltaTime * movementSpeed));

            if (_walkingDirection != new Vector2(0,0))
            {
                ChangeState(PlayerWalkingLeft);   
            }
            else
            {
                ChangeState(PlayerIdleFront);
            }
        }
        
        private void ChangeState(string newState)
        {
            if (_currentState == newState) return;
            _animator.Play(newState);
            _currentState = newState;
        }

        private void Walking()
        {
            if (_walkingDirection.y > 0)
            {
                ChangeState(PlayerWalkingBack);
            }
            else if (_walkingDirection.y < 0)
            {
                ChangeState(PlayerWalkingFront);
            }

            if (_walkingDirection.x > 0)
            {
                if (_walkingDirection.y > 0)
                {
                    ChangeState(PlayerWalkingBack);
                }

                else if (_walkingDirection.y < 0)
                {
                    ChangeState(PlayerWalkingFront);
                }
                else
                {
                    ChangeState(PlayerWalkingRight);
                }
            }
            else if (_walkingDirection.y < 0)
            {
                if (_walkingDirection.y > 0)
                {
                    ChangeState(PlayerWalkingBack);
                }

                else if (_walkingDirection.y < 0)
                {
                    ChangeState(PlayerWalkingFront);
                }
                else
                {
                    ChangeState(PlayerWalkingLeft);
                }
            }
            else
            {
                ChangeState((PlayerIdleFront));
            }
        }


        private void Dashing()
        {
            
        }

        private void Shooting()
        {
            
        }
        
        private void MeleeAttack()
        {
            
        }
    }
}