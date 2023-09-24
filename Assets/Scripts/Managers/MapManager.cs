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

    public class MapCell
    {
        public ECellStatus status;
        //public 
    }

    public class MapManager : Singleton<MapManager>
    {
        public Dictionary<int, MapCell> allTowerCells = new Dictionary<int, MapCell>();
        public GameObject[] placementSpotHighlights;

        

        public void HighlightAvailblePlacementCells()
        {

        }
    }
}