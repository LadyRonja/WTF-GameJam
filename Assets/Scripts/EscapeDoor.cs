using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeDoor : MonoBehaviour
{
    [SerializeField] GameObject gameWinScreen;
    [Space]
    [SerializeField] Image fade;
    [SerializeField] TMP_Text gameWinText;
    [SerializeField] TMP_Text keptArtifactText;
    [SerializeField] TMP_Text returnToMainMenuText;
    [SerializeField] string escapedWithArtifactPrompt = "You got what you came for... was it worth it?";
    [SerializeField] string escapedWithoutArtifactPrompt = "You didn't even get what you came for... at least you survived";

    bool gameIsOver = false;

    private void Start()
    {
        gameWinScreen.SetActive(false);
    }

    private void Update()
    {
        if (!gameIsOver)
            return;

        // Don't wanna mess with the input manager again, so not renaming the "Debug Multiplier, which takes the Y-input on an xbox-controller"
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Debug Multiplier"))
            SceneManager.LoadScene("MainMenu");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            // disable player input
            PlayerArtifactHandler playerArtifactData = player.gameObject.GetComponent<PlayerArtifactHandler>();
            bool playerHasArtifact = playerArtifactData.IsCarryingArtifact;
            playerArtifactData.enabled = false;
            player.enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            // disable hunter movement
            HunterScript hunter = GameObject.FindFirstObjectByType<HunterScript>();
            hunter.enabled = false;
            Rigidbody2D hunterBody = hunter.GetComponent<Rigidbody2D>();
            hunterBody.gravityScale = 0;
            hunterBody.velocity = Vector2.zero;

            GameIsOver(playerHasArtifact);
        }
    }

    private void GameIsOver(bool hasArtifact)
    {
        if (gameIsOver)
            return;

        gameIsOver = true;

        if (hasArtifact)
            keptArtifactText.text = escapedWithArtifactPrompt;
        else
            keptArtifactText.text = escapedWithoutArtifactPrompt;

        gameWinScreen.SetActive(true);
        fade.gameObject.SetActive(false);
        gameWinText.gameObject.SetActive(false);
        keptArtifactText.gameObject.SetActive(false);
        returnToMainMenuText.gameObject.SetActive(false);

        StartCoroutine(FadeInOverTime(fade, 2f, 0f));
        StartCoroutine(FadeInOverTime(gameWinText, 1f, 2f));
        StartCoroutine(FadeInOverTime(keptArtifactText, 1f, 3f));
        StartCoroutine(FadeInOverTime(returnToMainMenuText, 1f, 5f));
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
