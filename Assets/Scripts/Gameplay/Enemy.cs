// Developed by Pluto
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sora 
{
    /// You may delete all of the stuff inside here. 
    /// Just remember to stick to the formating
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Stats")]
        public float moveSpeed;
        public float healthPoints;

        private float maxHealthPoints;
        [SerializeField] private Image healthBar;

        private void OnValidate()
        {
            maxHealthPoints = healthPoints;            
        }

        private void OnEnable()
        {
            healthBar.fillAmount = 1.0f;
        }

        public void TakeDamage(int damage)
        {
            healthPoints -= damage;
            healthBar.fillAmount = healthPoints / maxHealthPoints;

            if (healthPoints <= 0)
                gameObject.SetActive(false);
        }
    }
}