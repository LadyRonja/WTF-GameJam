using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] bool isPlayButton;
    [SerializeField] bool isQuitButton;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Artifact>(out Artifact a))
        {
            if (isPlayButton)
            {
                StartGame();
                return;
            }

            if(isQuitButton)
            {
                Quit();
                return;
            }
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
