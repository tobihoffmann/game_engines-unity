
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
        }

        private void FixedUpdate()
        {
            if (_isDashTriggered)
            {
                _player.MovePosition(_playerPosition + _dashDirection * (Time.deltaTime * dashDistance));
                _isDashTriggered = false;
            }
        }
    }
}

