using UnityEngine;

/// Holds the target settings used by the builder.
public static class TargetDirector
{
    public enum Kind { Duck, FastBird, Bear }

    public static Target Build(Kind kind, Target template, Vector3 pos, int dir, Sprite[] sprites)
    {
        Target t;
        switch (kind)
        {
            case Kind.FastBird:
                t = new TargetBuilder().SetSpeed(6f).SetSize(0.6f).SetPoints(30)
                    .SetSprite(sprites[1]).SetColor(new Color(0.3f, 1f, 0.4f)).Build(template, pos, dir);
                break;
            case Kind.Bear:
                t = new TargetBuilder().SetSpeed(1.2f).SetSize(1.5f).SetPoints(5)
                    .SetSprite(sprites[2]).SetColor(new Color(0.95f, 0.95f, 0.95f)).Build(template, pos, dir);
                break;
            default: // Use a duck by default.
                t = new TargetBuilder().SetSpeed(3f).SetSize(0.9f).SetPoints(10)
                    .SetSprite(sprites[0]).SetColor(new Color(1f, 0.9f, 0.2f)).Build(template, pos, dir);
                break;
        }
        t.Kind = kind; // Keep it for save/load.
        return t;
    }
}
