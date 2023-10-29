using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReplayText : MonoBehaviour
{
    public static ReplayText Instance;

    [Header("Victory")]
    [SerializeField] TMP_Text victoryText;
    [SerializeField] string victoryQuitKeyboard = "'R' to return to the Main Menu";
    [SerializeField] string victoryQuitGamepad = "'Y' Button to return to the Main Menu";

    [Header("Defeat")]
    [SerializeField] TMP_Text defeatRetryText;
    [SerializeField] TMP_Text defeatQuitText;
    [SerializeField] string defeatKeyboardReplay = "'R' to return to Replay";
    [SerializeField] string defeatKeyboardQuit = "'Q' to return to the Main Menu";
    [SerializeField] string defeatGamepadReplay = "'Y' Button to return to Replay";
    [SerializeField] string defeatGamepadQuit = "'B' Button to return to the Main Menu";

    void Start()
    {
        #region Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        #endregion

        UpdateInstructionsText();
    }

    public void UpdateInstructionsText()
    {
        if (GameInputSettings.Instance.usingGamepad)
        {
            victoryText.text = victoryQuitGamepad;
            defeatRetryText.text = defeatGamepadReplay;
            defeatQuitText.text = defeatGamepadQuit;
        }
        else
        {
            victoryText.text = victoryQuitKeyboard;
            defeatRetryText.text = defeatKeyboardReplay;
            defeatQuitText.text = defeatKeyboardQuit;
        }
    }
}
