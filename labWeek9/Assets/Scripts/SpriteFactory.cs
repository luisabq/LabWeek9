using UnityEngine;

/// Makes simple sprites when no art is assigned.
public static class SpriteFactory
{
    const int Size = 32;

    public static Sprite Square() => Make((x, y) => true);

    public static Sprite Circle()
    {
        float r = Size / 2f;
        return Make((x, y) =>
        {
            float dx = x - r + 0.5f, dy = y - r + 0.5f;
            return dx * dx + dy * dy <= r * r;
        });
    }

    public static Sprite Triangle() =>
        Make((x, y) => Mathf.Abs(x - Size / 2f + 0.5f) <= (Size - y) / 2f);

    static Sprite Make(System.Func<int, int, bool> inside)
    {
        var tex = new Texture2D(Size, Size) { filterMode = FilterMode.Point };
        for (int y = 0; y < Size; y++)
            for (int x = 0; x < Size; x++)
                tex.SetPixel(x, y, inside(x, y) ? Color.white : Color.clear);
        tex.Apply();
        // This keeps the sprite one world unit wide.
        return Sprite.Create(tex, new Rect(0, 0, Size, Size), new Vector2(0.5f, 0.5f), Size);
    }
}
