using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sora
{
    public class Tower : MonoBehaviour
    {
        private Transform target;

        public Transform RotatePoint;
        public float range = 15.0f;
        public float TurretRotationSpeed = 10.0f;

        public string EnemyTag = "Enemy";

        public float FireRate = 1.0f;

        private float FireCountdown = 0.0f;

        public GameObject BulletPrefab;
        public Transform FirePoint;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
            FirePoint = gameObject.transform.GetChild(0).transform.GetChild(1).transform;
        }

        void UpdateTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
            float ShortestDistance = Mathf.Infinity;
            GameObject NearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (DistanceToEnemy < ShortestDistance)
                {
                    ShortestDistance = DistanceToEnemy;
                    NearestEnemy = enemy;
                }
            }

            if (NearestEnemy != null && ShortestDistance <= range)
            {
                target = NearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (target == null)
                return;

            Vector3 direction = target.position - transform.position;
            Quaternion lookRotatation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(RotatePoint.rotation, lookRotatation, Time.deltaTime * TurretRotationSpeed).eulerAngles;
            RotatePoint.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);

            if (FireCountdown <= 0.0f)
            {
                Shoot();
                FireCountdown = 1.0f / FireRate;
            }

            FireCountdown -= Time.deltaTime;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        void Shoot()
        {
            GameObject bulletGO = (GameObject)Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Seek(target);
            }
        }
    }
}