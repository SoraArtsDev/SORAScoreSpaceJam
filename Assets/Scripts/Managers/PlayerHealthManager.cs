// Developed by Sora
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sora.Managers
{
    /// You may delete all of the stuff inside here. 
    /// Just remember to stick to the formating
    public class PlayerHealthManager : Singleton<PlayerHealthManager>
    {
        [SerializeField] private int playerHealth;
        [SerializeField] private Image healthBarFill;
        [SerializeField] private Animator healthBarAnimator;
        [SerializeField] private Events.SoraEvent gameOverEvent;

        private int initialHealth;

        private void OnEnable()
        {
            initialHealth = playerHealth;
            healthBarFill.type = Image.Type.Filled;
            healthBarFill.fillAmount = 1.0f;
        }

        public void TakeDamage()
        {
            playerHealth--;
            UpdateHealthBar();
            healthBarAnimator.Play("punch", 0);

            if(playerHealth <= 0)
            {
                gameOverEvent.InvokeEvent();
            }
        }

        private void UpdateHealthBar()
        {
            float ph = playerHealth;
            float ih = initialHealth;
            healthBarFill.fillAmount = ph / ih;
        }
    }
}