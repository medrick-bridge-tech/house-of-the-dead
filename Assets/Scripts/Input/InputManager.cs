using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Button interactionButton;
    [SerializeField] private Sprite pickUpIcon, puzzleIcon;
    private static InputManager instance;
    private Image buttonImage;
    public bool interactionKeyPressed { get; private set; }
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("Input Manager");
                instance = obj.AddComponent<InputManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
        interactionButton.onClick.AddListener(InteractionButtonClicked);
        buttonImage = interactionButton.transform.GetChild(0).GetComponent<Image>();
    }

    public bool IsTouching()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    public bool IsClicking()
    {
        return Input.GetMouseButtonDown(0);
    }

    public void InteractionButtonClicked()
    {
        interactionKeyPressed = true;
    }

    public void ActivateInteractionButton(bool isPickable)
    {
        buttonImage.sprite = isPickable ? pickUpIcon : puzzleIcon;
        interactionButton.gameObject.SetActive(true);
    }
    
    public void DeActivateInteractionButton()
    {
        interactionKeyPressed = false;
        interactionButton.gameObject.SetActive(false);
    }
}