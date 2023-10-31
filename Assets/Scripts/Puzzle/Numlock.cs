using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Numlock : Puzzle
{
    [SerializeField] private Button finishButton;
    [SerializeField] private List<GameObject> numberWheels;
    [SerializeField] private GameObject key;
    [SerializeField] private string correctCombination = "1836";
    [SerializeField] private AudioClip rattleSound, openSound;
    private SetCameraFOV cameraFov;

    private AudioSource audioSource;
    private Animator animator;
    private string currentCombination = "0000";
    private float rotationSpeed = 8f;
    private float rotationStep = 36f;
    private bool interactionEnabled = true;

    protected override void Awake()
    {
        base.Awake();
        OnPuzzleFailed += RattleDoor;
        OnPuzzleSolved += OpenDoor;
        cameraFov = virtualCamera.GetComponent<SetCameraFOV>();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        finishButton.onClick.AddListener(OnFinishButtonClicked);
        cameraFov.CameraZoomIn();
    }

    protected override void Update()
    {
        base.Update();

        if (InputManager.Instance.IsTouching() || InputManager.Instance.IsClicking())
            if (interactionEnabled)
                HandleInteraction();
    }

    private void HandleInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            for (int wheelNum = 0; wheelNum < numberWheels.Count; wheelNum++)
                if (hit.collider.gameObject == numberWheels[wheelNum])
                    StartCoroutine(RotateNumberWheel(wheelNum));
    }

    private void OnFinishButtonClicked()
    {
        HandlePuzzleFinish();
    }

    public override void HandlePuzzleFinish()
    {
        if (currentCombination == correctCombination)
            OnPuzzleSolved.Invoke();
        
        else
            OnPuzzleFailed.Invoke();
    }

    private void RattleDoor()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rattleSound);
            animator.SetTrigger("Rattle");
        }
    }

    private void OpenDoor()
    {
        cameraFov.CameraZoomOut();
        
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(openSound);
            animator.SetTrigger("Open");
        }
        
        finishButton.onClick.RemoveListener(OnFinishButtonClicked);
        finishButton.gameObject.SetActive(false);
    }

    private IEnumerator RotateNumberWheel(int wheelNumber)
    {
        interactionEnabled = false;
        Transform wheelTransform = numberWheels[wheelNumber].transform;
        Quaternion targetRotation = wheelTransform.rotation * Quaternion.Euler(0f, rotationStep, 0f);

        while (Quaternion.Angle(wheelTransform.rotation, targetRotation) > 0.01f)
        {
            wheelTransform.rotation = Quaternion.Lerp(
                wheelTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        wheelTransform.rotation = targetRotation;
        CalculateCurrentCode();
        interactionEnabled = true;
    }

    private void CalculateCurrentCode()
    {
        currentCombination = "";

        for (int number = 0; number < numberWheels.Count; number++)
        {
            float rotationInDegrees = numberWheels[number].transform.localEulerAngles.y;
            int code = Mathf.RoundToInt(rotationInDegrees / rotationStep);
            currentCombination += code.ToString();
        }
    }
    
    private void LockOpened()
    {
        key.SetActive(true);
        Destroy(this.gameObject);
    }

    protected override void HandleCharacterInteraction(Character character)
    {
        base.HandleCharacterInteraction(character);
        finishButton.gameObject.SetActive(true);
    }

}
