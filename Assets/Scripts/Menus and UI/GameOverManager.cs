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
        {
            SceneManager.LoadScene("GameScene");
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("MainMenu");
        }
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

        StartCoroutine(FadeInOverTime(fade, 2, 0));
        StartCoroutine(FadeInOverTime(gameOverText, 1, 2));
        StartCoroutine(FadeInOverTime(retryText, 1, 3));
        StartCoroutine(FadeInOverTime(instructionsRetryText, 1, 4));
        StartCoroutine(FadeInOverTime(instructionsQuitText, 1, 4));
    }

    IEnumerator FadeInOverTime(TMP_Text text, float duration, float delay)
    {
        yield return new WaitForSeconds(delay);
        text.gameObject.SetActive(true);
        Color fadeStart = text.color;
        Color fadeEnd = text.color;
        fadeStart.a = 0f;
        text.color = fadeStart;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            text.color = Color.Lerp(fadeStart, fadeEnd, normalizedTime);
            yield return null;
        }
        text.color = fadeEnd;
    }

    IEnumerator FadeInOverTime(Image image, float duration, float delay)
    {
        yield return new WaitForSeconds(delay);
        image.gameObject.SetActive(true);
        Color fadeStart = image.color;
        Color fadeEnd = fadeStart;
        fadeStart.a = 0f;
        image.color = fadeStart;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            image.color = Color.Lerp(fadeStart, fadeEnd, normalizedTime);
            yield return null;
        }
        image.color = fadeEnd;
    }
}
