// Developed by Pluto
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
"Turrets": [
{
  "Name": "Turret",
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
    public class Upgrades
    {
        public List<int> data;
    }

    [System.Serializable]
    public class TurretData
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
        public List<string> sprite;
        public List<int> sellRate;
        public Upgrades[] damageUpgrades;
        public Upgrades[] attackRateUpgrades;
        public Upgrades[] areaOfImpactUpgrades;
        public Upgrades[] costUpgrades;
    }

    [System.Serializable]
    public class Turrets
    {
        public TurretData[] turrets;
    }
}
