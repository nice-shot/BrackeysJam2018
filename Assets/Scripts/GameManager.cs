using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Level[] levels;
    public int currentLevel;
    public FloorTile tilePrefab;
    public Transform tilesParent;
    public UIManager uiManager;
    public Animator animator;
    private bool levelPassed;

    private FloorTile[,] tileGrid;

    [SerializeField] private Placeable currentPlaceable;

    private List<FloorTile> tiles = new List<FloorTile>();


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
        Placeable returnPlaceable = null;
        if (currentPlaceable != null) {
            uiManager.ChangePlaceableAmount(currentPlaceable, false);
            returnPlaceable = currentPlaceable;
        }
        currentPlaceable = null;
        return returnPlaceable;
    }

    public void ReturnPlaceable(Placeable placeable) {
        uiManager.ChangePlaceableAmount(placeable, true);
    }

    public void ClearLevel() {
        // Reset selected placeable
        currentPlaceable = null;

        // Clear tiles
        foreach(FloorTile tile in tiles) {
            if (!Application.isPlaying) {
                // Used when changing levels in the editor
                DestroyImmediate(tile.gameObject);
            } else {
                Destroy(tile.gameObject);
            }

        }
        tiles = new List<FloorTile>();
        // Default large size - probably won't reach this
        tileGrid = new FloorTile[20, 20];

        // Clear UI
        uiManager.ClearButtons();
    }

    public void LoadLevel() {
        ClearLevel();

        currentPlaceable = null;
        levelPassed = false;

        Level level = levels[currentLevel];

        // Place tiles
        foreach (Level.TilePlacement tilePlacement in level.tiles) {
            // Tiles are always on 0.5 on y to pop out of ground
            Vector3 position = new Vector3(tilePlacement.x, 0.5f, tilePlacement.z);
            FloorTile tile = Instantiate(tilePrefab, position, Quaternion.identity, tilesParent);
            tiles.Add(tile);
            tile.xIndex = (int)tilePlacement.x;
            tile.zIndex = (int)tilePlacement.z;
            tileGrid[tile.xIndex, tile.zIndex] = tile;
        }

        // Set Camera
        Camera.main.transform.position = level.cameraPosition;

        // Set UI
        uiManager.SetPlaceableButtons(level);

    }

    public void SaveLevel() {
        
        // Reset the tile list
        tiles = new List<FloorTile>();

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
            tiles.Add(tile);
        }

        // Set camera
        level.cameraPosition = Camera.main.transform.position;

        EditorUtility.SetDirty(level);
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }

    public void RunLevel() {
        animator.SetBool("ChangeLight", true);

        // Calculate lights (more accurately shadows)
        foreach (FloorTile tile in tiles) {
            if (tile.placeable is Wall) {
                // Inform light data:

                // Full light to tile above
                AddShadow(tile.xIndex, tile.zIndex + 1, 1f);

                // Quarter light to sides
                AddShadow(tile.xIndex + 1, tile.zIndex, 0.25f);
                AddShadow(tile.xIndex - 1, tile.zIndex, 0.25f);

                // Half light to diagonals
                AddShadow(tile.xIndex + 1, tile.zIndex + 1, 0.5f);
                AddShadow(tile.xIndex - 1, tile.zIndex + 1, 0.5f);
            }
        }

        // Check if level passed
        foreach (FloorTile tile in tiles) {
            if (tile.placeable is Plant) {
                Plant plant = tile.placeable as Plant;
                if (plant.sunNeeds != tile.GetReceivedLight()) {
                    Debug.Log("Plant on tile: " + tile.xIndex + "/" + tile.zIndex + " didn't get correct light");
                    return;
                }
            }
        }

        levelPassed = true;
    }

    private void AddShadow(int x, int z, float amount) {
        // Check that we're in grid bounds
        if (x < 0 || x >= tileGrid.Length
            || z < 0 || z >= tileGrid.Length) {
            return;
        }

        FloorTile tile = tileGrid[x, z];
        if (tile != null) {
            tile.ReceiveShadow(amount);
        }
    }

    public void OnLightAnimationEnd() {
        uiManager.ShowEndPanel(levelPassed);
    }

    public void ReloadLevel() {
        uiManager.ClearEndPanel();
        animator.SetBool("ChangeLight", false);
        LoadLevel();
    }

    public void NextLevel() {
        uiManager.ClearEndPanel();
        animator.SetBool("ChangeLight", false);
        ClearLevel();
        currentLevel = (currentLevel + 1) % levels.Length;
        LoadLevel();
    }
}
