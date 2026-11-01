using System;
using System.Collections.Generic;
using UnityEngine;

/// A target made by TargetBuilder.
/// It reports hits without knowing who listens.
public class Target : MonoBehaviour
{
    /// ScoreManager listens for this event.
    public static event Action<int> OnTargetHit;

    /// The targets currently in the scene.
    public static readonly List<Target> Active = new List<Target>();

    public TargetDirector.Kind Kind { get; set; }
    public int Direction => direction;

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
        if (Mathf.Abs(transform.position.x) > limit) Destroy(gameObject); // Escaped, so no points.
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
        OnTargetHit?.Invoke(points);   // Tell the score manager.
        bullet.ReturnToPool();         // Reuse the bullet.
        Destroy(gameObject);
    }

    /// Removes the target without adding points.
    public void Discard()
    {
        dead = true;
        Active.Remove(this);
        Destroy(gameObject);
    }
}
