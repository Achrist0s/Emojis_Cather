using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => PauseMenuManager.Instance.PauseGame());
    }
}