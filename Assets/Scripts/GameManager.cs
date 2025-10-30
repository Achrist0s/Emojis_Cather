using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public bool IsGameActive { get; private set; } = false;
    public float gameTime = 120f;
    private float timer;
    private int score;
    public int CurrentScore => score;

    private UIManager uiManager;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.ShowGameUI();
            StartGame();
        }
        else
        {
            Debug.LogError("❌ UIManager not found in GameScene!");
        }
    }

    void Update()
    {
        if (!IsGameActive) return;

        timer -= Time.deltaTime;
        uiManager.UpdateTimer(timer);

        if (timer <= 0)
            EndGame();
    }

    public void StartGame()
    {
        IsGameActive = true;
        timer = gameTime;
        score = 0;

        uiManager.ShowGameUI();
        uiManager.UpdateScore(score);
        uiManager.UpdateTimer(timer);

        AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic);
    }

    public void EndGame()
    {
        IsGameActive = false;
        uiManager.ShowGameOver(score);
        Debug.Log("Game over!");

        AudioManager.Instance.StopMusic(1f);
    }

    public void AddScore(int amount)
    {
        if (!IsGameActive) return;

        score += amount;
        uiManager.UpdateScore(score);

        AudioManager.Instance.PlayCollect();
    }

    public void RestartGame()
    {
        StartGame();
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}