// Developed by SORA
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sora.Game
{
    public class Bullet : MonoBehaviour
    {
        private Transform target;
        public float fSpeed = 60.0f;
        public int uDamage = 10; 
        public GameObject ImapctEffect;
        private bool bIsSniper;
        public void Seek(Transform a_target, int a_Damage, bool a_Sniper)
        {
            target = a_target;
            uDamage = a_Damage;
            bIsSniper = a_Sniper;
        }
        // Start is called before the first frame update
        void Start()
        {
            if(bIsSniper)
            {
                fSpeed = 200.0f;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 direction = target.position - transform.position;
            float fDistance = fSpeed * Time.deltaTime;

            if (direction.magnitude <= fDistance)
            {
                HitTarget();
                return;
            }

            transform.Translate(direction.normalized * fDistance, Space.World);
        }

        void HitTarget()
        {
            GameObject effectInstance = (GameObject)Instantiate(ImapctEffect, transform.position, transform.rotation);
            Destroy(effectInstance, 0.5f);

            //Destroy(target.gameObject);
            target.gameObject.GetComponent<Enemy>().TakeDamage(uDamage);
            Destroy(gameObject);
        }
    }
}
