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
        //public Transform[] highlightCells;
        public Dictionary<Vector3, GameObject> availableCells = new Dictionary<Vector3, GameObject>();

        [SerializeField] private InputSystem.GameplayInputReader inputReader;
        public GameObject currentTower;

        private void OnEnable()
        {
            //highlightCells = new Transform[allCells.transform.childCount];            

            for(int i = 0; i < allCells.transform.childCount; ++i)
            {
                availableCells.Add(allCells.transform.GetChild(i).transform.position, allCells.transform.GetChild(i).gameObject);
                //availableCells.Add(highlightCells[i].position, highlightCells[i].gameObject);
            }

            inputReader.cancelTowerEvent += OnCancellingTowerSelection;
        }

        public void HighlightAvailblePlacementCells()
        {
            allCells.SetActive(true);
        }

        public void DisableHighlights(Vector3 position)
        {
            allCells.SetActive(false);
            UpdateAvailableCells(position);
        }

        public void DisableHighlights()
        {
            allCells.SetActive(false);
        }

        public void UpdateAvailableCells(Vector3 cell)
        {
            availableCells[cell].SetActive(false);
        }

        private void OnCancellingTowerSelection()
        {
            DisableHighlights();
            Destroy(currentTower);
            currentTower = null;
            InventoryManager.instance.ResetInventoryAccess();
        }

        public void EnableMouseDetection(Component invoker, object data)
        {
            currentTower = (GameObject)data;
        }
    }
}