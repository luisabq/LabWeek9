using UnityEngine;

/// Moves the player and fires pooled bullets.
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

        // S is reserved for saving.
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x + GameInput.Horizontal() * moveSpeed * Time.deltaTime, -halfWidth, halfWidth);
        transform.position = p;

        if (GameInput.FireHeld() && Time.time >= nextFire)
        {
            // Wait if every bullet is already in flight.
            if (pool.Get(transform.position + Vector3.up * 0.7f) != null)
                nextFire = Time.time + fireCooldown;
        }
    }
}
