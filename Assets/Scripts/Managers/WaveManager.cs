// Developed by Pluto
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sora.Game;

namespace Sora.Managers
{    
    public class WaveData
    {
        public int waveCount;
        public int enemyTypes;
        public KeyValuePair<EEnemyType, int>[] enemyCountbyType;
        public int[] entryPoint;
    }

    public class WaveManager : Singleton<WaveManager>
    {
        [Header("UI Variables")]
        public TMP_Text waveCountText;
        public Image waveProgressBar;

        public Events.SoraEvent startWaveEvent;
        public EnemySpawner enemySpawner;

        private int waveCount;

        private void OnEnable()
        {
            Random.InitState(System.DateTime.Now.Second);
            waveCount = 0;
        }

        public void OnWaveStart()
        {
            waveCount++;
            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            wd.enemyTypes = 1;
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            for(int i = 0; i < wd.enemyTypes; ++i)
            {
                //EEnemyType e = (EEnemyType)Random.Range(0, (int)EEnemyType.COUNT);
                //wd.enemyCountbyType[i] = new KeyValuePair<Game.EEnemyType, int>(e, 10);
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(EEnemyType.MOUSE, 10);
                wd.entryPoint[i] = Random.Range(0, 2);
            }
            startWaveEvent.InvokeEvent(this, wd);
        }
    }
}