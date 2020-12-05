using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;


namespace UI
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField][Tooltip("Input actions for walking")]
        private InputAction click;
        
        [SerializeField][Tooltip("Input actions for walking")]
        private InputAction release;
        
        [SerializeField]
        private Texture2D cursor;
        
        [SerializeField]
        private Texture2D cursorClicked;

        private void OnEnable()
        {
            click.Enable();
            release.Enable();
        }

        private void OnDisable()
        {
            click.Disable();
            release.Disable();
        }

        private void Awake()
        {
            ChangeCursor(cursor);
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void Update()
        {
            if (click.triggered)
            {
                ChangeCursor(cursorClicked);
            }
            
            if (release.triggered)
            {
                ChangeCursor(cursor);
            }
        }

        private void ChangeCursor(Texture2D cursorType)
        {
            Vector2 hotSpot = new Vector2(cursorType.width / 2, cursorType.height / 2);
            Cursor.SetCursor(cursorType, hotSpot, CursorMode.Auto); 
        }
    }
}
