using UnityEngine;
using System.Collections;

public class PlaceableButton : MonoBehaviour {

    public GameObject placeablePrefab;

    public void OnPress() {
        GameManager.instance.SetPlaceable(placeablePrefab);
    }

}
