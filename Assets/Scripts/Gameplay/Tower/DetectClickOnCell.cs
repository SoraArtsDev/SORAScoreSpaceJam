// Developed by Sora
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sora.Managers;

namespace Sora.Game
{
    public class DetectClickOnCell : MonoBehaviour
    {
        [SerializeField] private float positionYOffset;
        [SerializeField] private bool isSuperCell;

        private void OnMouseDown()
        {
            MapManager.instance.DisableHighlights(MapManager.instance.currentTower.transform.position);
            
            if(isSuperCell)
            {

            }

            MapManager.instance.currentTower = null;
            InventoryManager.instance.ResetInventoryAccess();

        }

        private void OnMouseOver()
        {
            MapManager.instance.currentTower.transform.position = new Vector3(transform.position.x, positionYOffset, transform.position.z);
        }
    }
}