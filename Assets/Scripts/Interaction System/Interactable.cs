using System;
using Settings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class Interactable : MonoBehaviour
{
    [SerializeField] private BoxCollider interactionArea;
    [SerializeField] private bool shouldBeDestroyedOnInteract = true;

    public UnityEvent<Character> onInteraction;

    private bool _canBeInteracted;
    private Character _interactor;

    private InputManager inputManager;

    protected virtual void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeInteracted = true;
            _interactor = other.GetComponent<Character>();
            inputManager.ActivateInteractionButton(shouldBeDestroyedOnInteract);
        }
    }
        
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeInteracted = false;
            _interactor = null;
            inputManager.DeActivateInteractionButton();
        }
    }

    protected virtual void Update()
    {
        if (_canBeInteracted == false)
            return;

        if (Input.GetKeyDown(Keybindings.InteractionKey) || (inputManager.interactionKeyPressed))
        {
            inputManager.DeActivateInteractionButton();
            onInteraction.Invoke(_interactor);
            if (shouldBeDestroyedOnInteract)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Reset()
    {
        interactionArea = GetComponent<BoxCollider>();
        if (interactionArea == null)
            interactionArea = gameObject.AddComponent<BoxCollider>();
    }
}