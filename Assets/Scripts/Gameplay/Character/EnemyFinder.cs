// Developed by Sora
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sora 
{
    public class EnemyFinder : MonoBehaviour
    {

        private PlayerController playerController;
        void Start()
        {
            playerController = transform.parent.GetComponent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Enemy")
                return;
            Game.Enemy enemy = other.transform.GetComponent<Game.Enemy>();
            playerController.SetEnemyToAttack(enemy);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != "Enemy")
                return;
        }
    }
}