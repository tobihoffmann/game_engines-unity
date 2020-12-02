using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LogoAnimation : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] sprites;
        
        [SerializeField]
        private int framesPerSprite;

        
        private int _index = 0;
        private Image _image;
        private int _frame;
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
         Animate();
        }

        private void Animate()
        {
            if (_index == sprites.Length) return;
            _frame++;
            if (_frame < framesPerSprite) return;
            _image.sprite = sprites[_index];
            _frame = 0;
            _index++;
            if (_index >= sprites.Length)
            {
                _index = 0;
            }
        }
        
    }
}
