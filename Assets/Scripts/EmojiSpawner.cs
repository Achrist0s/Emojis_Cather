using UnityEngine;
using System.Linq;

public class EmojiSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject emojiPrefab;
    public Sprite[] emojiSprites;
    public float spawnInterval = 0.3f;

    private float timer;
    private Camera cam;
    private float minX, maxX, spawnY;

    void Awake()
    {
        cam = Camera.main;
        if (emojiSprites == null || emojiSprites.Length == 0)
        {
            var allSprites = Resources.LoadAll<Sprite>("EmojiSheet");
            emojiSprites = allSprites.Where(s => s != null && s.name != "Css_sprites_0" && s.name != "Css_sprites_7").ToArray();
        }

        minX = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        maxX = cam.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        spawnY = cam.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y + 1f;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameActive) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEmoji();
        }
    }

    void SpawnEmoji()
    {
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPos = new Vector2(randomX, spawnY);

        GameObject emoji = ObjectPool.Instance.Get(emojiPrefab);
        emoji.transform.position = spawnPos;
        emoji.transform.localScale = Vector3.one * 0.08f;

        SpriteRenderer sr = emoji.GetComponent<SpriteRenderer>();
        sr.sprite = emojiSprites[Random.Range(0, emojiSprites.Length)];
    }
}