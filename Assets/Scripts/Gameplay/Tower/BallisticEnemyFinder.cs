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
        public bool selfDestruct = true;
        public float areaOfImpact = 10.0f;

        public GameObject[] sceneEnemies;
        void Start()
        {
            selfDestruct  = true;
        }

        private void Update()
        {
            foreach (GameObject enemy in sceneEnemies)
            {
                float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (DistanceToEnemy < areaOfImpact)
                {
                    enemy.GetComponent<Sora.Game.Enemy>().TakeDamage(damage);
                    enemy.GetComponent<Sora.Game.Enemy>().PlayParticleEffect(Game.EParticleType.DEADEYE);
                }
            }
            if (selfDestruct)
            {
                selfDestruct = false;
                Utility.SoraClock.instance.ExecuteWithDelay(this, () => { Destroy(gameObject); }, 2.0f);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, areaOfImpact);
        }
    }
}