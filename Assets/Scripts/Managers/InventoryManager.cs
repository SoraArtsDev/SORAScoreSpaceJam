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

using UnityEngine.UI;
using TMPro;

namespace Sora.Managers
{
    /// You may delete all of the stuff inside here. 
    /// Just remember to stick to the formating
    public class InventoryManager : Singleton<InventoryManager>
    {
        public IntVariable playerTreats;

        [Header("UI Variables")]
        [SerializeField] private Button[] catButtons;
        [SerializeField] private TMP_Text treatsText;

        public void OnButtonClick(int index, int cost)
        {
            if (playerTreats.value > cost)
            {
                for (int i = 0; i < catButtons.Length; ++i)
                {
                    if (index != i)
                    {
                        catButtons[i].interactable = false;
                    }
                }
            }            
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