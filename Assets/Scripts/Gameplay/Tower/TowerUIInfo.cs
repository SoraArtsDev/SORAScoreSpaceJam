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
        private Tower tower;
        public void SetTower(GameObject obj)
        {
            tower = obj.GetComponent<Tower>();
        }

        public TowerData GetData()
        {
            return tower.data;
        }
    }
}