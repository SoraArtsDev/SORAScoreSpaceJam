// Developed by Sora
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[Header("")]
//[Space(0.5f)]
//[HideInInspector]
namespace Sora
{
    [CreateAssetMenu(menuName = "PlayerControllerData")]
    public class PlayerData : ScriptableObject
    {
        [Header("Movement")]
        public int moveSpeed;
        public int meeleRate;
        public int damage;

        private void OnValidate()
        {

        }
    }
}