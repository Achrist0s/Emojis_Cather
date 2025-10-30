using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button shopButton;
    public Button exitButton;
    public TextMeshProUGUI shopMessageText;

    private void Start()
    {
        if (shopMessageText != null)
            shopMessageText.gameObject.SetActive(false);

        startButton.onClick.AddListener(StartGame);
        shopButton.onClick.AddListener(ShowShopMessage);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void ShowShopMessage()
    {
        if (shopMessageText != null)
        {
            shopMessageText.text = "Магазин скоро буде!";
            shopMessageText.gameObject.SetActive(true);
            CancelInvoke(nameof(HideShopMessage));
            Invoke(nameof(HideShopMessage), 2.5f);
        }
    }

    private void HideShopMessage()
    {
        if (shopMessageText != null)
            shopMessageText.gameObject.SetActive(false);
    }

    private void ExitGame()
    {
        Debug.Log("Вихід з гри...");
        Application.Quit();
    }
}