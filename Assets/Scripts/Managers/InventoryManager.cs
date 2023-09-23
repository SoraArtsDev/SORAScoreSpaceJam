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

namespace Sora.Managers
{
    /// You may delete all of the stuff inside here. 
    /// Just remember to stick to the formating
    public class InventoryManager : Singleton<InventoryManager>
    {
        [SerializeField] private Button[] catButtons;

        public void OnButtonClick(int index, int cost)
        {
            if (ScoreManager.instance.playerTreats.value > cost)
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
    }
}