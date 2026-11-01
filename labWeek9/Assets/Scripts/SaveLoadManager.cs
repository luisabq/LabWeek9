using System;
using System.Collections.Generic;
using UnityEngine;

/// Press S to save and L to load.
/// Scores use a binary file; object locations use JSON.
public class SaveLoadManager : MonoBehaviour
{
    TargetSpawner spawner;
    ScoreManager score;
    GameBootstrap game;

    string message = "";
    float messageUntil;

    public string Message => Time.time < messageUntil ? message : "";

    public void Setup(TargetSpawner sp, ScoreManager sc, GameBootstrap g)
    {
        spawner = sp; score = sc; game = g;
    }

    void Update()
    {
        if (game.IsOver) return;
        if (GameInput.SavePressed()) Save();
        if (GameInput.LoadPressed()) Load();
    }

    void Save()
    {
        // Save the player and targets.
        var saveables = new List<ISaveable>();
        foreach (TransformSaver s in TransformSaver.All) saveables.Add(s);

        SavingService.SaveLocations(saveables);
        ScoreSaver.Save(score.Score, score.Hits);
        Show($"Game saved ({Target.Active.Count} targets)");
    }

    void Load()
    {
        SaveFile file = SavingService.LoadLocations();
        if (file == null || !ScoreSaver.TryLoad(out int savedScore, out int savedHits))
        {
            Show("No save found - press S first");
            return;
        }

        // Clear current targets before rebuilding the save.
        foreach (Target t in new List<Target>(Target.Active)) t.Discard();

        foreach (SaveRecord r in file.objects)
        {
            if (r.type == "Player")
            {
                // The player stays in the scene, so restore it directly.
                foreach (TransformSaver s in TransformSaver.All)
                    if (s.SaveID == r.saveID) s.LoadFromData(r);
            }
            else if (Enum.TryParse(r.type, out TargetDirector.Kind kind))
            {
                // Build the target, then restore its saved transform.
                Target t = spawner.Spawn(kind, r.localPosition, r.direction);
                t.GetComponent<TransformSaver>().LoadFromData(r);
            }
        }

        score.SetScore(savedScore, savedHits);
        Show("Game loaded");
    }

    void Show(string text)
    {
        message = text;
        messageUntil = Time.time + 2f;
    }
}
