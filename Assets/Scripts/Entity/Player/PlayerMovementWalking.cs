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

        private void Awake() => _controls = new Controls();
        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

        void Start()
        {
            _player = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            _walkingDirection =new Vector2(_controls.Player.WalkingHorizontal.ReadValue<float>(), _controls.Player.WalkingVertical.ReadValue<float>()).normalized;
        }

        void FixedUpdate()
        {
            _player.MovePosition(_player.position + _walkingDirection * (Time.deltaTime * movementSpeed));
        }
    }
}