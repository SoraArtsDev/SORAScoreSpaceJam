// Developed by Pluto
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sora
{
    public class ButtonUI
    {
        public Button btn;
        public TMPro.TextMeshProUGUI txt;

 
    }
   
    public class UpgradeUI
    {
        public ButtonUI lvl1;
        public ButtonUI lvl2;
        public ButtonUI lvl3;
        public ButtonUI sell;

        public UpgradeUI()
        {
            lvl1 = new ButtonUI();
            lvl2 = new ButtonUI();
            lvl3 = new ButtonUI();
            sell = new ButtonUI();
        }


        public void Check(int cost, int level)
        {
           bool canUpgrade = cost <= Managers.InventoryManager.instance.playerTreats.value;

            if(level==0)
            {
                lvl1.btn.interactable = canUpgrade;
                lvl1.txt.gameObject.SetActive(canUpgrade);
                lvl1.txt.text = cost.ToString();

                lvl2.btn.interactable = false;
                lvl3.btn.interactable = false;
                lvl2.txt.gameObject.SetActive(false);
                lvl3.txt.gameObject.SetActive(false);
            }
            else if(level==1)
            {
                lvl2.btn.interactable = canUpgrade;
                lvl2.txt.gameObject.SetActive(canUpgrade);
                lvl2.txt.text = cost.ToString();

                lvl1.btn.interactable = false;
                lvl3.btn.interactable = false;
                lvl1.txt.gameObject.SetActive(false);
                lvl3.txt.gameObject.SetActive(false);
            }
            else if(level == 2)
            {
                lvl3.btn.interactable = canUpgrade;
                lvl3.txt.gameObject.SetActive(canUpgrade);
                lvl3.txt.text = cost.ToString();

                lvl1.btn.interactable = false;
                lvl2.btn.interactable = false;
                lvl1.txt.gameObject.SetActive(false);
                lvl2.txt.gameObject.SetActive(false);
            }
        }
    }

    public class Clickables : MonoBehaviour
    {
        private UnityEvent clickEvent;
        private UpgradeUI ui;
        private Transform upgradeUI;
        private TowerUIInfo towerUIInfo;
        private void Start()
        {
            clickEvent = new UnityEvent();
            ui = new UpgradeUI();
            clickEvent.AddListener(OnClick);

            towerUIInfo = GetComponent<TowerUIInfo>();
            GeUIReference();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Towers")
                {
                    clickEvent.Invoke();
                }
            }
        }

        void OnClick()
        {
            upgradeUI.gameObject.SetActive(true);
            var data = towerUIInfo.GetData();
            int cost = data.costUpgrades[data.level].data[data.upgradeLevel];
            ui.Check(cost,data.level);
            ui.sell.txt.text = data.sellCost.ToString();
        }

        void GeUIReference()
        {
            var canvas = GameObject.Find("Canvas").gameObject;
            upgradeUI = canvas.transform.Find("UpgradeUI");
            Transform lvl1 = upgradeUI.Find("Lvl1");
            ui.lvl1.btn = lvl1.Find("btn").GetComponent<Button>();
            ui.lvl1.txt = lvl1.Find("text").GetComponent< TMPro.TextMeshProUGUI>();

            Transform lvl2 = upgradeUI.transform.Find("Lvl2");
            ui.lvl2.btn = lvl2.Find("btn").GetComponent<Button>();
            ui.lvl2.txt = lvl2.Find("text").GetComponent<TMPro.TextMeshProUGUI>();

            Transform lvl3 = upgradeUI.transform.Find("Lvl3");
            ui.lvl3.btn = lvl3.Find("btn").GetComponent<Button>();
            ui.lvl3.txt = lvl3.Find("text").GetComponent<TMPro.TextMeshProUGUI>();

            Transform sell = upgradeUI.transform.Find("Sell");
            ui.sell.btn = sell.Find("btn").GetComponent<Button>();
            ui.sell.txt = sell.Find("text").GetComponent<TMPro.TextMeshProUGUI>();

            upgradeUI.gameObject.SetActive(false);
        }
    }
}