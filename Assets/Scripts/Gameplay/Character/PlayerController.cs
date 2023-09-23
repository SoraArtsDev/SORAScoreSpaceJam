// Developed by Sora Arts
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using UnityEngine;
using UnityEngine.AI;

namespace Sora
{
    enum PlayerAction : short
    {
        E_Attack,
        E_Move,
    };

	public class PlayerController : MonoBehaviour
	{
        Rigidbody rb;
        NavMeshAgent agent;


        bool moveClicked;
        bool attackClicked;
        bool isAttacking;
        
        public PlayerData playerData;

		void Start()
		{
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            moveClicked = false;
        }

		void Update()
		{
            moveClicked = Input.GetMouseButtonDown((int)PlayerAction.E_Move);
            attackClicked = Input.GetMouseButtonDown((int)PlayerAction.E_Attack);

            if(moveClicked || attackClicked)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit))
                {
                    agent.SetDestination(hit.point);
                    isAttacking = attackClicked;
                }
            }

            if(isAttacking && agent.isStopped)
            {
                var enemy = ChooseBestEnemyInTheArea();
                if(enemy!=null)
                {
                    Debug.Log(enemy.name);
                    isAttacking = false;
                }
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

        void FixedUpdate()
        {
            if(moveClicked)
            {
                
            }
        }


        #region Controls
        void OnClick()
        {
            MovePlayer();
        }

        void MovePlayer()
        {

        }
        #endregion
    }
}
