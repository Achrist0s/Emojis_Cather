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
        float randomX = Random.Range(-2.5f, 2.5f);
        Vector2 spawnPos = new Vector2(randomX, 6f);

        GameObject emoji = ObjectPool.Instance.Get(emojiPrefab);
        emoji.transform.position = spawnPos;
        emoji.transform.localScale = Vector3.one * 0.1f;

        SpriteRenderer sr = emoji.GetComponent<SpriteRenderer>();
        sr.sprite = emojiSprites[Random.Range(0, emojiSprites.Length)];
    }
}