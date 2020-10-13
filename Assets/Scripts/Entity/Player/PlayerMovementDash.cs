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
    
        private CharacterController _controller;
        
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
            _controller = GetComponent<CharacterController>();
        }
        
        void Update()
        {
            Vector2 mousePosition = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 dashDirection = new Ray2D(playerPosition,mousePosition - playerPosition).direction;
            
            if (dash.triggered)
            {
                _controller.Move(dashDirection * (Time.deltaTime * dashDistance));
            }
        }
    }
}

