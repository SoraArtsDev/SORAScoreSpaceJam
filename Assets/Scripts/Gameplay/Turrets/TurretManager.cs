// Developed by Pluto
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sora 
{
    /// You may delete all of the stuff inside here. 
    /// Just remember to stick to the formating
    public class TurretManager : MonoBehaviour
    {
        public TextAsset turretFile;
        public Dictionary<string, TurretData> turrets;
        void Start()
        {
            turretFile = Resources.Load<TextAsset>("turretData");
            turrets = new Dictionary<string, TurretData>();

            ReadTurretData();
  
        }

        void ReadTurretData()
        {
            Turrets turretsData = JsonUtility.FromJson<Turrets>(turretFile.text);
            for(int i = 0; i < turretsData.turrets.Length; ++i)
            {
                ApplyUpgrades(ref turretsData.turrets[i]);
                turrets[turretsData.turrets[i].name] = turretsData.turrets[i];
            }
        }


        void ApplyUpgrades(ref TurretData data)
        {
            data.upgradeCost = data.costUpgrades[data.level].data[data.upgradeLevel];
            data.damage = data.damageUpgrades[data.level].data[data.upgradeLevel];
            data.attackRate = data.attackRateUpgrades[data.level].data[data.upgradeLevel];
            data.areaOfImpact = data.areaOfImpactUpgrades[data.level].data[data.upgradeLevel];
            data.buildCost += data.upgradeCost;
            data.sellCost = (int)(data.sellRate[data.level] * data.buildCost);
            data.spriteName = data.sprite[data.level];
            data.upgradeLevel++;//we set to next upgrade because that's what we want to show and check against and then apply
            if(data.upgradeLevel>=5)
            {
                //max upgrade set to next level;
                data.upgradeLevel = 0;
                data.level++;
            }
        }
    }
}