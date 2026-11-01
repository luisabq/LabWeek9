using System.Collections.Generic;
using UnityEngine;

/// Reuses a fixed set of bullets during play.
public class BulletPool : MonoBehaviour
{
    readonly Queue<Bullet> available = new Queue<Bullet>();
    int total;

    public int Available => available.Count;
    public int Total => total;
    public int InUse => total - available.Count;

    public void Initialize(Bullet prefab, int size)
    {
        total = size;
        for (int i = 0; i < size; i++)
        {
            Bullet b = Instantiate(prefab, transform);
            b.Init(this);
            b.gameObject.SetActive(false);
            available.Enqueue(b);
        }
    }

    /// Returns null when every bullet is in use.
    public Bullet Get(Vector3 position)
    {
        if (available.Count == 0) return null;
        Bullet b = available.Dequeue();
        b.transform.position = position;
        b.gameObject.SetActive(true);
        return b;
    }

    public void Release(Bullet b)
    {
        b.gameObject.SetActive(false);
        available.Enqueue(b);
    }
}
