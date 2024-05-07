using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSoundSource : MonoBehaviour
{
    public AudioSource[] audioSources;
    private AudioSource currentlyPlaying;

    public float minBreakDuration = 2f;
    public float maxBreakDuration = 5f;

    private bool isOnBreak = false;

    void Start()
    {
        if (audioSources.Length == 0)
        {
            Debug.LogError("No audio sources attached!");
            return;
        }

        StartCoroutine(PlayWithBreaks());
    }

    IEnumerator PlayWithBreaks()
    {
        while (true)
        {
            if (!isOnBreak && (currentlyPlaying == null || !currentlyPlaying.isPlaying))
            {
                PlayRandomSound();
                yield return new WaitForSeconds(currentlyPlaying.clip.length);
            }
            else if (!isOnBreak)
            {
                yield return new WaitForSeconds(currentlyPlaying.clip.length - currentlyPlaying.time);
            }

            isOnBreak = true;
            float breakDuration = Random.Range(minBreakDuration, maxBreakDuration);
            yield return new WaitForSeconds(breakDuration);
            isOnBreak = false;
        }
    }

    void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, audioSources.Length);
        currentlyPlaying = audioSources[randomIndex];

        currentlyPlaying.Play();
    }
}
