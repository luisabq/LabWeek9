using UnityEngine;
using UnityEngine.SceneManagement;

/// Put this on ONE empty GameObject in an empty scene. It builds the whole game at runtime.
/// Optional: drag Kenney sprites into the slots below to replace the placeholder shapes.
public class GameBootstrap : MonoBehaviour
{
    [Header("Optional sprites (leave empty for placeholders)")]
    [SerializeField] Sprite shooterSprite;
    [SerializeField] Sprite bulletSprite;
    [SerializeField] Sprite duckSprite;
    [SerializeField] Sprite birdSprite;
    [SerializeField] Sprite bearSprite;

    [Header("Settings")]
    [SerializeField] int poolSize = 8;
    [SerializeField] float roundSeconds = 60f;

    public float TimeLeft { get; private set; }
    public bool IsOver { get; private set; }

    Shooter shooter;
    TargetSpawner spawner;

    void Awake()
    {
        TimeLeft = roundSeconds;
        SetupCamera();

        Sprite square = SpriteFactory.Square();
        Sprite circle = SpriteFactory.Circle();
        Sprite tri = SpriteFactory.Triangle();

        // ---- Object Pool ----
        Bullet bulletTemplate = MakeBulletTemplate(bulletSprite != null ? bulletSprite : circle);
        var pool = new GameObject("BulletPool").AddComponent<BulletPool>();
        pool.Initialize(bulletTemplate, poolSize);

        // ---- Shooter ----
        var shooterGO = new GameObject("Shooter");
        var sr = shooterGO.AddComponent<SpriteRenderer>();
        sr.sprite = shooterSprite != null ? shooterSprite : tri;
        sr.color = new Color(0.3f, 0.8f, 1f);
        shooterGO.transform.position = new Vector3(0f, -Camera.main.orthographicSize + 0.9f, 0f);
        shooterGO.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        shooter = shooterGO.AddComponent<Shooter>();
        shooter.Setup(pool);

        // ---- Builder (spawner uses the Director) ----
        Target targetTemplate = MakeTargetTemplate();
        spawner = new GameObject("TargetSpawner").AddComponent<TargetSpawner>();
        spawner.Setup(targetTemplate, new[]
        {
            duckSprite != null ? duckSprite : circle,
            birdSprite != null ? birdSprite : tri,
            bearSprite != null ? bearSprite : square
        });

        // ---- Observer ----
        var score = new GameObject("ScoreManager").AddComponent<ScoreManager>();

        // ---- HUD ----
        new GameObject("HUD").AddComponent<GameHUD>().Setup(score, pool, this);
    }

    void Update()
    {
        if (!IsOver)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft <= 0f)
            {
                TimeLeft = 0f;
                IsOver = true;
                shooter.enabled = false;
                spawner.enabled = false;
            }
        }
        else if (GameInput.RestartPressed())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void SetupCamera()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            var go = new GameObject("Main Camera") { tag = "MainCamera" };
            cam = go.AddComponent<Camera>();
        }
        cam.orthographic = true;
        cam.orthographicSize = 5f;
        cam.transform.position = new Vector3(0f, 0f, -10f);
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.12f, 0.06f, 0.2f);
    }

    Bullet MakeBulletTemplate(Sprite sprite)
    {
        var go = new GameObject("BulletTemplate");
        go.transform.SetParent(transform);
        go.SetActive(false);
        go.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.color = new Color(1f, 0.4f, 0.3f);
        var rb = go.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        go.AddComponent<CircleCollider2D>().isTrigger = true;
        return go.AddComponent<Bullet>();
    }

    Target MakeTargetTemplate()
    {
        var go = new GameObject("TargetTemplate");
        go.transform.SetParent(transform);
        go.SetActive(false);
        go.AddComponent<SpriteRenderer>();
        var rb = go.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        go.AddComponent<BoxCollider2D>().isTrigger = true;
        return go.AddComponent<Target>();
    }
}
