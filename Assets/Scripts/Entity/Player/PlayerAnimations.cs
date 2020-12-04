using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator _animator;
        private String _currentState;
        
        private Controls _controls;

        private Vector2 _walkingDirection;
        
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
        
        
        // Start is called before the first frame update
        void Start()
        {
            _animator = gameObject.GetComponent<Animator>();
            _controls.Player.Dash.performed += _ => Dashing();
            _controls.Player.Shoot.performed += _ => Shooting();
            _controls.Player.Melee.performed += _ => MeleeAttack();
        }

        private void Update()
        {
            Walking();
        }
        
        private void ChangeState(string newState)
        {
            if (_currentState == newState) return;
            _animator.Play(newState);
            _currentState = newState;
        }
        
        private void Walking()
        {
            _walkingDirection =new Vector2(_controls.Player.WalkingHorizontal.ReadValue<float>(), _controls.Player.WalkingVertical.ReadValue<float>()).normalized;
            
            if      (_walkingDirection.y > 0) ChangeState(PlayerWalkingBack);
            else if (_walkingDirection.y < 0) ChangeState(PlayerWalkingFront);
            else if (_walkingDirection.y == 0 && _walkingDirection.x > 0) ChangeState(PlayerWalkingRight);
            else if (_walkingDirection.y == 0 && _walkingDirection.x < 0)  ChangeState(PlayerWalkingLeft);
            else ChangeState((PlayerIdleFront));
        }


        private void Dashing()
        {
            
        }

        private void Shooting()
        {
            
        }
        
        private void MeleeAttack()
        {
            ChangeState(PlayerMelee);
        }
        
    }
}
