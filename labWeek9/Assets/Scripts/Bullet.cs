using UnityEngine;

/// Bullet reused by the pool instead of destroyed.
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 12f;
    const float HitRadius = 0.15f;
    BulletPool pool;

    public void Init(BulletPool owner) => pool = owner;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Check the live targets for a hit.
        for (int i = Target.Active.Count - 1; i >= 0; i--)
        {
            Target t = Target.Active[i];
            if (t.Overlaps(transform.position, HitRadius))
            {
                t.Hit(this);
                return;
            }
        }

        // Return it once it leaves the screen.
        var cam = Camera.main;
        if (transform.position.y > cam.transform.position.y + cam.orthographicSize + 1f)
            ReturnToPool();
    }

    public void ReturnToPool()
    {
        if (gameObject.activeSelf) pool.Release(this);
    }
}
