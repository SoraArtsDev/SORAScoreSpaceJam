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
    public class TowerUIInfo : MonoBehaviour
    {
        public Tower tower;
        public GameObject cat1;
        public GameObject cat2;
        public GameObject cat3;
        public static int totalTowers;

        public void SetTower(GameObject obj, GameObject cat1, GameObject cat2, GameObject cat3)
        {
            totalTowers++;
            gameObject.name = gameObject.name + "" + totalTowers;
            tower = obj.GetComponent<Tower>();
            this.cat1 = cat1;
            this.cat2 = cat2;
            this.cat3 = cat3;
        }

        public TowerData GetData()
        {
            tower = GetComponentInChildren<Tower>();
            return tower.data;
        }
    }
}