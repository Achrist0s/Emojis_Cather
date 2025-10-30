using UnityEngine;
using UnityEngine.InputSystem;

public class ClickManager : MonoBehaviour
{
    void Update()
    {
        if (!GameManager.Instance.IsGameActive) return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

            foreach (var hit in hits)
            {
                Emoji emoji = hit.GetComponent<Emoji>();
                if (emoji != null)
                {
                    emoji.Collect();
                }
            }
        }
    }
}