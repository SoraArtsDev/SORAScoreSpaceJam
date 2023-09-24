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

using UnityEngine.UI;
using TMPro;

namespace Sora.Managers
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        public IntVariable playerTreats;
        public int initialTreats;

        [Header("UI Variables")]
        [SerializeField] private Button[] towerButtons;
        [SerializeField] private TMP_Text treatsText;
        [SerializeField] private TowerUIInfo[] towerProperties;
        [SerializeField] private Events.SoraEvent selectingTowerPosition;

        private void OnEnable()
        {
            playerTreats.value = initialTreats;
            treatsText.text = playerTreats.value.ToString();
        }

        public void OnButtonClick(int index)
        {
            GameObject tower = Instantiate(towerProperties[index].gameObject);
            TowerData td = tower.GetComponent<TowerUIInfo>().GetData();

            if (playerTreats.value > td.buildCost)
            {
                for (int i = 0; i < towerButtons.Length; ++i)
                {
                    if (index != i)
                    {
                        towerButtons[i].interactable = false;
                    }
                }

                MapManager.instance.HighlightAvailblePlacementCells();
                selectingTowerPosition.InvokeEvent();
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