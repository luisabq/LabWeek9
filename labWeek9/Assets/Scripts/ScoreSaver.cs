using System.IO;
using UnityEngine;

/// Saves and loads the score as binary data.
public static class ScoreSaver
{
    static string FilePath => Path.Combine(Application.persistentDataPath, "score.dat");

    public static void Save(int score, int hits)
    {
        using (var writer = new BinaryWriter(File.Open(FilePath, FileMode.Create)))
        {
            writer.Write(score);
            writer.Write(hits);
        }
        Debug.Log("Score saved to: " + FilePath);
    }

    public static bool TryLoad(out int score, out int hits)
    {
        score = 0; hits = 0;
        if (!File.Exists(FilePath)) return false;

        using (var reader = new BinaryReader(File.Open(FilePath, FileMode.Open)))
        {
            score = reader.ReadInt32();
            hits = reader.ReadInt32();
        }
        return true;
    }
}
