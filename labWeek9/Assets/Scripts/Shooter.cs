using UnityEngine;

/// Player-controlled shooter. Takes bullets from the pool (never instantiates them).
public class Shooter : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float fireCooldown = 0.25f;
    BulletPool pool;
    float nextFire;

    public void Setup(BulletPool bulletPool) => pool = bulletPool;

    void Update()
    {
        var cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect - 0.6f;

        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x + GameInput.Horizontal() * moveSpeed * Time.deltaTime, -halfWidth, halfWidth);
        transform.position = p;

        if (GameInput.FireHeld() && Time.time >= nextFire)
        {
            // Get() returns null when the pool is empty, so the player waits for a bullet to come back
            if (pool.Get(transform.position + Vector3.up * 0.7f) != null)
                nextFire = Time.time + fireCooldown;
        }
    }
}
