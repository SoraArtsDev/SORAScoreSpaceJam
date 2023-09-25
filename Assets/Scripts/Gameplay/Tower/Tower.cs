using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sora.Game;

namespace Sora
{
    public class Tower : MonoBehaviour
    {
        private SortedList<float,Transform> targets;
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

        public ParticleSystem particle1;
        public ParticleSystem particle2;
        public ParticleSystem particle3;
        public GameObject ballistic;


        public GameObject[] sceneEnemies;

        public float angle = 45f;
        public float gravity = 45f;
        public float velocityMult = 4;
        public float minDistance = 0;
        // Start is called before the first frame update

        private void Awake()
        {
            bUseLaser = type == TowerType.E_Lazer;
            bUseFreeze = type == TowerType.E_Freeze;
            bUseFlame = type == TowerType.E_FlameThrower;
            bMortar = type == TowerType.E_Mortar;

            minDistance = 0;
            if (bMortar)
                minDistance = 25.0f;

            data = new TowerData(Managers.TowerManager.instance.GetTowerData(type));
            //Debug.Log("UPgradeLevel"+data.upgradeLevel);
            var cat1 = transform.Find("RotatePoint").Find("Cat").Find("Level1").gameObject;
            var cat2 = transform.Find("RotatePoint").Find("Cat").Find("Level2").gameObject;
            var cat3 = transform.Find("RotatePoint").Find("Cat").Find("Level3").gameObject;
            transform.parent.GetComponent<Sora.TowerUIInfo>().SetTower(transform.gameObject, cat1, cat2, cat3);//not proud of this but had to do this to save time and not change prefab
            var particle1Obj = transform.Find("RotatePoint").Find("Particle1");
            var particle2Obj = transform.Find("RotatePoint").Find("Particle2");
            var particle3Obj = transform.Find("RotatePoint").Find("Particle3");
            if(particle1Obj != null)
            {
                particle1 = particle1Obj.GetComponent<ParticleSystem>();
                particle1.gameObject.SetActive(true);
                particle1.Stop();
            }
            if (particle2Obj != null)
            {
                particle2 = particle2Obj.GetComponent<ParticleSystem>();
                particle2.gameObject.SetActive(true);
                particle2.Stop();
            }
            if (particle3Obj != null)
            {
                particle3 = particle3Obj.GetComponent<ParticleSystem>();
                particle3.gameObject.SetActive(true);
                particle3.Stop();
            }

        }

        void Start()
        {
            targets = new SortedList<float, Transform>();
            target = null;
            transform.tag = "Towers";
            InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
            //FirePoint = gameObject.transform.GetChild(0).transform.GetChild(1).transform;
        }

        void UpdateTarget()
        {
            targets.Clear();

            sceneEnemies = GameObject.FindGameObjectsWithTag(EnemyTag);
            float ShortestDistance = Mathf.Infinity;
            GameObject NearestEnemy = null;
            /*  foreach (GameObject enemy in enemies)
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
            }*/


            foreach (GameObject enemy in sceneEnemies)
            {
                float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (DistanceToEnemy <= data.areaOfImpact && DistanceToEnemy>=minDistance)
                {
                    targets.Add(DistanceToEnemy, enemy.transform);
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
                StopParticles();
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
                    FireCountdown = data.attackRate;
                    Debug.Log(FireCountdown + "FireCountdown");
                }

                FireCountdown -= Time.deltaTime;
            }

        }

        void StopParticles()
        {
            if(bUseFlame || bUseFreeze)
            {
                particle1.Stop();
                particle2.Stop();
                particle3.Stop();
            }
        }

        void Attack()
        {
            if (bMortar)
            {
                ShootBallistic();
            }
            else if (bUseFreeze)
            {
                StopParticles();
                Freeze();
            }
            else if (bUseFlame)
            {
                StopParticles();

                FlameThrower();
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

            if (data.level == 0)
                particle1.Play();
            else if (data.level == 1)
                particle2.Play();
            else if (data.level == 2)
                particle3.Play();

            foreach (var pair in targets)
            {
                pair.Value.GetComponent<Enemy>().AffectMovementSpeed(data.effectMultiplier, data.effectDuration);
                pair.Value.GetComponent<Enemy>().PlayParticleEffect(EParticleType.FREEZE);
                pair.Value.GetComponent<Enemy>().TakeDamage(data.damage);
            }
        }

        void FlameThrower()
        {
            if (target == null)
                return;

            if (data.level == 0)
                particle1.Play();
            else if (data.level == 1)
                particle2.Play();
            else if (data.level == 2)
                particle3.Play();
            foreach (var pair in targets)
            {
                pair.Value.GetComponent<Enemy>().AffectMovementSpeed(data.effectMultiplier, data.effectDuration);
                pair.Value.GetComponent<Enemy>().PlayParticleEffect(EParticleType.FIRE);
                pair.Value.GetComponent<Enemy>().TakeDamage(data.damage);
            }
        }

        void ShootBallistic()
        {
            GameObject go = Instantiate(ballistic);
            go.transform.position = FirePoint.position;
            go.GetComponent<Ballistic>().FireCannonAtPoint(target.position, angle, gravity, velocityMult,data.damage, ref sceneEnemies);
        }
    }
}