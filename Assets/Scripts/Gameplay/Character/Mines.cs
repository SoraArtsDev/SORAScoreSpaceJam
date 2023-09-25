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
    /// You may delete all of the stuff inside here. 
    /// Just remember to stick to the formating
    public class Mines : MonoBehaviour
    {

        public GameObject enemyFinder;
        public ParticleSystem particle;
        public int damageAmount;

        public float duration = 5.0f;
        public bool animationPlayed = false;
        public bool mineAdded = false;

        void Start()
        {
            animationPlayed = false;
        }

        void Update()
        {
            Vector3 vec = new Vector3(Mathf.Sin(Time.time * 2), Mathf.Sin(Time.time * 2), Mathf.Sin(Time.time * 2));
            transform.localScale = vec;

            if (duration <= .1f)
            {
                if(!animationPlayed)
                    particle.Play();

                animationPlayed = true;
            }
            else if(!mineAdded)
            {
                AddMine();
                mineAdded = true;
            }
            duration -= Time.deltaTime;
        }
        void AddMine()
        {
            GameObject go = Instantiate(enemyFinder);
            go.transform.position = transform.position;
            go.GetComponent<Sora.BallisticEnemyFinder>().damage = damageAmount;
            go.GetComponent<Sora.BallisticEnemyFinder>().sceneEnemies = Managers.WaveManager.instance.currentEnemies;

            Destroy(gameObject);
        }
    }
}