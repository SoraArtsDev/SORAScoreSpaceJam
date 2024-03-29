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
        public Button upgradeBtbtn;
        public Button buyBtn;
        public TMPro.TextMeshProUGUI txt;
        public Image img;
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

        public void MaxedOut()
        {
            lvl1.txt.text = "Max";
            lvl2.txt.text = "Max";
            lvl3.txt.text = "Max";

            lvl1.upgradeBtbtn.gameObject.SetActive(false);
            lvl2.upgradeBtbtn.gameObject.SetActive(false);
            lvl3.upgradeBtbtn.gameObject.SetActive(false);
        }

        Sprite GetImage(int level, TowerType type)
        {
            switch(type)
            {
                case TowerType.E_FlameThrower:
                    {

                        if (level == 0)
                        {
                            return Managers.InventoryManager.instance.Fire[0];
                        }
                        else if (level == 1)
                        {
                            return Managers.InventoryManager.instance.Fire[1];

                        }
                        else
                        {
                            return Managers.InventoryManager.instance.Fire[2];

                        }
                    }
                    break;
                case TowerType.E_Freeze:
                    {

                        if (level == 0)
                        {
                            return Managers.InventoryManager.instance.Freeze[0];
                        }
                        else if (level == 1)
                        {
                            return Managers.InventoryManager.instance.Freeze[1];

                        }
                        else
                        {
                            return Managers.InventoryManager.instance.Freeze[2];

                        }
                    }
                    break;
                case TowerType.E_Lazer:
                    {

                        if (level == 0)
                        {
                            return Managers.InventoryManager.instance.Lazer[0];
                        }
                        else if (level == 1)
                        {
                            return Managers.InventoryManager.instance.Lazer[1];

                        }
                        else
                        {
                            return Managers.InventoryManager.instance.Lazer[2];

                        }
                    }
                    break;
                case TowerType.E_Turret:
                    {

                        if (level == 0)
                        {
                            return Managers.InventoryManager.instance.Turret[0];
                        }
                        else if (level == 1)
                        {
                            return Managers.InventoryManager.instance.Turret[1];

                        }
                        else
                        {
                            return Managers.InventoryManager.instance.Turret[2];

                        }
                    }
                    break;
                case TowerType.E_Mortar:
                    {

                        if (level == 0)
                        {
                            return Managers.InventoryManager.instance.Mortar[0];
                        }
                        else if (level == 1)
                        {
                            return Managers.InventoryManager.instance.Mortar[1];

                        }
                        else
                        {
                            return Managers.InventoryManager.instance.Mortar[2];

                        }
                    }
                    break;
                case TowerType.E_Sniper:
                    {

                        if (level == 0)
                        {
                            return Managers.InventoryManager.instance.Sniper[0];
                        }
                        else if (level == 1)
                        {
                            return Managers.InventoryManager.instance.Sniper[1];

                        }
                        else
                        {
                            return Managers.InventoryManager.instance.Sniper[2];

                        }
                    }
                    break;
            }
            return null;
        }
        public void Check(int cost, int level, int upgradeLevel,TowerType type)
        {
            bool canUpgrade = cost <= Managers.InventoryManager.instance.playerTreats.value;

            if(level==0)
            {
                //set info for lvl1 buttons
                lvl2.upgradeBtbtn.gameObject.SetActive(true);
                lvl1.upgradeBtbtn.interactable = canUpgrade;
                lvl1.txt.gameObject.SetActive(true);
                lvl1.txt.text = cost.ToString();
                lvl1.img.enabled = true;
                lvl1.img.sprite = GetImage(level, type);

                //disable others
                lvl2.upgradeBtbtn.interactable = false;
                lvl3.upgradeBtbtn.interactable = false;

                lvl2.txt.gameObject.SetActive(false);
                lvl3.txt.gameObject.SetActive(false);

                lvl2.buyBtn.gameObject.SetActive(false);
                lvl3.buyBtn.gameObject.SetActive(false);

                lvl2.upgradeBtbtn.gameObject.SetActive(false);
                lvl3.upgradeBtbtn.gameObject.SetActive(false);

                lvl2.img.enabled = false;
                lvl3.img.enabled = false;
            }
            else if(level==1)
            {
                //set info for lvl2 buttons
                if (upgradeLevel>0)
                {
                    lvl2.buyBtn.gameObject.SetActive(false);
                    lvl2.upgradeBtbtn.gameObject.SetActive(true);
                    lvl2.upgradeBtbtn.interactable = canUpgrade;
                }
                else
                {
                    lvl2.buyBtn.gameObject.SetActive(true);
                    lvl2.buyBtn.interactable = canUpgrade;
                    lvl2.upgradeBtbtn.interactable = false;
                    lvl2.upgradeBtbtn.gameObject.SetActive(false);
                }

                lvl2.txt.gameObject.SetActive(true);
                lvl2.txt.text = cost.ToString();

                lvl1.txt.text = "Max";
                lvl1.img.enabled = true;
                lvl2.img.enabled = true;
                lvl1.img.sprite = GetImage(0, type);
                lvl2.img.sprite = GetImage(1, type);
                //disable others
                //lvl1.txt.gameObject.SetActive(false);
                lvl3.txt.gameObject.SetActive(false);

                lvl3.buyBtn.gameObject.SetActive(false);

                lvl1.upgradeBtbtn.gameObject.SetActive(false);
                lvl3.upgradeBtbtn.gameObject.SetActive(false);
                lvl3.img.enabled = false;
            }
            else if(level == 2)
            {
                //set info for lvl3 buttons
                if (upgradeLevel > 0)
                {
                    lvl3.buyBtn.gameObject.SetActive(false);
                    lvl3.upgradeBtbtn.gameObject.SetActive(true);
                    lvl3.upgradeBtbtn.interactable = canUpgrade;
                }
                else
                {
                    lvl3.buyBtn.gameObject.SetActive(true);
                    lvl2.buyBtn.interactable = canUpgrade;
                    lvl3.upgradeBtbtn.interactable = false;
                    lvl3.upgradeBtbtn.gameObject.SetActive(false);
                }

                lvl3.txt.gameObject.SetActive(canUpgrade);
                lvl3.txt.text = cost.ToString();


                //disable others
                lvl1.txt.text = "Max";
                lvl2.txt.text = "Max";

                lvl1.img.enabled = true;
                lvl2.img.enabled = true;
                lvl3.img.enabled = true;
                lvl1.img.sprite = GetImage(0, type);
                lvl2.img.sprite = GetImage(1, type);
                lvl3.img.sprite = GetImage(2, type);
                //lvl1.txt.gameObject.SetActive(false);
                //lvl2.txt.gameObject.SetActive(false);

                lvl2.buyBtn.gameObject.SetActive(false);

                lvl1.upgradeBtbtn.gameObject.SetActive(false);
                lvl2.upgradeBtbtn.gameObject.SetActive(false);
            }
            else
            {
                //show maxed up stats
            }
        }
    }

    public class Clickables : MonoBehaviour
    {
        private UnityEvent clickEvent;
        private Button.ButtonClickedEvent sellButtonEvent;
        private Button.ButtonClickedEvent upgradeButtonEvent;
        private Button.ButtonClickedEvent buyButtonEvent;
        private UpgradeUI upgradeUI;
        private Transform upgradeUITransform;
        private TowerUIInfo towerUIInfo;
        private static Clickables selectedClickable;





        public bool setIsBeingPlaced;
        private void Start()
        {
            selectedClickable = null;
            clickEvent = new UnityEvent();
            upgradeButtonEvent = new Button.ButtonClickedEvent();
            sellButtonEvent = new Button.ButtonClickedEvent();
            buyButtonEvent = new Button.ButtonClickedEvent();

            upgradeUI = new UpgradeUI();
            clickEvent.AddListener(this.OnClick);
            sellButtonEvent.AddListener(SellClicked);
            upgradeButtonEvent.AddListener(UpgradeClicked);
            buyButtonEvent.AddListener(BuyClicked);
            towerUIInfo = gameObject.GetComponent<TowerUIInfo>();

            //Debug.Log("Stored"+towerUIInfo.gameObject.name);
            GeUIReference();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !setIsBeingPlaced)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Towers" && (hit.collider.gameObject == towerUIInfo.cat1 || hit.collider.gameObject == towerUIInfo.cat2 || hit.collider.gameObject == towerUIInfo.cat3))
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    selectedClickable = gameObject.GetComponent<Clickables>();
                    clickEvent.Invoke();
                }
                else if(hit.collider != null && hit.collider.tag != "Towers" && hit.collider.tag != "UpgradeUI" && hit.collider.tag != "Untagged")
                {
                   // Debug.Log(hit.collider.gameObject.name);
                    upgradeUITransform.gameObject.SetActive(false);
                    selectedClickable = null;
                }
            }
        }

        void OnClick()
        {
            upgradeUITransform.gameObject.SetActive(true);
            var data = towerUIInfo.GetData();
            if (!data.maxed[data.level])
            {
               // Debug.Log("Called selectedClickable for " + towerUIInfo.gameObject.name);
                int cost = data.costUpgrades[data.level].data[data.upgradeLevel];
                upgradeUI.Check(cost,data.level, data.upgradeLevel, data.type);
                upgradeUI.sell.txt.text = data.sellCost.ToString();

            }
            else
            {
                //Debug.Log("Max");
                upgradeUI.MaxedOut();
                //show maxed stats
            }
        }

        void GeUIReference()
        {
            var canvas = GameObject.Find("Canvas").gameObject;
            upgradeUITransform = canvas.transform.Find("UpgradeUI");
            Transform lvl1 = upgradeUITransform.Find("Lvl1");
            upgradeUI.lvl1.upgradeBtbtn = lvl1.Find("btn").GetComponent<Button>();
            upgradeUI.lvl1.upgradeBtbtn.onClick  = upgradeButtonEvent;
            upgradeUI.lvl1.txt = lvl1.Find("text").GetComponent< TMPro.TextMeshProUGUI>();
            upgradeUI.lvl1.img = lvl1.Find("img").GetComponent<Image>();

           Transform lvl2 = upgradeUITransform.transform.Find("Lvl2");
            upgradeUI.lvl2.upgradeBtbtn = lvl2.Find("btn").GetComponent<Button>();
            upgradeUI.lvl2.upgradeBtbtn.onClick  = upgradeButtonEvent;
            upgradeUI.lvl2.buyBtn = lvl2.Find("buyBtn").GetComponent<Button>();
            upgradeUI.lvl2.buyBtn.onClick  = buyButtonEvent;
            upgradeUI.lvl2.txt = lvl2.Find("text").GetComponent<TMPro.TextMeshProUGUI>();
            upgradeUI.lvl2.img = lvl2.Find("img").GetComponent<Image>();

            Transform lvl3 = upgradeUITransform.transform.Find("Lvl3");
            upgradeUI.lvl3.upgradeBtbtn = lvl3.Find("btn").GetComponent<Button>();
            upgradeUI.lvl3.upgradeBtbtn.onClick  = upgradeButtonEvent;
            upgradeUI.lvl3.buyBtn = lvl3.Find("buyBtn").GetComponent<Button>();
            upgradeUI.lvl3.buyBtn.onClick = buyButtonEvent;
            upgradeUI.lvl3.txt = lvl3.Find("text").GetComponent<TMPro.TextMeshProUGUI>();
            upgradeUI.lvl3.img = lvl3.Find("img").GetComponent<Image>();

            Transform sell = upgradeUITransform.transform.Find("Sell");
            upgradeUI.sell.upgradeBtbtn = sell.Find("btn").GetComponent<Button>();
            upgradeUI.sell.upgradeBtbtn.onClick  = sellButtonEvent;
            upgradeUI.sell.txt = sell.Find("text").GetComponent<TMPro.TextMeshProUGUI>();

            upgradeUITransform.gameObject.SetActive(false);
        }

        static void UpgradeClicked()
        {
            // Debug.Log("selectedClickable for " + selectedClickable.towerUIInfo.gameObject.name);
            Managers.InventoryManager.instance.SpendTreats(selectedClickable.towerUIInfo.tower.data.upgradeCost);
            Managers.TowerManager.instance.ApplyUpgrades(ref selectedClickable.towerUIInfo.tower.data);
            selectedClickable.OnClick();
        }

        static void SellClicked()
        {
            //Debug.Log("Selling");
            Managers.InventoryManager.instance.AddTreats(selectedClickable.towerUIInfo.tower.data.sellCost);
            //selectedClickable.OnClick();
            selectedClickable.upgradeUITransform.gameObject.SetActive(false);
            Destroy(selectedClickable.towerUIInfo.gameObject);
        }

        static void BuyClicked()
        {
           // Debug.Log("Buying");
            Managers.InventoryManager.instance.SpendTreats(selectedClickable.towerUIInfo.tower.data.upgradeCost);
            Managers.TowerManager.instance.ApplyUpgrades(ref selectedClickable.towerUIInfo.tower.data);
            //Managers.TowerManager.instance.ApplyUpgrades(ref towerUIInfo.tower.data);
            if (selectedClickable.towerUIInfo.tower.data.level == 1)
            {
                selectedClickable.towerUIInfo.cat1.SetActive(false);
                selectedClickable.towerUIInfo.cat2.SetActive(true);

            }
            else if (selectedClickable.towerUIInfo.tower.data.level == 2)
            {
                selectedClickable.towerUIInfo.cat2.SetActive(false);
                selectedClickable.towerUIInfo.cat3.SetActive(true);

            }
            selectedClickable.OnClick();
        }

        public void JustPlacedTower()
        {
            setIsBeingPlaced = true;
            transform.Find("Tower").GetComponent<Tower>().isPlaced = true;
            StartCoroutine(EnableClickables());
        }

        IEnumerator EnableClickables()
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(.1f);
            setIsBeingPlaced = false;
        }
    }
}