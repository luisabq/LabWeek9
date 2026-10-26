using UnityEngine;

/// BUILDER PATTERN - Director.
/// Holds the premade "recipes" so other code never has to know the numbers.
public static class TargetDirector
{
    public enum Kind { Duck, FastBird, Bear }

    public static Target Build(Kind kind, Target template, Vector3 pos, int dir, Sprite[] sprites)
    {
        switch (kind)
        {
            case Kind.FastBird:
                return new TargetBuilder().SetSpeed(6f).SetSize(0.6f).SetPoints(30)
                    .SetSprite(sprites[1]).SetColor(new Color(0.3f, 1f, 0.4f)).Build(template, pos, dir);
            case Kind.Bear:
                return new TargetBuilder().SetSpeed(1.2f).SetSize(1.5f).SetPoints(5)
                    .SetSprite(sprites[2]).SetColor(new Color(0.95f, 0.95f, 0.95f)).Build(template, pos, dir);
            default: // Duck
                return new TargetBuilder().SetSpeed(3f).SetSize(0.9f).SetPoints(10)
                    .SetSprite(sprites[0]).SetColor(new Color(1f, 0.9f, 0.2f)).Build(template, pos, dir);
        }
    }
}
