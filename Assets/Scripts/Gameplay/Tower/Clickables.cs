// Developed by Pluto
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sora
{
    public class Clickables : MonoBehaviour
    {
        private UnityEvent clickEvent;

        private void Start()
        {
            clickEvent = new UnityEvent();
            clickEvent.AddListener(OnClick);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Towers")
                {
                    clickEvent.Invoke();
                }
            }
        }

        void OnClick()
        {
            Debug.Log(gameObject.name);
        }
    }
}