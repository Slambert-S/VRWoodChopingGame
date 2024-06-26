using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayQuickSound))]
public class PlaySoundController : MonoBehaviour
{
    public static PlaySoundController current { get; private set; }

    private PlayQuickSound _refPlayQuickSound;

    // Start is called before the first frame update
    void Start()
    {
        if (current != null && current != this)
        {
            Destroy(this);
        }
        else
        {
            current = this;
            _refPlayQuickSound = this.gameObject.GetComponent<PlayQuickSound>();
        }
    }


    public void PlayOneShot(AudioClip sound)
    {
        if(sound != null)
        {
            _refPlayQuickSound.Play(sound);
        }
    }
}
