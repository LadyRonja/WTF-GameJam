using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource transitionSource;

    [SerializeField] AudioClip ambientMusic;
    [SerializeField] AudioClip chasedMusic;

    public Transform player;
    public Transform monster;


    private void Update()
    {
        DecideMusic();
    }

    private void DecideMusic()
    {
        if(FastMath.Distance(player.position, monster.position) >
            Mathf.Min(CameraController.Instance.Height, CameraController.Instance.Width))
        {
            if(musicSource.clip != ambientMusic)
            {
                musicSource.clip = ambientMusic;
                musicSource.Play();
            }
        }
        else
        {
            if (musicSource.clip != chasedMusic)
            {
                transitionSource.Play();
                transitionSource.volume = 2;
                musicSource.clip = chasedMusic;
                musicSource.Play();
            }
            transitionSource.volume -= Time.deltaTime/2;
        }
    }
}
