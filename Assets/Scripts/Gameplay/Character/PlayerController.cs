// Developed by Sora Arts
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
    enum PlayerAction : short
    {
        E_Attack,
        E_Move,
    };

	public class PlayerController : MonoBehaviour
	{
        Rigidbody rb;
        public PlayerData playerData;

        bool wasClicked;
		void Start()
		{
            rb = GetComponent<Rigidbody>();
            wasClicked = false;
        }

		void Update()
		{
            wasClicked = Input.GetMouseButtonDown((int)PlayerAction.E_Move);
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
