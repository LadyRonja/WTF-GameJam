using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [SerializeField] GameObject gameoverScreen;
    [Space]
    [SerializeField] Image fade;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text retryText;
    [SerializeField] TMP_Text instructionsRetryText;
    [SerializeField] TMP_Text instructionsQuitText;

    bool gameIsOver = false;

    private void Awake()
    {
        #region singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        #endregion

        gameoverScreen.SetActive(false);
    }

    private void Update()
    {
        if (!gameIsOver) 
            return;

        // Don't wanna mess with the input manager again, so not renaming the "Debug Multiplier, which takes the Y-input on an xbox-controller"
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Debug Multiplier"))
            SceneManager.LoadScene("GameScene");

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Cancel"))
            SceneManager.LoadScene("MainMenu");
    }

    public void GameIsOver()
    {
        // Prevent repeat activations
        if(gameIsOver) return;
        gameIsOver = true;

        gameoverScreen.SetActive(true);
        fade.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retryText.gameObject.SetActive(false);
        instructionsRetryText.gameObject.SetActive(false);
        instructionsQuitText.gameObject.SetActive(false);

        StartCoroutine(Appear(0, fade.gameObject));
        StartCoroutine(Appear(0f, gameOverText.gameObject));
        StartCoroutine(Appear(1f, retryText.gameObject));
        StartCoroutine(Appear(2.5f, instructionsRetryText.gameObject));
        StartCoroutine(Appear(2.5f, instructionsQuitText.gameObject));
    }

    private IEnumerator Appear(float delay, GameObject gameObject)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(true);
        
        yield return null;
    }
}
