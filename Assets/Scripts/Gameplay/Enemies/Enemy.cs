// Developed by Sora
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
        BEES,
        COUNT
    }

    public enum EParticleType
    {
        DEADEYE,
        DEATH,
        FIRE,
        FREEZE
    }

    public enum EEnemyLevel
    {
        LEVEL1,
        LEVEL2,
        LEVEL3
    }

    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Stats")]
        public float moveSpeed;
        public float healthPoints;
        public float armour;
        public bool isFlying;
        public int treatsDropped;
        public int scoreProvided;

        [Space]
        [SerializeField] private Image healthBar;
        [SerializeField] private Image armourBar;
        [Space]
        private float maxHealthPoints;
        private float maxArmour;
        [HideInInspector] public bool attacking;


        private float initialMovespeed;
        private Utility.DelayedMethod resetMovespeed;
        private Utility.DelayedMethod resetParticle;
        private Vector3 dir;
        private int waypointListIndex;
        private int waypointIndex;
        public int entryPoint;
        private List<List<Vector3>> waypoints;


        private ParticleSystem[] particles;

        private void Awake()
        {
            maxHealthPoints = healthPoints;
            initialMovespeed = moveSpeed;
            maxArmour = armour;

            particles = new ParticleSystem[4];
            particles[(int)EParticleType.DEADEYE] = transform.Find("deadEye").GetComponent<ParticleSystem>();
            particles[(int)EParticleType.DEATH] = transform.Find("blood").GetComponent<ParticleSystem>();
            particles[(int)EParticleType.FIRE] = transform.Find("fire").GetComponent<ParticleSystem>();
            particles[(int)EParticleType.FREEZE] = transform.Find("freeze").GetComponent<ParticleSystem>();
    }

        private void OnEnable()
        {
            healthBar.fillAmount = 1.0f;

            if(armourBar)
                armourBar.fillAmount = 1.0f;
            moveSpeed = initialMovespeed;

            switch(entryPoint)
            {
                case 0:
                    {
                        waypointListIndex = Random.Range(0, 3);
                    }
                    break;
                case 1:
                    {
                        waypointListIndex = Random.Range(3, 6);                    
                    }
                    break;
                case 2:
                    {
                        waypointListIndex = Random.Range(6, 8);
                    }
                    break;
            }
            //Debug.Log(waypointListIndex);
            waypoints = WaypointKeeper.instance.waypoints;
            waypointIndex = 0;
            transform.position =  waypoints[waypointListIndex][waypointIndex];

            Vector3 _dir =  waypoints[waypointListIndex][1] - waypoints[waypointListIndex][0];
            transform.forward = _dir;
            waypointIndex++;
        }

        private void Update()
        {
            if (!attacking)
            {
                dir =  waypoints[waypointListIndex][waypointIndex] - transform.position;
                transform.Translate(moveSpeed * Time.deltaTime * dir.normalized, Space.World);

                if (Vector3.Distance(transform.position,  waypoints[waypointListIndex][waypointIndex]) < 0.01f)
                {
                    waypointIndex++;

                    if (waypointIndex >=  waypoints[waypointListIndex].Count)
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
            if (armour > 0)
            {
                armour -= damage;
                armourBar.fillAmount = (float) (armour / maxArmour);
            }
            else
            {
                healthPoints -= damage;
                healthBar.fillAmount = (float) (healthPoints / maxHealthPoints);

                if (healthPoints <= 0)
                {
                    Managers.InventoryManager.instance.AddTreats(treatsDropped);
                    Managers.GameManager.instance.AddScore(scoreProvided);
                    DisableObject();
                }
            }            
        }

        private void DisableObject()
        {
            healthPoints = maxHealthPoints;
            armour = maxArmour;
            waypointIndex = 0;
            gameObject.SetActive(false);
        }

        private void RotateTowardFacingDirection()
        {
            Vector3 _dir =  waypoints[waypointListIndex][waypointIndex] - transform.position;

            transform.forward = _dir;
        }

        public void OnAttacked(bool attack)
        {
            attacking = attack;
        }

        private void AttackThePlayer()
        {

        }

        public void AffectMovementSpeed(float speedModifier, float duration)
        {
            if (resetMovespeed != null)
                Utility.SoraClock.instance.StopDelayedMethod(resetMovespeed);

            moveSpeed *= speedModifier;
            resetMovespeed = Utility.SoraClock.instance.ExecuteWithDelay(this, () => { moveSpeed = initialMovespeed;}, duration);
        }


        public void PlayParticleEffect(EParticleType particleType)
        {
            if (particles[(int)particleType].isPlaying)
                return;

            if (resetParticle != null)
                Utility.SoraClock.instance.StopDelayedMethod(resetParticle);

            particles[(int)particleType].gameObject.SetActive(true);
            particles[(int)particleType].Play();

            resetParticle =  Utility.SoraClock.instance.ExecuteWithDelay(this, () => { StopParticle(); }, 2.0f);
        }

        public void StopParticle()
        {
            particles[(int)EParticleType.DEADEYE].Stop();
            particles[(int)EParticleType.DEATH].Stop();
            particles[(int)EParticleType.FIRE].Stop();
            particles[(int)EParticleType.FREEZE].Stop();


            particles[(int)EParticleType.DEADEYE].gameObject.SetActive(false);
            particles[(int)EParticleType.DEATH].gameObject.SetActive(false);
            particles[(int)EParticleType.FIRE].gameObject.SetActive(false);
            particles[(int)EParticleType.FREEZE].gameObject.SetActive(false);
        }
    }
}