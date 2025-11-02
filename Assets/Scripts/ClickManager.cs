using UnityEngine;
using UnityEngine.InputSystem;

public class ClickManager : MonoBehaviour
{
    void Update()
    {
        if (!GameManager.Instance.IsGameActive) return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            CollectAtPosition(pos);
        }

        if (Touchscreen.current != null)
        {
            foreach (var touch in Touchscreen.current.touches)
            {
                if (touch.press.wasPressedThisFrame)
                {
                    Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position.ReadValue());
                    CollectAtPosition(pos);
                }
            }
        }
    }

    void CollectAtPosition(Vector2 position)
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(position);
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
