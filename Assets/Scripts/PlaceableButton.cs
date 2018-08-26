using UnityEngine;
using System.Collections;

public class PlaceableButton : MonoBehaviour {

    public Placeable placeablePrefab;

    public void OnPress() {
        GameManager.instance.SetPlaceable(placeablePrefab);
    }

}
