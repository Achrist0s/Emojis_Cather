using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    private Dictionary<GameObject, Queue<GameObject>> pool = new Dictionary<GameObject, Queue<GameObject>>();

    public GameObject Get(GameObject prefab)
    {
        if (!pool.ContainsKey(prefab))
            pool[prefab] = new Queue<GameObject>();

        if (pool[prefab].Count > 0)
        {
            GameObject obj = pool[prefab].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject newObj = Instantiate(prefab);
        ParticleSystem ps = newObj.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Stop();
            ps.Play();
            newObj.AddComponent<ReturnAfterEffect>();
        }
        return newObj;
    }

    public void ReturnToPool(GameObject prefab, GameObject obj)
    {
        obj.SetActive(false);
        if (!pool.ContainsKey(prefab))
            pool[prefab] = new Queue<GameObject>();
        pool[prefab].Enqueue(obj);
    }
    public void DeactivateAll()
    {
        foreach (var kvp in pool)
        {
            foreach (var obj in kvp.Value)
            {
                if (obj != null && obj.activeInHierarchy)
                    obj.SetActive(false);
            }
        }
    }
}

public class ReturnAfterEffect : MonoBehaviour
{
    private ParticleSystem ps;
    private GameObject prefab;

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        prefab = gameObject;
        if (ps != null)
            StartCoroutine(WaitAndReturn());
    }

    System.Collections.IEnumerator WaitAndReturn()
    {
        yield return new WaitUntil(() => !ps.IsAlive(true));
        gameObject.SetActive(false);
    }
}
