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
            

            if (dash.triggered)
            {
                Vector2 mousePosition = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                Vector2 dashDirection = new Ray(transform.position,mousePosition).direction;
                _controller.Move(dashDirection * (Time.deltaTime * dashDistance));
            }
            
            Debug.DrawRay(transform.position,mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Color.red);
            
        }
    }
}

