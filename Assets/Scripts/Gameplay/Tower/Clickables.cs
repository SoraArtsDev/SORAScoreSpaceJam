// Developed by Pluto
//
// Copyright(c) Sora Arts 2023-2024
//
// This script is covered by a Non-Disclosure Agreement (NDA) and is Confidential.
// Destroy the file immediately if you have not been explicitly granted access.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Sora
{

    public class Clickable : MonoBehaviour, IPointerClickHandler
    {
        void OnEnable()
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //if (handler.selectedItem != null && eventData.button == PointerEventData.InputButton.Left && eventData.pointerClick.tag == "selectedElement")
            //{
            //    handler.DropSelectedItem(transform.gameObject);
            //}
            Debug.Log(eventData.pointerClick.tag);
        }
    }
}