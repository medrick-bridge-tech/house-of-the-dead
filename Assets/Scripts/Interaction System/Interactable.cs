using System;
using Settings;
using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private BoxCollider interationArea;

        public UnityEvent onInteraction;

        private bool _canBeInteracted;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _canBeInteracted = true;
        }

        private void Update()
        {
            if (_canBeInteracted == false)
                return;

            if (Input.GetKeyDown(Keybindings.InteractionKey))
                onInteraction.Invoke();
        }

        private void Reset()
        {
            interationArea = GetComponent<BoxCollider>();
            if (interationArea == null)
                interationArea = gameObject.AddComponent<BoxCollider>();
        }
    }
}