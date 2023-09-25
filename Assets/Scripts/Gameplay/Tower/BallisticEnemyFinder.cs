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
    public class BallisticEnemyFinder : MonoBehaviour
    {
        public int damage;
        public bool dealDamage = true;
        public float areaOfImpact = 10.0f;
        public float destructionTimer = 1.0f;

        public GameObject[] sceneEnemies;
        void Start()
        {
            dealDamage = true;
        }

        private void Update()
        {
            if (dealDamage)
            {
                dealDamage = false;
                foreach (GameObject enemy in sceneEnemies)
                {
                    float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (DistanceToEnemy < areaOfImpact)
                    {
                        enemy.GetComponent<Sora.Game.Enemy>().TakeDamage(damage);
                    }
                }
            }
            
            if(destructionTimer<=0)
            {
                DestroySelf();
            }
            destructionTimer -= Time.deltaTime;
        }


        void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, areaOfImpact);
        }
    }
}