using UnityEngine;

/// Spawns random target types from the side of the screen using the TargetDirector.
public class TargetSpawner : MonoBehaviour
{
    [SerializeField] float interval = 0.9f;
    Target template;
    Sprite[] sprites;
    float timer;

    public void Setup(Target targetTemplate, Sprite[] targetSprites)
    {
        template = targetTemplate;
        sprites = targetSprites;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;
        timer = interval * Random.Range(0.7f, 1.3f);

        var cam = Camera.main;
        float edge = cam.orthographicSize * cam.aspect + 1.5f;
        int dir = Random.value < 0.5f ? 1 : -1;
        float y = Random.Range(0.3f, cam.orthographicSize - 1.5f);
        Vector3 pos = new Vector3(-dir * edge, y, 0f);

        // weighted: ducks common, birds medium, bears less common
        float r = Random.value;
        var kind = r < 0.5f ? TargetDirector.Kind.Duck
                 : r < 0.8f ? TargetDirector.Kind.FastBird
                            : TargetDirector.Kind.Bear;

        TargetDirector.Build(kind, template, pos, dir, sprites);
    }
}
