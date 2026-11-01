using UnityEngine;

/// Builds a target from a few settings.
public class TargetBuilder
{
    float speed = 2f;
    float size = 1f;
    int points = 10;
    Sprite sprite;
    Color color = Color.white;

    public TargetBuilder SetSpeed(float value)  { speed = value;  return this; }
    public TargetBuilder SetSize(float value)   { size = value;   return this; }
    public TargetBuilder SetPoints(int value)   { points = value; return this; }
    public TargetBuilder SetSprite(Sprite value){ sprite = value; return this; }
    public TargetBuilder SetColor(Color value)  { color = value;  return this; }

    public Target Build(Target template, Vector3 position, int direction)
    {
        Target t = Object.Instantiate(template, position, Quaternion.identity);
        t.Configure(speed, size, points, sprite, color, direction);
        t.gameObject.SetActive(true);
        return t;
    }
}
