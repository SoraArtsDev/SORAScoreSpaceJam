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
        [SerializeField] private Events.SoraEvent selectingTowerEvent;
        [SerializeField] private BoolVariable selectingTower;
        [SerializeField] private GameObject blockButtonsUI;

        private void OnEnable()
        {
            playerTreats.value = initialTreats;
            treatsText.text = playerTreats.value.ToString();
        }

        public void OnButtonClick(int index)
        {
            GameObject tower = Instantiate(towerProperties[index].gameObject);
            tower.transform.position = new Vector3(40, 12, 30);
            TowerData td = towerProperties[index].GetData();
            blockButtonsUI.SetActive(true);
            selectingTower.value = true;


            if (playerTreats.value > td.buildCost)
            {
                for (int i = 0; i < towerButtons.Length; ++i)
                {
                    if (index != i)
                    {
                        towerButtons[i].interactable = false;
                    }
                    else
                        towerButtons[i].Select();
                }

                MapManager.instance.HighlightAvailblePlacementCells();
                selectingTowerEvent.InvokeEvent(this, tower);
            }
        }

        public void ResetInventoryAccess()
        {
            blockButtonsUI.SetActive(false);
            selectingTower.value = false;


            for (int i = 0; i < towerButtons.Length; ++i)
            {
                towerButtons[i].interactable = true;
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