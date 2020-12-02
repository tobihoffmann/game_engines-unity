
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Entity.Player
{
    public class PlayerMovementDash : MonoBehaviour
    {
        [SerializeField][Tooltip("Dash InputAction of the player")]
        private InputAction dash;
        
        [SerializeField][Tooltip("Dash distance of the player")]
        private float dashDistance = 500f;
        
        [SerializeField][Tooltip("Main Camera Object")]
        private Camera mainCam;

        [SerializeField] [Tooltip("All Layers with Objects to collide with on dash")]
        private LayerMask dashLayerMask;

        [SerializeField] [Tooltip("Cooldown time for Dash")]
        private float CoolDown;
        
        
        private Rigidbody2D _player;

        private Vector2 _mousePosition;
        private Vector2 _playerPosition;
        private Vector2 _dashDirection;
        private bool _isDashTriggered;
        private float t;
        
        private void OnEnable()
        {
            dash.Enable();
        }
        private void OnDisable()
        {
            dash.Disable();
        }

        void Start()
        {
            _player = GetComponent<Rigidbody2D>();
            //set t to CoolDown time
            t = CoolDown;
        }
        
        void Update()
        {    
            //count down cooldown time
            t = t - Time.deltaTime;
            
            _mousePosition = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _playerPosition = new Vector2(transform.position.x, transform.position.y);
            _dashDirection = new Ray2D(_playerPosition,_mousePosition - _playerPosition).direction;

            if (dash.triggered)
            {
                if(t<=0)_isDashTriggered = true;
                
            }
        }

        private void FixedUpdate()
        {
            if (_isDashTriggered)
            {
                Vector3 dashPosition = _playerPosition + _dashDirection * (Time.deltaTime * dashDistance);

                RaycastHit2D rcHitToDashPosition = Physics2D.Raycast(_playerPosition, _dashDirection, (Time.deltaTime * dashDistance), dashLayerMask);
                if (rcHitToDashPosition.collider != null)
                {
                    dashPosition = rcHitToDashPosition.point;
                }
                _player.MovePosition(dashPosition);
                _isDashTriggered = false;
                
                //set t back to Cooldown time
                t = CoolDown;
            }
        }
    }
}

