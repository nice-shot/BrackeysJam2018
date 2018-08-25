using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileController : MonoBehaviour {

    public GameObject overlayUI;

    private bool available = true;
    private float doubleClickTimer;
    private bool clicked = false;
    const float DOUBLE_CLICK_DELAY = 0.25f;

    private void Update() {
        if (clicked) {
            if (Time.time - doubleClickTimer > DOUBLE_CLICK_DELAY) {
                Debug.Log("Clearing click");
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
            Debug.Log("Clicked when available");
            available = false;
            overlayUI.SetActive(false);
            //TODO: Place element above tile

            return;
        }

        if (!clicked) {
            Debug.Log("First click");
            clicked = true;
            doubleClickTimer = Time.time;
        } else {
            Debug.Log("Second click");
            available = true;
            clicked = false;
            overlayUI.SetActive(true);
            // TODO: Clear element above tile
        }
    }
}
