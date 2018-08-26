using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMessagePanel : MonoBehaviour {

    public Text outcomeText;
    public Text buttonText;
    public Button continueButton;

    public void SetButtonToContinue() {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(GameManager.instance.NextLevel);
    }

    public void SetButtonToReload() {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(GameManager.instance.ReloadLevel);
    }
}
