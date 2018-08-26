using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Transform placeableBar;
    public PlaceableButton placeableButtonPrefab;
    public Button goButtton;
    public GameObject endMessagePanel;

    private Dictionary<string, PlaceableButton> placeableButtons = new Dictionary<string, PlaceableButton>();
    private int totalCounter;

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

            placeableButtons[placeableData.placeable.id] = button;
            totalCounter += placeableData.amount;
        }
        CheckCounter();
    }

    public void ChangePlaceableAmount(Placeable usedPlaceable, bool increase) {
        if (increase) {
            placeableButtons[usedPlaceable.id].AddAmount();
            totalCounter++;
        } else {
            placeableButtons[usedPlaceable.id].LowerAmount();
            totalCounter--;
        }
        CheckCounter();
    }

    private void CheckCounter() {
        if (totalCounter <= 0) {
            goButtton.interactable = true;
        } else {
            goButtton.interactable = false;
        }
    }

    public void ShowEndPanel(bool outcome) {
        // TODO: change messages for negative outcome
        endMessagePanel.SetActive(true);
    }

    public void ClearEndPanel() {
        endMessagePanel.SetActive(false);
    }
}
