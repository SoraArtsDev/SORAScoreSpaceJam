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


        public Sprite[] Sniper;//= { "ALL_MUG_0","ALL_MUG_1","ALL_MUG_2"};
        public Sprite[] Lazer;//=// { "ALL_MUG_3","ALL_MUG_4","ALL_MUG_5"};
        public Sprite[] Mortar; //= { "ALL_MUG_6","ALL_MUG_7","ALL_MUG_8"};
        public Sprite[] Freeze;//= { "ALL_MUG_9","ALL_MUG_10","ALL_MUG_11"};
        public Sprite[] Turret;// { "ALL_MUG_12","ALL_MUG_13","ALL_MUG_14"};
        public Sprite[] Fire;//= { "ALL_MUG_15","ALL_MUG_16","ALL_MUG_17"};


        public TowerData td;

        private void OnEnable()
        {
            playerTreats.value = initialTreats;
            treatsText.text = playerTreats.value.ToString();
        }

        public void OnButtonClick(int index)
        {
            GameObject tower = Instantiate(towerProperties[index].gameObject);
            tower.transform.position = new Vector3(40, 12, 30);
            td = towerProperties[index].GetData();

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
            }
            else
                return;

            blockButtonsUI.SetActive(true);
            selectingTower.value = true;

            MapManager.instance.HighlightAvailblePlacementCells();
            selectingTowerEvent.InvokeEvent(this, tower);
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