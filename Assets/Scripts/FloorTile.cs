using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour {

    public GameObject overlayUI;
    public int xIndex;
    public int zIndex;

    public Placeable placeable;
    private bool available = true;
    private float doubleClickTimer;
    private bool clicked = false;
    private float lightReceived = 1f;
    const float DOUBLE_CLICK_DELAY = 0.25f;

    private GameManager manager;

    private void Awake() {
        manager = GameManager.instance;
    }

    private void Update() {
        if (clicked) {
            if (Time.time - doubleClickTimer > DOUBLE_CLICK_DELAY) {
                clicked = false;
            }
        }
    }

    private void OnMouseEnter() {
        if (available) {
            overlayUI.SetActive(true);
        }
    }

    private void OnMouseExit() {
        overlayUI.SetActive(false);
    }

    private void OnMouseDown() {
        if (available) {
            // Place element above tile
            PlaceElement(manager.GetPlaceable());
            return;
        }

        if (!clicked) {
            clicked = true;
            doubleClickTimer = Time.time;
        } else {
            available = true;
            clicked = false;
            overlayUI.SetActive(true);
            // Remove element above tile
            RemoveElement();
        }
    }

    private void PlaceElement(Placeable placeablePrefab) {
        if (placeablePrefab == null) {
            // TODO: Indicate there was no element to place
            return;
        }
        placeable = Instantiate(placeablePrefab, transform);
        available = false;
        overlayUI.SetActive(false);
    }

    private void RemoveElement() {
        if (placeable != null) {
            manager.ReturnPlaceable(placeable);
            Destroy(placeable.gameObject);
        }
        placeable = null;
    }

    public void ReceiveShadow(float amount) {
        lightReceived -= amount;
        lightReceived = Mathf.Max(lightReceived, 0f);
    }

    public float GetReceivedLight() {
        return lightReceived;
    }
}
