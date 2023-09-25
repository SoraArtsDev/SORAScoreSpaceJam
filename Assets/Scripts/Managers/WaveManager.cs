// Developed by Sora
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
        public Button waveStartButton;
        public GameObject waveWarningUI;
        public TMP_Text waveWarningText;
        public int autoWaveDuration;


        [Space]
        public Events.SoraEvent startWaveEvent;

        private int waveCount;
        private int autoWaveCD;

        private void OnEnable()
        {
            Random.InitState(System.DateTime.Now.Second);
            autoWaveCD = autoWaveDuration;
            waveCount = 1;
        }

        public void StartAutoWaveCountdown()
        {
            StartCoroutine(AutoWaveCountdown());
        }

        private IEnumerator AutoWaveCountdown()
        {
            waveWarningUI.SetActive(true);
            while (autoWaveDuration >= 0)
            {
                waveWarningText.text = "Starting next Wave in: " + autoWaveDuration;
                autoWaveDuration--;
                yield return new WaitForSecondsRealtime(1.0f);
            }

            waveStartButton.interactable = false;
            OnWaveStart();
            waveWarningUI.SetActive(false);
            autoWaveDuration = autoWaveCD;
        }

        public void OnWaveStart()
        {
            // reset countdown coroutine
            StopAllCoroutines();
            waveWarningUI.SetActive(false);
            autoWaveDuration = autoWaveCD;

            waveCountText.text = "Wave: " + waveCount.ToString();
            waveStartButton.interactable = false;

            switch (waveCount)
            {
                case 1:
                    Wave1();
                    return;
                case 2:
                    StartCoroutine(Wave2());
                    return;
                case 3:
                    StartCoroutine(Wave3());
                    return;
                case 4:
                    StartCoroutine(Wave4());
                    return;
                case 5:
                    StartCoroutine(Wave5());
                    return;
                case 6:
                    Wave6();
                    return;
                case 7:
                    StartCoroutine(Wave7());
                    return;
                case 8:
                    Wave8();
                    return;
                case 9:
                    Wave9();
                    return;
                case 10:
                    Wave10();
                    return;
                default:
                    break;
            }

            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            //TODO:: BALANCE
            wd.enemyTypes = Random.Range(3, Mathf.Max((int)(waveCount * 0.4f), 6));
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            Random.InitState(System.DateTime.Now.Millisecond);
            for (int i = 0; i < wd.enemyTypes; ++i)
            {                
                EEnemyType e = (EEnemyType)Random.Range(0, (int)EEnemyType.COUNT);
                //TODO:: BALANCE
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, Random.Range((int)(waveCount * 0.3f), (int)(waveCount * 0.4f)));
                
                if(waveCount <= 15)
                    wd.entryPoint[i] = Random.Range(0, 2);
                else
                {
                    int rand = Random.RandomRange(0, 10);
                    if (rand > 4)
                        wd.entryPoint[i] = Random.Range(0, 2);
                    else
                        wd.entryPoint[i] = Random.Range(0, 3);
                }
            }
            startWaveEvent.InvokeEvent(this, wd);

            waveCount++;
        }

        private void Wave1()
        {
            waveCount++;
            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            wd.enemyTypes = 1;
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = EEnemyType.MOUSE;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 3);
                wd.entryPoint[i] = 0;
            }
            startWaveEvent.InvokeEvent(this, wd);
        }

        private IEnumerator Wave2()
        {
            waveCount++;
            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            wd.enemyTypes = 1;
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = EEnemyType.MOUSE;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 4);
                wd.entryPoint[i] = 0;
            }
            startWaveEvent.InvokeEvent(this, wd);

            yield return new WaitForSecondsRealtime(6.0f);

            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];
            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = EEnemyType.MOUSE;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 2);
                wd.entryPoint[i] = 1;
            }
            startWaveEvent.InvokeEvent(this, wd);
        }

        private IEnumerator Wave3()
        {
            waveCount++;
            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            wd.enemyTypes = 2;
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e;
                if (i == 0)
                    e = EEnemyType.MOUSE;
                else
                    e = EEnemyType.HEDGEHOG;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 2);
                wd.entryPoint[i] = 0;
            }
            startWaveEvent.InvokeEvent(this, wd);

            yield return new WaitForSecondsRealtime(6.0f);


            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e;
                if (i == 0)
                    e = EEnemyType.MOUSE;
                else
                    e = EEnemyType.HEDGEHOG;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 2);
                wd.entryPoint[i] = 1;
            }
            startWaveEvent.InvokeEvent(this, wd);
        }

        private IEnumerator Wave4()
        {
            waveCount++;
            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            wd.enemyTypes = 2;
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e;
                if (i == 0)
                    e = EEnemyType.MOUSE;
                else
                    e = EEnemyType.PIGEON;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 5);
                wd.entryPoint[i] = 0;
            }
            startWaveEvent.InvokeEvent(this, wd);

            yield return new WaitForSecondsRealtime(5.0f);

            wd.enemyTypes = 1;
            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = EEnemyType.PIGEON;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 3);
                wd.entryPoint[i] = 1;
            }
            startWaveEvent.InvokeEvent(this, wd);
        }

        private IEnumerator Wave5()
        {
            waveCount++;
            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            wd.enemyTypes = 1;
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = EEnemyType.MOUSE;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 4);
                wd.entryPoint[i] = 0;
            }
            startWaveEvent.InvokeEvent(this, wd);

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = EEnemyType.TORTOISE;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 2);
                wd.entryPoint[i] = Random.Range(0, 2);
            }
            startWaveEvent.InvokeEvent(this, wd);

            yield return new WaitForSecondsRealtime(6.0f);

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = EEnemyType.MOUSE;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 3);
                wd.entryPoint[i] = Random.Range(0, 2);
            }
            startWaveEvent.InvokeEvent(this, wd);
        }

        private void Wave6()
        {
            waveCount++;
            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            wd.enemyTypes = 2;
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e;
                if (i == 0)
                    e = EEnemyType.HEDGEHOG;
                else
                    e = EEnemyType.BEES;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 6);
                wd.entryPoint[i] = Random.Range(0,2);
            }
            startWaveEvent.InvokeEvent(this, wd);
        }

        private IEnumerator Wave7()
        {
            waveCount++;
            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            wd.enemyTypes = 1;
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = EEnemyType.SPIDER;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 6);
                wd.entryPoint[i] = 0;
            }
            startWaveEvent.InvokeEvent(this, wd);

            yield return new WaitForSecondsRealtime(8.0f);

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = EEnemyType.MOUSE;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 3);
                wd.entryPoint[i] = 2;
            }
            startWaveEvent.InvokeEvent(this, wd);
        }

        private void Wave8()
        {
            waveCount++;
            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            wd.enemyTypes = 1;
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = EEnemyType.BEES;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 15);
                wd.entryPoint[i] = 1;
            }
            startWaveEvent.InvokeEvent(this, wd);
        }

        private void Wave9()
        {
            waveCount++;
            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            wd.enemyTypes = 3;
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e;
                if (i == 0)
                    e = EEnemyType.MOUSE;
                else if (i == 1)
                    e = EEnemyType.HEDGEHOG;
                else
                    e = EEnemyType.SNAKE;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, Random.Range(4, 8));
                wd.entryPoint[i] = Random.Range(0, 1);
            }
            startWaveEvent.InvokeEvent(this, wd);
        }

        private void Wave10()
        {
            waveCount++;
            WaveData wd = new WaveData();
            wd.waveCount = waveCount;
            wd.enemyTypes = 3;
            wd.entryPoint = new int[wd.enemyTypes];
            wd.enemyCountbyType = new KeyValuePair<EEnemyType, int>[wd.enemyTypes];

            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e;
                if (i == 0)
                    e = EEnemyType.BEES;
                else if (i == 1)
                    e = EEnemyType.HEDGEHOG;
                else
                    e = EEnemyType.PIGEON;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, Random.Range(4, 8));
                wd.entryPoint[i] = Random.Range(0, 1);
            }
            startWaveEvent.InvokeEvent(this, wd);

            wd.enemyTypes = 2;
            for (int i = 0; i < wd.enemyTypes; ++i)
            {
                EEnemyType e = EEnemyType.TORTOISE;
                wd.enemyCountbyType[i] = new KeyValuePair<EEnemyType, int>(e, 3);
                wd.entryPoint[i] = 1;
            }
            startWaveEvent.InvokeEvent(this, wd);
        }
    }
}