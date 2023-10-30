﻿using Settings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class Interactable : MonoBehaviour
    {
        [SerializeField] private BoxCollider interactionArea;
        [SerializeField] private bool shouldDestroyOnInteraction = true;
        public UnityEvent<Character> onInteraction;

        private bool _canBeInteracted;
        private Character _interactor;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _canBeInteracted = true;
                _interactor = other.GetComponent<Character>();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _canBeInteracted = false;
                _interactor = null;
            }
        }

        private void Update()
        {
            if (_canBeInteracted == false)
                return;

            if (Input.GetKeyDown(Keybindings.InteractionKey))
            {
                onInteraction.Invoke(_interactor);
                if(shouldDestroyOnInteraction)
                    Destroy(gameObject);
            }
                
        }

        private void Reset()
        {
            interactionArea = GetComponent<BoxCollider>();
            if (interactionArea == null)
                interactionArea = gameObject.AddComponent<BoxCollider>();
        }
    }