using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject currentPlaceable;

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
}
