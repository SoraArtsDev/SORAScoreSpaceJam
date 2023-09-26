using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sora.Game;
using TMPro;

namespace Sora
{
    public class DeadEyeUI : MonoBehaviour
    {
        public GameObject DeadEyePanel;
        public GameObject TargetLockButton;
        public GameObject DeadEyePopUpText;
        public AudioSource AudioSourceObj;
        private GameObject[] gameObjects;
        public GameObject DeadEyeFill;

        public bool bTimerIsRunning = false;
        public float timeRemaining = 3.0f;
        public TMP_Text TimerText;
        // Start is called before the first frame update
        void Start()
        {
            bTimerIsRunning = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (bTimerIsRunning)
            {
                if (timeRemaining > 0.0f)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                    gameObject.GetComponent<Button>().enabled = false;
                    DeadEyeFill.SetActive(false);
                }
                else
                {
                    Debug.Log("Dead Eye Timer out!");
                    timeRemaining = 0;
                    gameObject.GetComponent<Button>().enabled = true;
                    DeadEyeFill.SetActive(true);
                    bTimerIsRunning = true;
                }
            }
            
        }

        void DisplayTime(float a_timeToDisplay)
        {
            a_timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(a_timeToDisplay / 60.0f);
            float seconds = Mathf.FloorToInt(a_timeToDisplay % 60);
            TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        public void OnLockTarget()
        {
            DeadEyePopUpText.SetActive(true);

            AfterDeadEye();
        }

        public void OnDeadEyeButton()
        {
            DeadEyePanel.SetActive(true);
            DetectAllEnemies();
            AudioSourceObj.Play();
        
            Time.timeScale = 0.2f;
        }

        public void AfterDeadEye()
        {
            TargetLockButton.SetActive(false);
            StartCoroutine(DestroyAllEnemiesInScene());

            Time.timeScale = 1.0f;
            DeadEyePanel.SetActive(false);
            TargetLockButton.SetActive(true);
            timeRemaining = 10.0f;
        }

        public void DetectAllEnemies()
        {
            gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < gameObjects.Length; ++i)
            {
                gameObjects[i].transform.GetChild(1).transform.Find("TargetLock").gameObject.SetActive(true);
            }
        }

        IEnumerator DestroyAllEnemiesInScene()
        {
            for (int i = 0; i < gameObjects.Length; ++i)
            {
                gameObjects[i].transform.GetChild(0).gameObject.SetActive(false);
                gameObjects[i].GetComponent<Enemy>().enabled = false;
                gameObjects[i].transform.Find("FX_Death_AA").gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(1.5f);

            for (int i = 0; i < gameObjects.Length; ++i)
            {
                Destroy(gameObjects[i]);
            }
        }
    }
}
