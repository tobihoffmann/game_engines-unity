﻿using System;
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

        

        [SerializeField]
        private int maxHealth;

        [SerializeField]
        private Image[] hearts;
        
        [SerializeField]
        private Sprite fullHeart;
        
        [SerializeField]
        private Sprite emptyHeart;
        
        private int _currentHealth;

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

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        private void OnHealthUpdated(int newValue)
        {
            _currentHealth = newValue;
            
            // Clamp current health in case of overhealing
            if (_currentHealth > maxHealth) _currentHealth = maxHealth;
            // Clamp current health in case of damaging Entity below 0
            if (_currentHealth < 0) _currentHealth = 0;
            
            
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].sprite = (i < _currentHealth) ? fullHeart : emptyHeart;
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