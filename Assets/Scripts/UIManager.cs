using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;

    public Button restartButton;
    public Button mainMenuButton;

    [Header("Animation Settings")]
    public float fadeDuration = 0.8f;
    public float scaleUpAmount = 1.3f;

    private CanvasGroup gameOverGroup;
    private Vector3 originalScale;


    void Start()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());

        gameOverGroup = gameOverText.gameObject.GetComponent<CanvasGroup>();
        if (gameOverGroup == null)
            gameOverGroup = gameOverText.gameObject.AddComponent<CanvasGroup>();

        originalScale = gameOverText.transform.localScale;

        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    public void ShowGameUI()
    {
        scoreText.text = "Score: 0";
        timerText.text = "Time: 120s";
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);

        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    public void UpdateTimer(float time)
    {
        if (timerText == null) return;

        int seconds = Mathf.CeilToInt(time);
        timerText.text = $"Time: {seconds}s";
    }

    public void ShowGameOver(int score)
    {
        if (gameOverText == null || restartButton == null || mainMenuButton == null) return;

        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        gameOverText.text = $"Game Over!\nFinal Score: {score}";
        gameOverText.gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(AnimateGameOver());
    }

    private IEnumerator AnimateGameOver()
    {
        gameOverGroup.alpha = 0f;
        gameOverText.transform.localScale = originalScale * 0.5f;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            gameOverGroup.alpha = Mathf.Lerp(0f, 1f, t);
            gameOverText.transform.localScale = Vector3.Lerp(originalScale * 0.5f, originalScale * scaleUpAmount, t);

            yield return null;
        }

        elapsed = 0f;
        while (elapsed < 0.3f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 0.3f;
            gameOverText.transform.localScale = Vector3.Lerp(originalScale * scaleUpAmount, originalScale, t);
            yield return null;
        }

        gameOverText.transform.localScale = originalScale;
    }
}