﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Level[] levels;
    public int currentLevel;
    public GameObject tilePrefab;
    public Transform tilesParent;

    [SerializeField] private GameObject currentPlaceable;

    private List<GameObject> tiles = new List<GameObject>();

    #region Instance
    public static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            // There can be only one!
            Destroy(this.gameObject);
        }
    }
    #endregion

    public void SetPlaceable(GameObject newPlaceable) {
        // Return the old placeable to stack
        currentPlaceable = newPlaceable;
    }

    public GameObject GetPlaceable() {
        return currentPlaceable;
    }

    [ContextMenu("Clear Level")]
    public void ClearLevel() {
        foreach(GameObject tile in tiles) {
            if (!Application.isPlaying) {
                // Used when changing levels in the editor
                DestroyImmediate(tile);
            } else {
                Destroy(tile);
            }

        }
        tiles = new List<GameObject>();
    }

    [ContextMenu("Load Level")]
    public void LoadLevel() {
        ClearLevel();
        Level level = levels[currentLevel];
        foreach(Level.TilePlacement tilePlacement in level.tiles) {
            // Tiles are always on 0.5 to fit the grid
            Vector3 position = new Vector3(tilePlacement.x, 0.5f, tilePlacement.z);
            GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, null);
            tiles.Add(tile);
        }
    }

    public void SaveLevel() {

    }
}
