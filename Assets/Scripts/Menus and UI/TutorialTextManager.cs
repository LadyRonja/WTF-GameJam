using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextManager : MonoBehaviour
{
    public static TutorialTextManager Instance;

    [Header("Movement")]
    [SerializeField] TMP_Text moveText;
    [SerializeField] string moveKeyboard = "WASD";
    [SerializeField] string moveGamepad = "Left Stick";

    [Header("Jumping")]
    [SerializeField] TMP_Text jumpText;
    [SerializeField] string jumpKeyboard = "Space";
    [SerializeField] string jumpGamepad = "'A' Button";

    [Header("Aim")]
    [SerializeField] TMP_Text aimText;
    [SerializeField] string aimKeyboard = "Right Mouse Button";
    [SerializeField] string aimGamepad = "Right Stick";

    [Header("Throw")]
    [SerializeField] TMP_Text thorwText;
    [SerializeField] string throwKeyboard = "Left Mouse Button";
    [SerializeField] string throwGamepad = "Right Bumper";

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        #endregion

        UpdateTutorialText();
    }

    public void UpdateTutorialText()
    {
        if (GameInputSettings.Instance.usingGamepad)
        {
            moveText.text = "<br> <br>" + moveGamepad;
            jumpText.text = "<br> <br>" + jumpGamepad;
            aimText.text = "<br> <br>" + aimGamepad;
            thorwText.text = "<br> <br>" + throwGamepad;
        }
        else
        {
            moveText.text = "<br> <br>" + moveKeyboard;
            jumpText.text = "<br> <br>" + jumpKeyboard;
            aimText.text = "<br> <br>" + aimKeyboard;
            thorwText.text = "<br> <br>" + throwKeyboard;
        }
    }
}
