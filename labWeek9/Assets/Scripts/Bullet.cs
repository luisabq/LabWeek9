using UnityEngine;

/// A pooled projectile. It is never destroyed: it returns itself to the BulletPool.
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 12f;
    const float HitRadius = 0.15f;
    BulletPool pool;

    public void Init(BulletPool owner) => pool = owner;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Check for a hit against every live target
        for (int i = Target.Active.Count - 1; i >= 0; i--)
        {
            Target t = Target.Active[i];
            if (t.Overlaps(transform.position, HitRadius))
            {
                t.Hit(this);
                return;
            }
        }

        // Left the top of the screen -> give it back to the pool
        var cam = Camera.main;
        if (transform.position.y > cam.transform.position.y + cam.orthographicSize + 1f)
            ReturnToPool();
    }

    public void ReturnToPool()
    {
        if (gameObject.activeSelf) pool.Release(this);
    }
}