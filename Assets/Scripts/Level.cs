﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject {
    [System.Serializable]
    public class TilePlacement {
        public float x;
        public float z;
    }

    public TilePlacement[] tiles;

}
