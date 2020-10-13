using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Player
{
    public class PlayerMovementWalking : MonoBehaviour
    {
        [SerializeField][Tooltip("Movement speed of the player")]
        private float movementSpeed = 5f;
        
        [SerializeField][Tooltip("Input actions for walking")]
        private InputAction walking;

        private Vector2 _inputVector;

        private CharacterController _controller;

        private void OnEnable()
        {
            walking.Enable();
        }
        private void OnDisable()
        {
            walking.Disable();
        }

        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }
        
        void Update()
        {
            _inputVector = walking.ReadValue<Vector2>();
            _controller.Move(_inputVector * (Time.deltaTime * movementSpeed));
        }
    }
}