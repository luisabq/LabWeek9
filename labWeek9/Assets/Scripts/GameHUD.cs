using UnityEngine;

/// Simple on-screen text using OnGUI (no Canvas/TMP setup required).
public class GameHUD : MonoBehaviour
{
    ScoreManager score;
    BulletPool pool;
    GameBootstrap game;
    GUIStyle style, big;

    public void Setup(ScoreManager s, BulletPool p, GameBootstrap g) { score = s; pool = p; game = g; }

    void OnGUI()
    {
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.label) { fontSize = 26, fontStyle = FontStyle.Bold };
            style.normal.textColor = Color.white;
            big = new GUIStyle(style) { fontSize = 48, alignment = TextAnchor.MiddleCenter };
        }

        GUI.Label(new Rect(15, 10, 400, 40), $"Score: {score.Score}", style);
        GUI.Label(new Rect(15, 45, 500, 40), $"Bullets: {pool.Available}/{pool.Total}  (in flight: {pool.InUse})", style);
        GUI.Label(new Rect(Screen.width - 230, 10, 220, 40), $"Time: {Mathf.CeilToInt(game.TimeLeft)}", style);

        if (game.IsOver)
        {
            GUI.Label(new Rect(0, Screen.height / 2f - 80, Screen.width, 60), "TIME'S UP!", big);
            GUI.Label(new Rect(0, Screen.height / 2f - 20, Screen.width, 60), $"Final score: {score.Score}", big);
            GUI.Label(new Rect(0, Screen.height / 2f + 40, Screen.width, 60), "Press R to play again", style.Centered());
        }
        else
        {
            GUI.Label(new Rect(15, Screen.height - 40, Screen.width, 40), "A/D or Arrows: move   Space: shoot", style);
        }
    }
}

static class StyleExt
{
    public static GUIStyle Centered(this GUIStyle s)
    {
        var c = new GUIStyle(s) { alignment = TextAnchor.MiddleCenter };
        return c;
    }
}
