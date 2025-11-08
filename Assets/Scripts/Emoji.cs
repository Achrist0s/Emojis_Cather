using UnityEngine;
using UnityEngine.InputSystem;

public class Emoji : MonoBehaviour
{
    public float fallSpeed = 2f;
    public float rotationSpeed = 50f;
    public GameObject destroyEffectPrefab;

    private bool collected = false;
    private float rotationDirection;

    void OnEnable()
    {
        collected = false;
        rotationDirection = Random.value < 0.5f ? 1f : -1f;
    }

    void FixedUpdate() 
    {
        if (!GameManager.Instance.IsGameActive) return;

        transform.Translate(Vector3.down * fallSpeed * Time.fixedDeltaTime, Space.World);

        transform.Rotate(0f, 0f, rotationDirection * rotationSpeed * Time.fixedDeltaTime, Space.Self);

        if (transform.position.y < -7f)
            gameObject.SetActive(false);
    }

    public void Collect()
    {
        if (PauseMenuManager.Instance != null && PauseMenuManager.Instance.isPaused)
            return;
        if (collected) return;
        collected = true;
        AudioManager.Instance.PlayClick();

        if (destroyEffectPrefab != null)
        {
            GameObject effect = ObjectPool.Instance.Get(destroyEffectPrefab);
            effect.transform.position = transform.position;
        }

        GameManager.Instance.AddScore(1);
        gameObject.SetActive(false);
    }
}