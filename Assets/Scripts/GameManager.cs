using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Level[] levels;
    public int currentLevel;
    public GameObject tilePrefab;
    public Transform tilesParent;
    public UIManager uiManager;
    public Animator animator;

    [SerializeField] private Placeable currentPlaceable;

    private List<GameObject> tiles = new List<GameObject>();


    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            // There can be only one!
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        LoadLevel();
    }

    public void SetPlaceable(Placeable newPlaceable) {
        // Return the old placeable to stack
        currentPlaceable = newPlaceable;
    }

    public Placeable GetPlaceable() {
        if (currentPlaceable != null) {
            uiManager.ChangePlaceableAmount(currentPlaceable, false);
        }
        return currentPlaceable;
    }

    public void ReturnPlaceable(Placeable placeable) {
        uiManager.ChangePlaceableAmount(placeable, true);
    }

    public void ClearLevel() {
        // Reset selected placeable
        currentPlaceable = null;

        // Clear tiles
        foreach(GameObject tile in tiles) {
            if (!Application.isPlaying) {
                // Used when changing levels in the editor
                DestroyImmediate(tile);
            } else {
                Destroy(tile);
            }

        }
        tiles = new List<GameObject>();

        // Clear UI
        uiManager.ClearButtons();
    }

    public void LoadLevel() {
        ClearLevel();

        Level level = levels[currentLevel];

        // Place tiles
        foreach (Level.TilePlacement tilePlacement in level.tiles) {
            // Tiles are always on 0.5 to fit the grid
            Vector3 position = new Vector3(tilePlacement.x, 0.5f, tilePlacement.z);
            GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, tilesParent);
            tiles.Add(tile);
        }

        // Set Camera
        Camera.main.transform.position = level.cameraPosition;

        // Set UI
        uiManager.SetPlaceableButtons(level);

    }

    public void SaveLevel() {
        
        // Reset the tile list
        tiles = new List<GameObject>();

        // Find tiles
        FloorTile[] currentTiles;
        if (tilesParent != null) {
            currentTiles = tilesParent.GetComponentsInChildren<FloorTile>();
        } else {
            currentTiles = GetComponents<FloorTile>();
        }

        // Set tile positions in level scriptable object
        Level level = levels[currentLevel];
        level.tiles = new Level.TilePlacement[currentTiles.Length];
        for (int i = 0; i < currentTiles.Length; i++) {
            FloorTile tile = currentTiles[i];
            Level.TilePlacement tilePlacement = new Level.TilePlacement();
            tilePlacement.x = tile.transform.position.x;
            tilePlacement.z = tile.transform.position.z;
            level.tiles[i] = tilePlacement;
            tiles.Add(tile.gameObject);
        }

        // Set camera
        level.cameraPosition = Camera.main.transform.position;

        EditorUtility.SetDirty(level);
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }

    public void RunLevel() {
        animator.SetBool("ChangeLight", true);

        // We'll show the level's output when it's over. Though we'll check the level outcome here
    }

    public void OnLightAnimationEnd() {
        // TODO: Check if the level passed on print message
        uiManager.ShowEndPanel(true);
    }

    public void NextLevel() {
        uiManager.ClearEndPanel();
        animator.SetBool("ChangeLight", false);
        currentLevel = (currentLevel + 1) % levels.Length;
        LoadLevel();
    }
}
