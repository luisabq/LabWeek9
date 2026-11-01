using System;
using System.Collections.Generic;
using UnityEngine;

/// Saves and restores an object's local transform.
/// Used by the player and targets.
public class TransformSaver : MonoBehaviour, ISaveable
{
    /// All active saveable objects.
    public static readonly List<TransformSaver> All = new List<TransformSaver>();

    [SerializeField] string saveID;

    public string SaveID => saveID;
    public void SetSaveID(string id) => saveID = id;

    void OnEnable()
    {
        if (string.IsNullOrEmpty(saveID)) saveID = Guid.NewGuid().ToString();
        All.Add(this);
    }

    void OnDisable() => All.Remove(this);

    public SaveRecord SavedData
    {
        get
        {
            // Keep target details so they can be rebuilt on load.
            Target target = GetComponent<Target>();
            return new SaveRecord
            {
                saveID = saveID,
                type = target != null ? target.Kind.ToString() : "Player",
                direction = target != null ? target.Direction : 0,
                localPosition = transform.localPosition,
                localRotation = transform.localRotation,
                localScale = transform.localScale
            };
        }
    }

    public void LoadFromData(SaveRecord data)
    {
        transform.localPosition = data.localPosition;
        transform.localRotation = data.localRotation;
        transform.localScale = data.localScale;
    }
}
