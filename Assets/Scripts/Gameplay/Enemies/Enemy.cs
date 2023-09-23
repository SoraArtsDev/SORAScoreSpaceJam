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

namespace Sora.Game
{
    public enum EEnemyType
    {
        MOUSE,
        SPIDER,
        PIGEON,
        SNAKE,
        HEDGEHOG,
        HUMAN
    }

    public enum EEnemyLevel
    {
        LEVEL1,
        LEVEL2,
        LEVEL3
    }

    [System.Serializable]
    public class ArrayClass
    {
        public Vector3[] array;

        public Vector3 this[int key]
        {
            get => array[key];
            set => array[key] = value;
        }
    }

    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Stats")]
        public float moveSpeed;
        public float healthPoints;

        [Space]
        [SerializeField] private Image healthBar;
        public ArrayClass[] waypointArray;
        private float maxHealthPoints;

        private Vector3 dir;
        public int waypointListIndex;
        private int waypointIndex;

        private void OnValidate()
        {
            maxHealthPoints = healthPoints;            
        }

        private void OnEnable()
        {
            healthBar.fillAmount = 1.0f;
            waypointIndex = 0;
        }

        private void Update()
        {
            Debug.Log(waypointArray[waypointListIndex][waypointIndex]);
            dir = waypointArray[waypointListIndex][waypointIndex] - transform.position;
            transform.Translate(moveSpeed * Time.deltaTime * dir.normalized, Space.World);

            if(Vector3.Distance(transform.position, waypointArray[waypointListIndex][waypointIndex]) < 0.01f)
            {
                waypointIndex++;
            }

            if (waypointListIndex == waypointArray[waypointListIndex].array.Length)
                DisableObject();
        }

        public void TakeDamage(int damage)
        {
            healthPoints -= damage;
            healthBar.fillAmount = healthPoints / maxHealthPoints;

            if (healthPoints <= 0)
                DisableObject();
        }

        public void DisableObject()
        {
            healthPoints = maxHealthPoints;
            waypointIndex = 0;
            gameObject.SetActive(false);
        }
    }
}