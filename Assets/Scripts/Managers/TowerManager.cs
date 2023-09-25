// Developed by Sora
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sora.Managers
{

        public class TowerManager : Singleton<TowerManager>
        {
        private TextAsset towerDataFile;
        public Dictionary<TowerType, TowerData> towersData;
        private bool isCreated;

        void OnEnable()
        {
            isCreated = false;
            ReadTowerData();
        }

        public void ReadTowerData()
        {
            if (isCreated)
                return;

            isCreated = true;
            towerDataFile = Resources.Load<TextAsset>("towerData");
            towersData = new Dictionary<TowerType, TowerData>();

            ReadTowerData();
            Towers data = JsonUtility.FromJson<Towers>(towerDataFile.text);
            for(int i = 0; i < data.towers.Length; ++i)
            {
                ApplyUpgrades(ref data.towers[i]);
                towersData[data.towers[i].type] = data.towers[i];
            }
        }

        public void ApplyUpgrades(ref TowerData data)
        {
            data.upgradeCost = data.costUpgrades[data.level].data[data.upgradeLevel];
            data.damage = data.damageUpgrades[data.level].data[data.upgradeLevel];
            data.attackRate = data.attackRateUpgrades[data.level].data[data.upgradeLevel];
            data.areaOfImpact = data.areaOfImpactUpgrades[data.level].data[data.upgradeLevel];
            data.effectDuration = data.effectDurationUpgrades[data.level].data[data.upgradeLevel];
            data.effectMultiplier = data.effectMultiplierUpgrades[data.level].data[data.upgradeLevel];

            data.buildCost += data.upgradeCost;
            data.sellCost = (int)(data.sellRate[data.level]*.01*data.buildCost);
            data.spriteName = data.sprite[data.level];
            data.upgradeLevel++;//we set to next upgrade because that's what we want to show and check against and then apply
            if(data.upgradeLevel>5)
            {
                data.maxed[data.level] = true;
                //max upgrade set to next level;
                data.upgradeLevel = 0;
                data.level++;
                data.level = data.level >2 ? 2 : data.level;
            }
        }

        public TowerData GetTowerData(TowerType type)
        {
            return towersData[type];
        }
    }
}