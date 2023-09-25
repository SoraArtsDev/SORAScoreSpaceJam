// Developed by Sora
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

namespace Sora.Managers
{

    public class LeaderboardManager : Singleton<LeaderboardManager>
    {
        public TMP_Text scoreText;
        public GameObject lbUI;

        [Header("Score submission Variables")]
        [SerializeField] private TMP_InputField playerNameInput;
        [SerializeField] private IntVariable playerFinalScore;
        [SerializeField] private string leaderboardKey;
        

        [Header("Leaderboard Variables")]
        [SerializeField] private TMP_Text[] playerNames;
        [SerializeField] private TMP_Text[] playerScores;

        [Header("Current Score Variables")]
        [SerializeField] private TMP_Text currentRankText;
        [SerializeField] private TMP_Text currentName;
        [SerializeField] private TMP_Text currentScore;

        private int currentRank;

        private void OnEnable()
        {
            scoreText.text = playerFinalScore.value.ToString();
        }

        public bool StartLootlockerGuestSession()
        {
            bool _success = false;

            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (!response.success)
                {
                    //Debug.Log("Lootlocker:: Error Starting lootlocker session.");
                    _success = false;
                    return;
                }

                _success = true;
               /// Debug.Log("Lootlocker:: Session started successfully.");
            });

            return _success;
        }

        public void SubmitScore()
        {
            LootLockerSDKManager.SubmitScore(playerNameInput.text, playerFinalScore.value, leaderboardKey, (response) =>
            {
                if (response.success)
                {
                    currentRank = response.rank;
                    Debug.Log("Lootlocker:: Score submission successful.");

                    LoadLeaderboards();
                }
                else
                {
                    Debug.Log("Lootlocker:: " + response.errorData);
                }
            });
        }

        private void LoadLeaderboards()
        {
            // fill in current player details
            currentRankText.text = currentRank.ToString();
            currentName.text = playerNameInput.text;
            currentScore.text = playerFinalScore.value.ToString();

            LootLockerSDKManager.GetScoreList(leaderboardKey, 6, 0, (response) =>
            {
                if (response.success)
                {                    
                    StartCoroutine(LoadScore(response));                    
                }
                else
                {
                    Debug.Log("Lootlocker:: " + response.errorData);
                }
            });
        }

        public void GoToMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        IEnumerator LoadScore(LootLockerGetScoreListResponse res)
        {
            Debug.Log("Calling coroutine");
            yield return new WaitForSecondsRealtime(1.0f);

            lbUI.SetActive(true);
            LootLockerLeaderboardMember[] _members = res.items;

            for (int i = 0; i < _members.Length; ++i)
            {
                playerNames[i].text = _members[i].player.name;
                if(playerNames[i].text == "")
                    playerNames[i].text = _members[i].member_id;

                playerScores[i].text = _members[i].score.ToString();
            }
        }
    }
}