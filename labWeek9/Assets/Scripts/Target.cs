using System;
using System.Collections.Generic;
using UnityEngine;

/// The product built by TargetBuilder. Also the SUBJECT of the OBSERVER pattern:
/// when a bullet hits it, it raises OnTargetHit and doesn't know who is listening.
public class Target : MonoBehaviour
{
    /// OBSERVER PATTERN: static event that observers (ScoreManager) subscribe to.
    public static event Action<int> OnTargetHit;

    /// All live targets, so bullets can check for hits.
    public static readonly List<Target> Active = new List<Target>();

    float speed;
    int direction = 1;
    int points;
    bool dead;

    void OnEnable()  => Active.Add(this);
    void OnDisable() => Active.Remove(this);

    public void Configure(float speed, float size, int points, Sprite sprite, Color color, int direction)
    {
        this.speed = speed;
        this.points = points;
        this.direction = direction;
        transform.localScale = new Vector3(size, size, 1f);
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.color = color;
    }

    void Update()
    {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        var cam = Camera.main;
        float limit = cam.orthographicSize * cam.aspect + 2f;
        if (Mathf.Abs(transform.position.x) > limit) Destroy(gameObject); // escaped, no points
    }

    public bool Overlaps(Vector3 point, float radius)
    {
        float half = transform.localScale.x * 0.5f;
        return Mathf.Abs(point.x - transform.position.x) < half + radius &&
               Mathf.Abs(point.y - transform.position.y) < half + radius;
    }

    public void Hit(Bullet bullet)
    {
        if (dead) return;
        dead = true;
        OnTargetHit?.Invoke(points);   // notify observers
        bullet.ReturnToPool();         // bullet goes back to the pool
        Destroy(gameObject);
    }
}