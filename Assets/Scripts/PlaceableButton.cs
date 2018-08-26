using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableButton : MonoBehaviour {

    public Placeable placeablePrefab;
    public int amount;
    public Text amountText;
    public Text nameText;

    private GameManager manager;

    private void Awake() {
        manager = GameManager.instance;
    }

    public void Setup() {
        amountText.text = amount.ToString();
        nameText.text = placeablePrefab.name;
    }

    public void OnPress() {
        GameManager.instance.SetPlaceable(placeablePrefab);
    }

}
