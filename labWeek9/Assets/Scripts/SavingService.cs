using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// Reads and writes save data as JSON.
public static class SavingService
{
    static string FilePath => Path.Combine(Application.persistentDataPath, "locations.json");

    public static void SaveLocations(IEnumerable<ISaveable> saveables)
    {
        var file = new SaveFile();
        foreach (ISaveable s in saveables)
            file.objects.Add(s.SavedData);

        File.WriteAllText(FilePath, JsonUtility.ToJson(file, true));
        Debug.Log("Locations saved to: " + FilePath);
    }

    /// Returns null when no save exists yet.
    public static SaveFile LoadLocations()
    {
        if (!File.Exists(FilePath)) return null;
        return JsonUtility.FromJson<SaveFile>(File.ReadAllText(FilePath));
    }
}
