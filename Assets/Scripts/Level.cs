using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject {
    [System.Serializable]
    public class TilePlacement {
        public float x;
        public float z;
    }

    [System.Serializable]
    public class AvailablePlaceable {
        public Placeable placeable;
        public int amount;
    }

    public TilePlacement[] tiles;
    public AvailablePlaceable[] placeables;
    public Vector3 cameraPosition;

}
