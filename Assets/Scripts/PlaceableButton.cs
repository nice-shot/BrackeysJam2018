using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableButton : MonoBehaviour {

    public Placeable placeablePrefab;
    public int amount;
    public Text amountText;
    public Text nameText;
    public Button button;

    // To set up light icons
    public Transform lightIconHolder;
    public Image lightIconPrefab;
    public Sprite moonSprite;
    public Sprite sunSprite;

    private GameManager manager;

    private void Awake() {
        manager = GameManager.instance;
    }

    public void Setup() {
        amountText.text = amount.ToString();
        nameText.text = placeablePrefab.name;
        // TODO: Add icon
        if (placeablePrefab is Plant) {
            AddLightIcons(placeablePrefab as Plant);
        }
    }

    private void AddLightIcons(Plant placeablePlant) {
        if (Mathf.Approximately(placeablePlant.sunNeeds, 1f)) {
            AddIcon(true);
            return;
        }

        if (Mathf.Approximately(placeablePlant.sunNeeds, 0.75f)) {
            AddIcon(false);
            AddIcon(true);
            AddIcon(true);
            AddIcon(true);
            return;
        }

        if (Mathf.Approximately(placeablePlant.sunNeeds, 0.5f)) {
            AddIcon(false);
            AddIcon(true);
            return;
        }

        if (Mathf.Approximately(placeablePlant.sunNeeds, 0.25f)) {
            AddIcon(false);
            AddIcon(false);
            AddIcon(false);
            AddIcon(true);
            return;
        }

        if (Mathf.Approximately(placeablePlant.sunNeeds, 0f)) {
            AddIcon(false);
            return;
        }

    }

    private void AddIcon(bool isSun) {
        Image icon = Instantiate(lightIconPrefab, lightIconHolder);
        if (isSun) {
            icon.sprite = sunSprite;
        } else {
            icon.sprite = moonSprite;
        }
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
        if (amount <= 0) {
            button.interactable = false;
        } else {
            button.interactable = true;
        }
    }

}
