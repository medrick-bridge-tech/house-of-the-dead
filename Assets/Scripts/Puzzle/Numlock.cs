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
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    
    private AudioSource audioSource;
    private Animator animator;
    private string currentCombination = "0000";
    private float rotationSpeed = 6f;
    private float zoomInFOV = 38f;
    private float zoomOutFOV = 60f;
    private float rotationStep = 36f;
    private bool interactionEnabled = true;

    private void Awake()
    {
        OnPuzzleFailed += RattleDoor;
        OnPuzzleSolved += OpenDoor;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        finishButton.onClick.AddListener(OnFinishButtonClicked);
        virtualCamera.m_Lens.FieldOfView = zoomInFOV;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (interactionEnabled)
            {
                HandleInteraction();
            }
        }
    }

    private void HandleInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            for (int wheelNum = 0; wheelNum < numberWheels.Count; wheelNum++)
            {
                if (hit.collider.gameObject == numberWheels[wheelNum])
                {
                    StartCoroutine(RotateNumberWheel(wheelNum));
                }
            }
        }
    }

    private void OnFinishButtonClicked()
    {
        HandlePuzzleFinish();
    }

    public override void HandlePuzzleFinish()
    {
        if (currentCombination == correctCombination)
        {
            OnPuzzleSolved.Invoke();
        }
        else
        {
            OnPuzzleFailed.Invoke();
        }
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
        virtualCamera.m_Lens.FieldOfView = zoomOutFOV;
        
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

}
