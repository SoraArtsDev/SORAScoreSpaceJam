// Developed by Sora
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sora.Events;
using Sora.Utility;
using TMPro;

namespace Sora.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private BoolVariable isGameOver;
        [SerializeField] private IntVariable playerScore;

        [Header("UI Variables")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Animator scoreAnimator;
        [SerializeField] private GameObject gameOverUI;

        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);

            playerScore.value = 0;
            scoreText.text = playerScore.value.ToString();
        }

        public void AddScore(int score)
        {
            playerScore.value += score;
            scoreText.text = playerScore.value.ToString();
        }

        public void OnGameOver(Component Invoker, object data)
        {
            isGameOver.value = true;
            gameOverUI.SetActive(true);

            Utility.SoraClock.instance.ExecuteWithDelay(this, () => { SceneManager.instance.LoadLeaderboardScene(2); }, 2.0f);
            
        }
    }
}