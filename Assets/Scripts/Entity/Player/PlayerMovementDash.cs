
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Entity.Player
{
    public class PlayerMovementDash : MonoBehaviour
    {
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
        }
        
        void Update()
        {
            _mousePosition = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _playerPosition = new Vector2(transform.position.x, transform.position.y);
            _dashDirection = new Ray2D(_playerPosition,_mousePosition - _playerPosition).direction;

            if (dash.triggered) _isDashTriggered = true;
            //turns the player in mousedirection
           
            // Vector2 lookDir = _mousePosition - _player.position;
            // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            // _player.rotation = angle;
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
            }
        }
    }
}

