using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject winPanelPrefab;
    [SerializeField] GameObject losePanelPrefab;
    [SerializeField] GameObject pausePanelPrefab;

    private static UIManager instance;

    public static UIManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    public void ShowJoyStick()
    {
        joystick.SetActive(true);
    }
    
    public void HideJoyStick()
    {
        joystick.SetActive(false);
    }

    public void ShowWinPanel()
    {
        winPanelPrefab.SetActive(true);
    }

    public void HideWinPanel()
    {
        winPanelPrefab.SetActive(false);
    }

    public void ShowLosePanel()
    {
        losePanelPrefab.SetActive(true);
    }

    public void HideLosePanel()
    {
        losePanelPrefab.SetActive(false);
    }

    public void ShowPausePanel()
    {
        pausePanelPrefab.SetActive(true);
    }

    public void HidePausePanel()
    {
        pausePanelPrefab.SetActive(false);
    }
}
