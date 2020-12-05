using System;
using Assets.Scripts.Item_Management;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Entity.Player
{
    public class PlayerMovementWalking : MonoBehaviour
    {
        

        [SerializeField][Tooltip("Movement speed of the player")]
        public float movementSpeed = 5f;
        
        [SerializeField][Tooltip("Input actions for walking")]
        private InputAction walking;

        private Vector2 _walkingDirection;
        

        private Rigidbody2D _player;

        private void OnEnable()
        {
            Inventory.onMovementSpeedUpdate += OnMovementSpeedUpdated;
            walking.Enable();
        }
        private void OnDisable()
        {
            Inventory.onMovementSpeedUpdate -= OnMovementSpeedUpdated;
            walking.Disable();
        }

        public void OnMovementSpeedUpdated(int value)
        {
            movementSpeed += value;
        }
        
        void Start()
        {
            _player = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _walkingDirection = walking.ReadValue<Vector2>();
        }

        void FixedUpdate()
        {
            _player.MovePosition(_player.position + _walkingDirection * (Time.deltaTime * movementSpeed));
        }
    }
}