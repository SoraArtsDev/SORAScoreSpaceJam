using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sora.Game;

namespace Sora
{
    public class Tower : MonoBehaviour
    {
        private Transform target;

       // [Header("General")]
        //public float range = 15.0f;
        public int towerCost;

        [Header("Use Bullets")]
        public GameObject BulletPrefab;
       // public float FireRate = 1.0f;
        private float FireCountdown = 0.0f;

        private bool bUseLaser = false;
        private bool bUseFreeze = false;
        private bool bUseFlame = false;
        private bool bMortar = false;
        public LineRenderer lineRenderer;

        [Header("Setup Fields")]
        public Transform RotatePoint;
        public float TurretRotationSpeed = 10.0f;
        public string EnemyTag = "Enemy";

        public Transform FirePoint;
        public TowerType type;
        public TowerData data;
        // Start is called before the first frame update

        private void Awake()
        {
            bUseLaser = type == TowerType.E_Lazer;
            bUseFreeze = type == TowerType.E_Freeze;
            bUseFlame = type == TowerType.E_FlameThrower;
            bMortar = type == TowerType.E_Mortar;
            data = new TowerData(Managers.TowerManager.instance.GetTowerData(type));
            Debug.Log("UPgradeLevel"+data.upgradeLevel);
            var cat1 = transform.Find("RotatePoint").Find("Cat").Find("Level1").gameObject;
            var cat2 = transform.Find("RotatePoint").Find("Cat").Find("Level2").gameObject;
            var cat3 = transform.Find("RotatePoint").Find("Cat").Find("Level3").gameObject;
            transform.parent.GetComponent<Sora.TowerUIInfo>().SetTower(transform.gameObject, cat1, cat2, cat3);//not proud of this but had to do this to save time and not change prefab

        }

        void Start()
        {
            transform.tag = "Towers";
            InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
            //FirePoint = gameObject.transform.GetChild(0).transform.GetChild(1).transform;
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

            if (NearestEnemy != null && ShortestDistance <= data.areaOfImpact)
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
            {
                if (bUseLaser)
                {
                    if (lineRenderer.enabled)
                    {
                        lineRenderer.enabled = false;
                    }

                }
                return;
            }
                
            LockOnTarget();

            if (bUseLaser)
            {
                Laser();
            }
            else
            {
                if (FireCountdown <= 0.0f)
                {
                    Attack();
                    FireCountdown = 1.0f / data.attackRate;
                }

                FireCountdown -= Time.deltaTime;
            }

        }

        void Attack()
        {
            if (bMortar)
            {

            }
            else if (bUseFreeze)
            {
                Freeze();
            }
            else if (bUseFlame)
            {

            }
            else
            {
                Shoot();
            }
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, data.areaOfImpact);
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

        void LockOnTarget()
        {
            Vector3 direction = target.position - transform.position;
            Quaternion lookRotatation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(RotatePoint.rotation, lookRotatation, Time.deltaTime * TurretRotationSpeed).eulerAngles;
            RotatePoint.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);
        }

        void Laser()
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
            lineRenderer.SetPosition(0, FirePoint.position);
            lineRenderer.SetPosition(1, target.position);
        }

        void Freeze()
        {
            if (target == null)
                return;

            Debug.Log("effectMultiplier: " + data.effectMultiplier);
            Debug.Log("effectDuration: " + data.effectDuration);
            target.GetComponent<Enemy>().AffectMovementSpeed(data.effectMultiplier, data.effectDuration);
        }
    }
}