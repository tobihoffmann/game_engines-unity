
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Entity.Player
{
    public class PlayerMovementDash : MonoBehaviour

        
    {
        [SerializeField][Tooltip("Cooldown Time for Dash")]
        private float coolDown;

        [SerializeField][Tooltip("Dash distance of the player")]
        private float dashDistance = 500f;
        
        [SerializeField][Tooltip("Dash InputAction of the player")]
        private InputAction dash;

        [SerializeField][Tooltip("Main Camera Object")]
        private Camera mainCam;

        [SerializeField] [Tooltip("All Layers with Objects to collide with on dash")]
        private LayerMask dashLayerMask;
        
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
            t = coolDown;
            _player = GetComponent<Rigidbody2D>();

            
        }
        
        void Update()
        {
            //Dash cooldown
            t -= Time.deltaTime;
            if (t <= 0)
            {
                //Dash direction calculations
                _mousePosition = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _playerPosition = new Vector2(transform.position.x, transform.position.y);
                _dashDirection = new Ray2D(_playerPosition, _mousePosition - _playerPosition).direction;

                if (dash.triggered) _isDashTriggered = true;
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
                
                //if dash is used, reset t ro CoolDown value
                t = coolDown;
            }
        }
    }
}

