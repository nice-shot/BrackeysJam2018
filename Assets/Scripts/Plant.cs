using UnityEngine;
using System.Collections;


/// <summary>
/// A planet that can be placed on the garden
/// </summary>
public class Plant : Placeable {
    public enum SunNeeds {
        Total,
        Half,
        ThreeQuarters,
        None
    }

    public SunNeeds sunReq;
}
