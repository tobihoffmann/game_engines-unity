using System;
using Entity.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;


namespace UI
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField]
        private Texture2D cursor;
        
        [SerializeField]
        private Texture2D cursorClicked;
        
        private Controls _controls;

        private void Awake()
        {
            _controls = new Controls();
            ChangeCursor(cursor);
            Cursor.lockState = CursorLockMode.Confined;
        }
        
        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

        private void Start()
        {
            _controls.Player.Shoot.started += _ => ChangeCursor(cursorClicked);
            _controls.Player.Shoot.canceled += _ => ChangeCursor(cursor);
        }

        private void ChangeCursor(Texture2D cursorType)
        {
            Vector2 hotSpot = new Vector2(cursorType.width / 2, cursorType.height / 2);
            Cursor.SetCursor(cursorType, hotSpot, CursorMode.Auto); 
        }


    }
}
