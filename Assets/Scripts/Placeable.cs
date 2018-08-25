using UnityEngine;
using System.Collections;

/// <summary>
/// An object that can be placed on the garden
/// </summary>
public abstract class Placeable : MonoBehaviour {
    public enum Types {
        Wall,
        Plant
    }
}
