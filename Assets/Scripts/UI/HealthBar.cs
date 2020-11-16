using System;
using System.Linq;
using Entity.Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        //public string SpriteLocation = "Sprites/PlayerHungerStates_Sprite";

        [SerializeField] private int currentHealth = 5;

        [SerializeField] private int maxHealth;

        [SerializeField] private Image[] hearts;

        [SerializeField] private Sprite fullHeart;

        [SerializeField] private Sprite emptyHeart;

        private void OnEnable()
        {
            PlayerState.OnPlayerHitPointsUpdate += OnHealthUpdated;
            //TODO: PlayerState.OnPlayerPowerUpsUpdate += OnMaxHealthUpdated
        }

        private void OnDisable()
        {
            PlayerState.OnPlayerHitPointsUpdate -= OnHealthUpdated;
            //TODO: PlayerState.OnPlayerPowerUpsUpdate -= OnMaxHealthUpdated
        }

        private void OnHealthUpdated(int newValue)
        {
            currentHealth = newValue;
            
            // Clamp current health in case of overhealing
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            // Clamp current health in case of damaging Entity below 0
            if (currentHealth < 0) currentHealth = 0;
            
            
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].sprite = (i < currentHealth) ? fullHeart : emptyHeart;
            }
        }

        private void Update()
        {
            // Display/Change Max Health
            // TODO: Has to subscribe to OnPlayerPowerUpsUpdate in PlayerState.cs once implemented
            for (int i = 0; i < hearts.Length; i++) hearts[i].enabled = i < maxHealth;
        }
    }
}