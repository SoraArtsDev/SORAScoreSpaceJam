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
        TORTOISE,
        PIGEON,
        SNAKE,
        HEDGEHOG,
        HUMAN,
        BEES,
        COUNT

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
        public float armour;
        public bool isFlying;
        public int treatsDropped;

        [Space]
        [SerializeField] private Image healthBar;
        [Space]
        [Space]
        public ArrayClass[] waypointArray;
        private float maxHealthPoints;
        public bool attacking;

        private Vector3 dir;
        private int waypointListIndex;
        private int waypointIndex;
        public int entryPoint;

        private void OnValidate()
        {
            maxHealthPoints = healthPoints;            
        }

        private void OnEnable()
        {
            healthBar.fillAmount = 1.0f;

            switch(entryPoint)
            {
                case 0:
                    {
                        waypointListIndex = Random.Range(0, 3);
                    }
                    break;
                case 1:
                    {
                        waypointListIndex = Random.Range(3, 5);                    
                    }
                    break;
                case 2:
                    {
                        waypointListIndex = Random.Range(5, 8);
                    }
                    break;
            }

            waypointIndex = 0;
            transform.position = waypointArray[waypointListIndex][waypointIndex];

            Vector3 _dir = waypointArray[waypointListIndex][waypointIndex] - transform.position;
            transform.forward = _dir;
            waypointIndex++;
        }

        private void Update()
        {
            if (!attacking)
            {
                dir = waypointArray[waypointListIndex][waypointIndex] - transform.position;
                transform.Translate(moveSpeed * Time.deltaTime * dir.normalized, Space.World);

                if (Vector3.Distance(transform.position, waypointArray[waypointListIndex][waypointIndex]) < 0.01f)
                {
                    waypointIndex++;

                    if (waypointIndex >= waypointArray[waypointListIndex].array.Length)
                        DisableObject();

                    RotateTowardFacingDirection();
                }
            }
            else
            {
                AttackThePlayer();
            }
        }

        public void TakeDamage(int damage)
        {
            healthPoints -= damage;
            healthBar.fillAmount = healthPoints / maxHealthPoints;

            if (healthPoints <= 0)
            {
                DisableObject();
                Managers.ScoreManager.instance.AddTreats(treatsDropped);
            }
        }

        private void DisableObject()
        {
            healthPoints = maxHealthPoints;
            waypointIndex = 0;
            gameObject.SetActive(false);
        }

        private void RotateTowardFacingDirection()
        {
            Vector3 _dir = waypointArray[waypointListIndex][waypointIndex] - transform.position;

            transform.forward = _dir;
        }

        public void OnAttacked(bool attack)
        {
            attacking = attack;
        }

        private void AttackThePlayer()
        {

        }
    }
}