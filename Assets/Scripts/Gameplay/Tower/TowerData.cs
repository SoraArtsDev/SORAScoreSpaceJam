// Developed by Sora
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//

/*{
"Towers": [
{
  "Name": "Towers",
  "Level":0,
  "Upgrade":0,
  "DefaultDamage":1,
  "DefaultAttackRate":1,
  "DefaultAreaOfImpact":1,
  "Damage": [
    [1,2,3,4,5],
    [1,2,3,4,5],
    [1,2,3,4,5]
  ],
  "AttackRate": [
    [1,2,3,4,5],
    [1,2,3,4,5],
    [1,2,3,4,5]
  ],
  "AreaOfImpact": [
    [1,2,3,4,5],
    [1,2,3,4,5],
    [1,2,3,4,5]
  ],
  "Sprite": [
    "Test",
    "Test",
    "Test"
  ],
  "UpgradeCost": [
    [1,2,3,4,5],
    [1,2,3,4,5],
    [1,2,3,4,5]
  ],
  "SellRate": [
    40,
    42,
    43
  ]
}
]
}
*/

namespace Sora 
{
    [System.Serializable]
    public enum TowerType
    {
        E_Turret,
        E_Lazer,
        E_FlameThrower,
        E_Freeze,
        E_Mortar,
        E_Sniper,
    }

    [System.Serializable]
    public class Upgrades
    {
        public List<int> data;
    }

    [System.Serializable]
    public class TowerData
    {
        public string name;
        public string spriteName;
        public int level;
        public int upgradeLevel;
        public int damage;
        public int attackRate;
        public int areaOfImpact;
        public int upgradeCost;
        public int sellCost;
        public int buildCost;
        public bool[] maxed;
        public TowerType type;
        public List<string> sprite;
        public List<int> sellRate;
        public Upgrades[] damageUpgrades;
        public Upgrades[] attackRateUpgrades;
        public Upgrades[] areaOfImpactUpgrades;//1,42,5,6,7
        public Upgrades[] costUpgrades;

        public TowerData(TowerData data)
        {
            name = data.name;
            spriteName = data.spriteName;
            level = data.level;
            upgradeLevel = data.upgradeLevel;
            damage = data.damage;
            attackRate = data.attackRate;
            areaOfImpact = data.areaOfImpact;
            upgradeCost = data.upgradeCost;
            sellCost = data.sellCost;
            buildCost = data.buildCost;
            type = data.type;

            maxed = new bool[3];

            sprite = data.sprite;/// new List<string>();
            sellRate = data.sellRate;// new List<int>();

            damageUpgrades = data.damageUpgrades;// new Upgrades[3];
            attackRateUpgrades = data.attackRateUpgrades;// new Upgrades[3];
            areaOfImpactUpgrades = data.areaOfImpactUpgrades;//; new Upgrades[3];
            costUpgrades = data.costUpgrades;// Upgrades[3];

        }
    }

    [System.Serializable]
    public class Towers
    {
        public TowerData[] towers;
    }
}
