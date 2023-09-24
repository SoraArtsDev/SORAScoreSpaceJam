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
    public enum ECellStatus
    {
        AVAILABLE,
        EQUIPPED,
        LOCKED
    }

    public class MapManager : Singleton<MapManager>
    {
        public GameObject allCells;
        public Transform[] highlightCells;
        public Dictionary<Vector3, GameObject> availableCells = new Dictionary<Vector3, GameObject>();

        private void OnEnable()
        {
            for(int i = 0; i < highlightCells.Length; ++i)
            {
                availableCells.Add(highlightCells[i].position, highlightCells[i].gameObject);
            }
        }

        public void HighlightAvailblePlacementCells()
        {
            allCells.SetActive(true);
        }

        public void UpdateAvailableCells(Transform cell)
        {
            availableCells[cell.position].SetActive(false);
            availableCells.Remove(cell.position);
        }
    }
}