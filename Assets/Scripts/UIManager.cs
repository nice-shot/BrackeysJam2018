using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UIManager : MonoBehaviour {
    public Transform placeableBar;
    public PlaceableButton placeableButtonPrefab;

    private Dictionary<Placeable, PlaceableButton> placeableButtons = new Dictionary<Placeable, PlaceableButton>();

    public void ClearButtons() {
        foreach (PlaceableButton button in placeableButtons.Values) {
            if (!Application.isPlaying) {
                DestroyImmediate(button.gameObject);
            } else {
                Destroy(button.gameObject);
            }
        }
        placeableButtons.Clear();
    }

    public void SetPlaceableButtons(Level level) {
        foreach (Level.AvailablePlaceable placeableData in level.placeables) {
            PlaceableButton button = Instantiate(placeableButtonPrefab, placeableBar);
            button.transform.SetAsFirstSibling();
            button.placeablePrefab = placeableData.placeable;
            button.amount = placeableData.amount;

            // Used instead of Awake/Start to make sure added objects are placed
            button.Setup();

            placeableButtons[placeableData.placeable] = button;
        }
    }
}
