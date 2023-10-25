using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    
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
    }

    public bool IsTouching()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    public bool IsClicking()
    {
        return Input.GetMouseButtonDown(0);
    }
}