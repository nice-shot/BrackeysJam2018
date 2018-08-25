﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour {

    public GameObject overlayUI;

    private GameObject placeable;
    private bool available = true;
    private float doubleClickTimer;
    private bool clicked = false;
    const float DOUBLE_CLICK_DELAY = 0.25f;

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
            PlaceElement(GameManager.instance.GetPlaceable());
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

    private void PlaceElement(GameObject placeablePrefab) {
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
            Destroy(placeable);
        }
        placeable = null;
    }
}
