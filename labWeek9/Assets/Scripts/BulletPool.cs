using System.Collections.Generic;
using UnityEngine;

/// OBJECT POOL PATTERN
/// Pre-creates a fixed number of bullets. Shooting = Get(), bullet finished = Release().
/// No Instantiate/Destroy happens while the game is running.
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

    /// Returns null if every bullet is currently in flight (pool exhausted).
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
