using UnityEngine;
using System.Linq;

public class EmojiSpawner : MonoBehaviour
{
    public GameObject emojiPrefab;
    public Sprite[] emojiSprites;

    public float spawnInterval = 0.3f;
    private float timer;

    void Start()
    {
        if (emojiSprites == null || emojiSprites.Length == 0)
        {
            var allSprites = Resources.LoadAll<Sprite>("EmojiSheet");
            emojiSprites = allSprites.Where(s => s != null && s.name != "Css_sprites_0" && s.name != "Css_sprites_7").ToArray();
        }
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameActive) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEmoji();
            timer = 0f;
        }
    }

    void SpawnEmoji()
    {
        Camera cam = Camera.main;

        float minX = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        float maxX = cam.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        float spawnY = cam.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y + 1f;

        float screenWidth = maxX - minX;
        float emojiSize = screenWidth * 0.018f;
        float halfSize = emojiSize / 2f;

        float spawnMinX = minX + halfSize;
        float spawnMaxX = maxX - halfSize;

        float randomX = Random.Range(spawnMinX, spawnMaxX);
        Vector2 spawnPos = new Vector2(randomX, spawnY);

        GameObject emoji = ObjectPool.Instance.Get(emojiPrefab);
        emoji.transform.position = spawnPos;
        emoji.transform.localScale = Vector3.one * emojiSize;

        SpriteRenderer sr = emoji.GetComponent<SpriteRenderer>();
        sr.sprite = emojiSprites[Random.Range(0, emojiSprites.Length)];
    }
}