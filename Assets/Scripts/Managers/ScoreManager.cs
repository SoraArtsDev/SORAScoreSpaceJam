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
using TMPro;

namespace Sora.Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [Header("Variable References")]
        [SerializeField] private IntVariable playerScore;
        public IntVariable playerTreats;

        [Header("UI Variables")]
        [SerializeField] private TMP_Text treatsText;

        private void OnEnable()
        {

        }

        public bool SpendTreats(int amount)
        {
            if (playerTreats.value >= amount)
            {
                playerTreats.value -= amount;
                treatsText.text = playerTreats.value.ToString();
                return true;
            }

            return false;
        }

        public void AddTreats(int amount)
        {
            playerTreats.value += amount;
            treatsText.text = playerTreats.value.ToString();
        }
    }
}