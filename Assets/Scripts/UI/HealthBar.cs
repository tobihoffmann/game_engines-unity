using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        //public string SpriteLocation = "Sprites/PlayerHungerStates_Sprite";
        
        [SerializeField] 
        private int currentHealth = 5;
        
        [SerializeField] 
        private int maxHealth;

        [SerializeField] 
        private Image[] hearts;

        [SerializeField] 
        private Sprite fullHeart;
        
        [SerializeField] 
        private Sprite emptyHeart;
    
    
        // Start is called before the first frame update


        private void Update()
        {
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < currentHealth)
                {
                    hearts[i].sprite = fullHeart;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }
                if (i < maxHealth)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }                
            }
        }

        private void Awake()
        {
            
        }
    
        public void UpdateBar(int newValue)
        {
           

        }

        private void ChangeSprite()
        {
        }
    
    
    }
}
                     