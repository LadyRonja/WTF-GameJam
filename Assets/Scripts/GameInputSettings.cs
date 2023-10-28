using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInputSettings : MonoBehaviour
{
    public static GameInputSettings Instance;
    public bool usingGamepad = false;
    private bool usingGamePadLastFrame = false;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        #endregion

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        DetermineInputDevice();
    }

    private void LateUpdate()
    {
        if (usingGamepad != usingGamePadLastFrame)
        {
            if(SceneManager.GetActiveScene().name == "MainMenu")
                TutorialTextManager.Instance.UpdateTutorialText();

            Debug.Log("Changing input");
        }

        usingGamePadLastFrame = usingGamepad;
    }

    private void DetermineInputDevice()
    {
        if (Input.anyKeyDown)
        {
            usingGamepad = false;
        }

        if (Input.GetAxis("Hor") != 0 || Input.GetAxis("Ver") != 0 ||
            Input.GetAxis("HorAimController") != 0 || Input.GetAxis("VerAimController") != 0)
        {
            usingGamepad = true;
        }

    }
}
