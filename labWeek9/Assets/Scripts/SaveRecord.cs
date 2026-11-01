using System;
using System.Collections.Generic;
using UnityEngine;

/// The data saved for one object.
[Serializable]
public class SaveRecord
{
    public string saveID;
    public string type;        // Player, Duck, FastBird, or Bear.
    public int direction;      // Movement direction; 0 for the player.
    public Vector3 localPosition;
    public Quaternion localRotation;
    public Vector3 localScale;
}

/// The complete save file.
[Serializable]
public class SaveFile
{
    public List<SaveRecord> objects = new List<SaveRecord>();
}
