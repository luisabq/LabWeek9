using UnityEngine;

/// Displays the score with Unity's built-in GUI.
public class GameHUD : MonoBehaviour
{
    ScoreManager score;
    BulletPool pool;
    GameBootstrap game;
    SaveLoadManager saveLoad;
    GUIStyle style, big, center;

    public void Setup(ScoreManager s, BulletPool p, GameBootstrap g, SaveLoadManager sl)
    {
        score = s; pool = p; game = g; saveLoad = sl;
    }

    void OnGUI()
    {
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.label) { fontSize = 26, fontStyle = FontStyle.Bold };
            style.normal.textColor = Color.white;
            big = new GUIStyle(style) { fontSize = 48, alignment = TextAnchor.MiddleCenter };
            center = new GUIStyle(style) { alignment = TextAnchor.MiddleCenter };
        }

        GUI.Label(new Rect(15, 10, 400, 40), $"Score: {score.Score}", style);
        GUI.Label(new Rect(15, 45, 500, 40), $"Bullets: {pool.Available}/{pool.Total}  (in flight: {pool.InUse})", style);
        GUI.Label(new Rect(Screen.width - 230, 10, 220, 40), $"Time: {Mathf.CeilToInt(game.TimeLeft)}", style);

        string msg = saveLoad.Message;
        if (msg != "")
            GUI.Label(new Rect(0, 90, Screen.width, 40), msg, center);

        if (game.IsOver)
        {
            GUI.Label(new Rect(0, Screen.height / 2f - 80, Screen.width, 60), "TIME'S UP!", big);
            GUI.Label(new Rect(0, Screen.height / 2f - 20, Screen.width, 60), $"Final score: {score.Score}", big);
            GUI.Label(new Rect(0, Screen.height / 2f + 40, Screen.width, 60), "Press R to play again", center);
        }
        else
        {
            GUI.Label(new Rect(15, Screen.height - 40, Screen.width, 40),
                "A/D or Arrows: move   Space: shoot   S: save   L: load", style);
        }
    }
}
