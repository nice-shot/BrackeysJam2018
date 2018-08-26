using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public Transform placeableBar;
    public PlaceableButton placeableButtonPrefab;

    private Dictionary<Placeable, PlaceableButton> placeableButtons = new Dictionary<Placeable, PlaceableButton>();

    public void SetPlaceableButtons(Level level) {
        foreach (Level.AvailablePlaceable placeableData in level.placeables) {
            PlaceableButton button = Instantiate(placeableButtonPrefab, placeableBar);
            button.transform.SetAsFirstSibling();
            button.placeablePrefab = placeableData.placeable;

            // TODO: Set number of avaliable placeables for this category

            placeableButtons[placeableData.placeable] = button;
        }
    }
}
