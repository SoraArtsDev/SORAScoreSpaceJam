// Developed by Pluto
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
    [System.Serializable]
    public class PooledObject
    {
        public GameObject prefab;
        public int prefabCount;
        public EEnemyType enemyType;

        [HideInInspector] public GameObject[] pool;
    }

    public class EnemySpawner : MonoBehaviour
    {
        public float spawnInterval;
        public PooledObject[] allEnemies;
        private Dictionary<EEnemyType, PooledObject> poolData = new Dictionary<EEnemyType, PooledObject>();

        private void OnEnable()
        {
            InstantiateInitialEnemyPool();
        }

        public void InstantiateInitialEnemyPool()
        {
            for(int i = 0; i < allEnemies.Length; ++i)
            {
                allEnemies[i].pool = new GameObject[allEnemies[i].prefabCount];
                
                for (int j = 0; j < allEnemies[i].prefabCount; ++j)
                {
                    allEnemies[i].pool[j] = Instantiate(allEnemies[i].prefab, transform.position, Quaternion.identity);
                    allEnemies[i].pool[j].SetActive(false);
                }

                poolData.Add(allEnemies[i].enemyType, allEnemies[i]);
            }
        }

        public void OnWaveStart(Component invoker, object data)
        {
            WaveData wd = (WaveData)data;

            StartCoroutine(SpawnWave(wd));
            //Managers.WaveManager.instance.waveProgressBar
        }

        private IEnumerator SpawnWave(WaveData wd)
        {
            for(int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = wd.enemyCountbyType[i].Key;
                int count = wd.enemyCountbyType[i].Value;

                for(int j = 0; j < count; ++j)
                {
                    poolData[e].pool[j].GetComponent<Enemy>().entryPoint = wd.entryPoint[i];

                    poolData[e].pool[j].SetActive(true);
                    yield return new WaitForSecondsRealtime(spawnInterval);
                }
            }
        }
    }
}