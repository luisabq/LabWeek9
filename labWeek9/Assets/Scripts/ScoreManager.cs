using UnityEngine;

/// OBSERVER PATTERN - the observer.
/// Subscribes to Target.OnTargetHit; Target never references this class.
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
}
