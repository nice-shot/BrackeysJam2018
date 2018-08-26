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
        // TODO: Add icon
    }

    public void OnPress() {
        GameManager.instance.SetPlaceable(placeablePrefab);
    }

    public void LowerAmount() {
        SetAmount(amount - 1);
    }

    public void AddAmount() {
        SetAmount(amount + 1);
    }

    private void SetAmount(int newAmount) {
        amount = newAmount;
        amountText.text = amount.ToString();
    }

}
