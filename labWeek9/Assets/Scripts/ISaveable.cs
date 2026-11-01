/// Implement this on anything that needs save data.
public interface ISaveable
{
    string SaveID { get; }               // ID used to match saved data.
    SaveRecord SavedData { get; }        // Data written to the save file.
    void LoadFromData(SaveRecord data);  // Restore the object.
}
