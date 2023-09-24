// Developed by Sora
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sora.Game
{
    [System.Serializable]
    public class TransformArrayClass
    {
        public Transform[] array;

        public Transform this[int key]
        {
            get => array[key];
            set => array[key] = value;
        }
    }

    public class WaypointKeeper : Managers.Singleton<WaypointKeeper>
    {
        [SerializeField] private TransformArrayClass[] waypointTransforms;
        public List<List<Vector3>> waypoints = new List<List<Vector3>>();
        
        private void OnEnable()
        {
            DontDestroyOnLoad(this);

            InstantiateWaypointList();
            EnemySpawner.instance.InstantiateInitialEnemyPool();
        }

        private void InstantiateWaypointList()
        {
            for (int i = 0; i < waypointTransforms.Length; ++i)
            {
                waypoints.Add(new List<Vector3>());
                for (int j = 0; j < waypointTransforms[i].array.Length; ++j)
                    waypoints[i].Add(waypointTransforms[i][j].position);
            }
        }
    }
}