using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance;

    [Header("UI Elements")]
    public CanvasGroup pauseMenuGroup;
    public Toggle musicToggle;
    public Toggle soundToggle;

    [Header("Buttons")]
    public Button resumeButton;
    public Button restartButton;
    public Button mainMenuButton;

    public bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        musicToggle.onValueChanged.AddListener(ToggleMusic);
        soundToggle.onValueChanged.AddListener(ToggleSound);

        musicToggle.isOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        soundToggle.isOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;

        UpdateAudioSettings();

        HidePauseMenu();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        ShowPauseMenu();
    }

    public void ResumeGame()
    {
   
        isPaused = false;
        Time.timeScale = 1f;
        HidePauseMenu();
    }

    private void ShowPauseMenu()
    {
        pauseMenuGroup.alpha = 1f;
        pauseMenuGroup.interactable = true;
        pauseMenuGroup.blocksRaycasts = true;
    }

    private void HidePauseMenu()
    {
        pauseMenuGroup.alpha = 0f;
        pauseMenuGroup.interactable = false;
        pauseMenuGroup.blocksRaycasts = false;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void ToggleMusic(bool isOn)
    {
        PlayerPrefs.SetInt("MusicEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateAudioSettings();
    }

    private void ToggleSound(bool isOn)
    {
        PlayerPrefs.SetInt("SoundEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateAudioSettings();
    }

    private void UpdateAudioSettings()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.musicSource.mute = !musicToggle.isOn;
            AudioManager.Instance.sfxSource.mute = !soundToggle.isOn;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

}