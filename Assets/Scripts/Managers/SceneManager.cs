// Developed by Pluto
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sora.Events;

namespace Sora.Managers
{
    /// You may delete all of the stuff inside here. 
    /// Just remember to stick to the formating
    public class SceneManager : Singleton<SceneManager>
    {
        [SerializeField] private SoraEvent gameStartedEvent;
        [SerializeField] private int nextSceneIndex;
        
        [Header("Loading Bar")]
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private UnityEngine.UI.Image loadingBar;

        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void OnPressingStartGame()
        {
            gameStartedEvent.InvokeEvent();
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }

        public void LoadGameScene(int sceneIndex)
        {
            StartCoroutine(LoadSceneAsync(sceneIndex));
        }

        private IEnumerator LoadSceneAsync(int sceneIndex)
        {
            AsyncOperation loadingOperation;

            loadingScreen.SetActive(true);
            loadingOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);

            while (!loadingOperation.isDone)
            {
                float barProgress = Mathf.Clamp01(loadingOperation.progress / 0.9f);

                loadingBar.fillAmount = barProgress;

                yield return null;
            }
        }
    }
}