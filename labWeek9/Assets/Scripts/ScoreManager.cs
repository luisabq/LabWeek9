using UnityEngine;

/// Tracks the score by listening for target hits.
public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }
    public int Hits { get; private set; }

    void OnEnable()  => Target.OnTargetHit += HandleTargetHit;
    void OnDisable() => Target.OnTargetHit -= HandleTargetHit;

    void HandleTargetHit(int points)
    {
        Score += points;
        Hits++;
    }

    /// Restores a score from a save.
    public void SetScore(int score, int hits)
    {
        Score = score;
        Hits = hits;
    }
}
