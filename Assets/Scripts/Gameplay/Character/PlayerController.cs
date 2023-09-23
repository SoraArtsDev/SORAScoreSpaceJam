// Developed by Sora Arts
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sora
{
    enum PlayerAction : short
    {
        E_Attack,
        E_Move,
    };

    public enum PlayerState : short
    {
        E_Idle,
        E_Attacking,
        E_Moving,
        E_Reloading,
    };

    public class PlayerController : MonoBehaviour
    {
        Rigidbody rb;
        NavMeshAgent agent;
        Game.Enemy selectedEnemy;

        bool moveClicked;
        bool attackClicked;
        bool canAttack;

        public PlayerState playerState;

        public PlayerData playerData;

        void Start()
        {
            playerState = PlayerState.E_Idle;
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            moveClicked = false;
        }

        void Update()
        {
            moveClicked = Input.GetMouseButtonDown((int)PlayerAction.E_Move);
            attackClicked = Input.GetMouseButtonDown((int)PlayerAction.E_Attack);

            if (moveClicked)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                }
                playerState = PlayerState.E_Moving;
                agent.isStopped = false;
            }
            else if(attackClicked)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                }
                playerState = PlayerState.E_Attacking;
                agent.isStopped = false;
            }

            //we cancel out attack and do new action
            if((moveClicked || attackClicked) && selectedEnemy!=null)
            {
                selectedEnemy.OnAttacked(false);
                selectedEnemy = null;
            }

            if(HasStopped())
            {
                //we check what the state was??
                playerState = playerState == PlayerState.E_Moving ? PlayerState.E_Idle : playerState;
                Attack();
            }

        }

        GameObject ChooseBestEnemyInTheArea()
        {
            RaycastHit hit;
            Vector3 p1 = transform.position;
            if (Physics.SphereCast(p1, 1, transform.forward, out hit, 2))
            {
                return hit.transform.gameObject;
            }
            return null;
        }

        public void SetEnemyToAttack(Game.Enemy enemy)
        {
            if (playerState != PlayerState.E_Attacking || selectedEnemy != null  || !HasStopped())
                return;

            agent.isStopped = true;
          //  rb.velocity = Vector3.zero;
            selectedEnemy = enemy;
            selectedEnemy.OnAttacked(true);
        }

        void FixedUpdate()
        {
            if (moveClicked)
            {

            }
        }
        #region Controls
        void Attack()
        {
            if (!CanAttack())
                return;

            playerState = PlayerState.E_Reloading;
            selectedEnemy.TakeDamage(playerData.damage);
            canAttack = false;
            Debug.Log("MeeleWait" + selectedEnemy.name);
            StartCoroutine("MeeleWait", playerData.meeleRate);
            canAttack = true;
            playerState = PlayerState.E_Attacking;
            Debug.Log("E_Attacking" + selectedEnemy.name);
        }

        bool CanAttack()
        {
            return canAttack && selectedEnemy != null && playerState == PlayerState.E_Attacking;
        }

        bool HasStopped()
        {
            return agent.remainingDistance <= 0.1f;
        }

        IEnumerator MeeleWait(float duration)
        {
            yield return new WaitForSeconds(duration);
        }
        #endregion
    }
}
