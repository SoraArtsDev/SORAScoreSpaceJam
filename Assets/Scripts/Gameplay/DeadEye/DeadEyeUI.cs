using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sora.Game;

namespace Sora
{
    public class DeadEyeUI : MonoBehaviour
    {
        public GameObject DeadEyePanel;
        public GameObject TargetLockButton;
        public GameObject DeadEyePopUpText;
        public AudioSource AudioSourceObj;
        public AudioClip a_Clip;
        private GameObject[] gameObjects;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnLockTarget()
        {
            DeadEyePopUpText.SetActive(true);
            // AudioSourceObj.PlayOneShot(a_Clip);

            AfterDeadEye();
        }

        public void OnDeadEyeButton()
        {
            DeadEyePanel.SetActive(true);
            DetectAllEnemies();
            Time.timeScale = 0.2f;
        }

        public void AfterDeadEye()
        {
            TargetLockButton.SetActive(false);
            StartCoroutine(DestroyAllEnemiesInScene());

            Time.timeScale = 1.0f;
            DeadEyePanel.SetActive(false);

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
