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
        bool wasClicked;
        NavMeshAgent agent;

        public PlayerData playerData;

		void Start()
		{
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            wasClicked = false;
        }

		void Update()
		{
            wasClicked = Input.GetMouseButtonDown((int)PlayerAction.E_Move);

            if(wasClicked)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit))
                {
                    agent.SetDestination(hit.point);
                }
            }

        }

        void FixedUpdate()
        {
            if(wasClicked)
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
